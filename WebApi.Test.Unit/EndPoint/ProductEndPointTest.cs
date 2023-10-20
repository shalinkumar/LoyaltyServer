using Application.Products.Model;
using Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using webapi.EndPoints;
using Shouldly;
using webapi.Requests.Product;
using Application.Products.CreateProduct;
using Application.Products.UpdateProduct;
using Application.Products.DeleteProduct;

namespace WebApi.Test.Unit.EndPoint
{
    public class ProductEndPointTest
    {
        [Fact]
        public async Task GetAllProducts_ShouldReturn_OK()
        {
            var mediator = Substitute.For<IMediator>();

            _ = mediator.Send(Arg.Any<GetProductsQuery>())
           .ReturnsForAnyArgs(new List<ProductModel>
           {
                new ProductModel
                {
                    Name = "Shalin",
                    Description = "Test",
                    Category = "Test",
                    Price = "12",
                    Color = "Red"
                }
           });

            //Act
            IResult response = await ProductEndPoint.GetProducts(mediator);

            // Assert
            Ok<List<ProductModel>> result = response.ShouldBeOfType<Ok<List<ProductModel>>>();

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);

            var value = result.Value.ShouldBeOfType<List<ProductModel>>();

            _ = value[0].Name.ShouldBeOfType<string>();
            value[0].Name.ShouldBe("Shalin");
            _ = value[0].Description.ShouldBeOfType<string>();
            value[0].Description.ShouldBe("Test");
            _ = value[0].Category.ShouldBeOfType<string>();
            value[0].Category.ShouldBe("Test");
            _ = value[0].Price.ShouldBeOfType<string>();
            value[0].Price.ShouldBe("12");
            _ = value[0].Color.ShouldBeOfType<string>();
            value[0].Color.ShouldBe("Red");
        }

        [Fact]
        public async Task CreateProducts_ShouldReturn_Created()
        {
            // Arrange
            var httpRequest = Substitute.For<HttpRequest>();
            var mediator = Substitute.For<IMediator>();
            var request = new CreateProductRequest
            {
                Name = "Shalin",
                UserDescription = "Test",
                Category = "Test",
                Price = "12",
                Color = "Red"
            };


            _ = mediator
            .Send(Arg.Any<CreateProdcutCommand>())
            .ReturnsForAnyArgs(new ProductModel
            {
                Name = "Shalin",
                Description = "Test",
                Category = "Test",
                Price = "12",
                Color = "Red"
            });

            // Act
            var response = await ProductEndPoint.CreateProduct(request, mediator, httpRequest);

            // Assert
            var result = response.ShouldBeOfType<Created<ProductModel>>();
            //var result =  response.ShouldBeOfType<Ok<List<ProductModel>>>();

            result.StatusCode.ShouldBe(StatusCodes.Status201Created);

            var value = result.Value.ShouldBeOfType<ProductModel>();


            _ = value.Name.ShouldBeOfType<string>();
            value.Name.ShouldBe("Shalin");
            _ = value.Description.ShouldBeOfType<string>();
            value.Description.ShouldBe("Test");
            _ = value.Category.ShouldBeOfType<string>();
            value.Category.ShouldBe("Test");
            _ = value.Price.ShouldBeOfType<string>();
            value.Price.ShouldBe("12");
            _ = value.Color.ShouldBeOfType<string>();
            value.Color.ShouldBe("Red");
        }

        [Fact]
        public async Task UpdateProducts_ShouldReturn_Updated()
        {
            // Arrange
            var httpRequest = Substitute.For<HttpRequest>();
            var mediator = Substitute.For<IMediator>();
            var request = new UpdateProductRequest
            {
                Name = "Shalin",
                UserDescription = "Test",
                Category = "Test",
                Price = "12",
                Color = "Red"
            };

            _ = mediator
            .Send(Arg.Any<UpdateProdcutCommand>())
            .ReturnsForAnyArgs(new ProductModel
            {
                Name = "Shalin",
                Description = "Test",
                Category = "Test",
                Price = "12",
                Color = "Red"
            });

            // Act
            var response = await ProductEndPoint.UpdateProduct(request, mediator, httpRequest);

            // Assert
            var result = response.ShouldBeOfType<Created<ProductModel>>();

            result.StatusCode.ShouldBe(StatusCodes.Status201Created);

            var value = result.Value.ShouldBeOfType<ProductModel>();


            _ = value.Name.ShouldBeOfType<string>();
            value.Name.ShouldBe("Shalin");
            _ = value.Description.ShouldBeOfType<string>();
            value.Description.ShouldBe("Test");
            _ = value.Category.ShouldBeOfType<string>();
            value.Category.ShouldBe("Test");
            _ = value.Price.ShouldBeOfType<string>();
            value.Price.ShouldBe("12");
            _ = value.Color.ShouldBeOfType<string>();
            value.Color.ShouldBe("Red");

        }

        [Fact]
        public async Task DeleteProduct_ShouldReturn_NoContent()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var httpRequest = Substitute.For<HttpRequest>();

            _ = mediator
                .Send(Arg.Any<DeleteProdcutCommand>())
                .ReturnsForAnyArgs(true);

            // Act
            var response = await ProductEndPoint.DeleteProduct("", mediator, httpRequest);

            // Assert
            var result = response.ShouldBeOfType<NoContent>();

            result.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }

    }
}
