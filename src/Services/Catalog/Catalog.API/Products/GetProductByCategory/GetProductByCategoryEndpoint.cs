namespace Catalog.API.Products.GetProductByCategory;

//public record GetProductByCategoryRequest(String Category);
public record GetProductByCategoryResponse(IEnumerable<Product> Products);
public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string Category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(Category));

            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(result);
        }).WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category"); ;
    }
}
