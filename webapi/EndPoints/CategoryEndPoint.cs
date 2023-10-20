using Application.Category.CreateCategory;
using Application.Category.DeleteCatogery;
using Application.Category.Model;
using Application.Category.Queries;
using Application.Category.UpdateCategory;
using Application.Products.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using webapi.Requests.Category;
using webapi.Validators;

namespace webapi.EndPoints
{
    public static class CategoryEndPoint
    {
        private static readonly UpdateCategoryValidator updateValidator = new();
        private static readonly CreateCategoryValidator createValidator = new();

        public static void MapCategoryEndPoint(this WebApplication web)
        {

            web.MapGet("/category", GetCategory)
               .Produces<List<CategoryModel>>()
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithSummary("Lookup all Products");

            web.MapPost("/create-category", CreateCategory)
            .Produces<ProductModel>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Create a Product");

            web.MapDelete("/delete-category/{id}", DeleteCategory)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .ProducesValidationProblem()
                .WithSummary("Delete a Product by its Id");

            web.MapPut("/update-category", UpdateCategory)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .ProducesValidationProblem()
                .WithSummary("Update a Product");

        }

        public static async Task<IResult> GetCategory(IMediator mediator)
        {
            try
            {
                return Results.Ok(await mediator.Send(new GetAllCategoryQuery()));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> CreateCategory([FromBody] CreateCategoryRequest categoryRequest, IMediator mediator, HttpRequest httpRequest)
        {
            try
            {
                var validationResult = createValidator.Validate(categoryRequest);

                if (!validationResult.IsValid)
                {
                    var stringBuilder = new StringBuilder();
                    foreach (var err in validationResult.Errors)
                    {
                        stringBuilder.Append(err.ErrorMessage);
                        stringBuilder.Append("\r\n");
                    }
                    throw new ValidationException(stringBuilder.ToString());
                }

                return Results.Created(
                    UriHelper.GetEncodedUrl(httpRequest),
                    await mediator.Send(new CreateCategoryCommand
                    {
                        Name = categoryRequest.Name,
                        Description = categoryRequest.Description
                    }));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> UpdateCategory([FromBody] UpdateCategoryRequest updateRequest, IMediator mediator, HttpRequest httpRequest)
        {
            try
            {
                var validationResult = updateValidator.Validate(updateRequest);

                if (!validationResult.IsValid)
                {
                    var stringBuilder = new StringBuilder();
                    foreach (var err in validationResult.Errors)
                    {
                        stringBuilder.Append(err.ErrorMessage);
                        stringBuilder.Append("\r\n");
                    }
                    throw new ValidationException(stringBuilder.ToString());
                }

                return Results.Created(
                    UriHelper.GetEncodedUrl(httpRequest),
                    await mediator.Send(new UpdateCategoryCommand
                    {
                        Id = updateRequest.Id,
                        Name = updateRequest.Name,
                        Description = updateRequest.Description,
                    }));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> DeleteCategory(string id, IMediator mediator, HttpRequest httpRequest)
        {
            try
            {
                Results.Created(
                   UriHelper.GetEncodedUrl(httpRequest),
                   await mediator.Send(new DeleteCategoryCommand
                   {
                       Id = id.ToString(),
                   }));
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
