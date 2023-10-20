using Application.Products.Model;
using Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using Application.Products.CreateProduct;
using Application.Products.UpdateProduct;
using Application.Products.DeleteProduct;
using webapi.Requests.Product;
using webapi.Validators;
using FluentValidation;
using System.Text;

namespace webapi.EndPoints
{
    public static class ProductEndPoint
    {
        private static readonly CreateProductValidator createValidator = new();
        private static readonly UpdateProductValidator updateValidator = new();

        public static void MapProductEndPoint(this WebApplication web)
        {

            var root = web.MapGroup("/products").WithTags("products").WithOpenApi();

            web.MapGet("/products", GetProducts)
               .Produces<List<ProductModel>>()
               .ProducesProblem(StatusCodes.Status500InternalServerError)
               .WithSummary("Lookup all Products")
               .WithDescription("\n    GET /Products");

            web.MapPost("/create-product", CreateProduct)
            .Produces<ProductModel>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Create a Product");

            web.MapDelete("/delete-product/{id}", DeleteProduct)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .ProducesValidationProblem()
                .WithSummary("Delete a Product by its Id");

            web.MapPut("/update-product", UpdateProduct)
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .ProducesValidationProblem()
                .WithSummary("Update a Product");
        }

        public static async Task<IResult> GetProducts(IMediator mediator)
        {
            try
            {
                return Results.Ok(await mediator.Send(new GetProductsQuery()));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> CreateProduct([FromBody] CreateProductRequest productRequest, IMediator mediator, HttpRequest httpRequest)
        {
            try
            {
                var validationResult = createValidator.Validate(productRequest);

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
                    await mediator.Send(new CreateProdcutCommand
                    {
                        Name = productRequest.Name,
                        UserDescription = productRequest.UserDescription,
                        Price = productRequest.Price,
                        Category = productRequest.Category,
                        Color = productRequest.Color

                    }));


            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> UpdateProduct([FromBody] UpdateProductRequest updateRequest, IMediator mediator, HttpRequest httpRequest)
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
                    await mediator.Send(new UpdateProdcutCommand
                    {
                        Id = updateRequest.Id,
                        Name = updateRequest.Name,
                        UserDescription = updateRequest.UserDescription,
                        Price = updateRequest.Price,
                        Category = updateRequest.Category,
                        Color = updateRequest.Color

                    }));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<IResult> DeleteProduct(string id, IMediator mediator, HttpRequest httpRequest)
        {
            try
            {
                Results.Created(
                    UriHelper.GetEncodedUrl(httpRequest),
                    await mediator.Send(new DeleteProdcutCommand
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
