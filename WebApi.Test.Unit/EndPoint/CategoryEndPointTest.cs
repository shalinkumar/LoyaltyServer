using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using webapi.EndPoints;
using Application.Category.Queries;
using Application.Category.Model;
using Shouldly;
using Application.Category.CreateCategory;
using webapi.Requests.Category;
using Application.Category.UpdateCategory;
using Application.Category.DeleteCatogery;

namespace WebApi.Test.Unit.EndPoint
{
    public class CategoryEndPointTest
    {
        [Fact]
        public async Task GetAllCategory_ShouldReturn_OK()
        {
            var mediator = Substitute.For<IMediator>();

            _ = mediator.Send(Arg.Any<GetAllCategoryQuery>())
           .ReturnsForAnyArgs(new List<CategoryModel>
           {
                new CategoryModel
                {
                    Name = "Shalin",
                    Description = "Test"
                }
           });

            //Act
            var response = await CategoryEndPoint.GetCategory(mediator);

            // Assert
            var result = response.ShouldBeOfType<Ok<List<CategoryModel>>>();

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);

            var value = result.Value.ShouldBeOfType<List<CategoryModel>>();

            _ = value[0].Name.ShouldBeOfType<string>();
            value[0].Name.ShouldBe("Shalin");
            _ = value[0].Description.ShouldBeOfType<string>();
            value[0].Description.ShouldBe("Test");
        }

        [Fact]
        public async Task CreateCategory_ShouldReturn_Created()
        {
            // Arrange
            var httpRequest = Substitute.For<HttpRequest>();
            var mediator = Substitute.For<IMediator>();
            var request = new CreateCategoryRequest
            {
                Name = "Shalin",
                Description = "Test"
            };

            _ = mediator
            .Send(Arg.Any<CreateCategoryCommand>())
            .ReturnsForAnyArgs(new CategoryModel
            {
                Name = "Shalin",
                Description = "Test"
            });

            // Act
            var response = await CategoryEndPoint.CreateCategory(request, mediator, httpRequest);

            // Assert
            var result = response.ShouldBeOfType<Created<CategoryModel>>();

            result.StatusCode.ShouldBe(StatusCodes.Status201Created);

            var value = result.Value.ShouldBeOfType<CategoryModel>();

            _ = value.Name.ShouldBeOfType<string>();
            value.Name.ShouldBe("Shalin");
            _ = value.Description.ShouldBeOfType<string>();
            value.Description.ShouldBe("Test");

        }

        [Fact]
        public async Task UpdateCategory_ShouldReturn_Updated()
        {
            // Arrange
            var httpRequest = Substitute.For<HttpRequest>();
            var mediator = Substitute.For<IMediator>();
            var request = new UpdateCategoryRequest
            {
                Name = "Shalin",
                Description = "Test"
            };

            _ = mediator
            .Send(Arg.Any<UpdateCategoryCommand>())
            .ReturnsForAnyArgs(new CategoryModel
            {
                Name = "Shalin",
                Description = "Test",
            });

            // Act
            var response = await CategoryEndPoint.UpdateCategory(request, mediator, httpRequest);

            // Assert
            var result = response.ShouldBeOfType<Created<CategoryModel>>();

            result.StatusCode.ShouldBe(StatusCodes.Status201Created);

            var value = result.Value.ShouldBeOfType<CategoryModel>();

            _ = value.Name.ShouldBeOfType<string>();
            value.Name.ShouldBe("Shalin");
            _ = value.Description.ShouldBeOfType<string>();
            value.Description.ShouldBe("Test");

        }

        [Fact]
        public async Task DeleteCategory_ShouldReturn_NoContent()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var httpRequest = Substitute.For<HttpRequest>();

            _ = mediator
                .Send(Arg.Any<DeleteCategoryCommand>())
                .ReturnsForAnyArgs(true);

            // Act
            var response = await CategoryEndPoint.DeleteCategory("1", mediator, httpRequest);

            // Assert
            var result = response.ShouldBeOfType<NoContent>();

            result.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }
    }
}
