using BuildingBlocks.CQRS;
using Catalog.API.Models;

using System.Windows.Input;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) :
    ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Imagefile is Required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be Greater than 0");

    }
}
internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateProductCommandHandler.Handle called with {@command}", command);

        //Create product entity from command object
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        //Save to the database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //Return CreateProductResult result
        return new CreateProductResult(product.Id);


    }
}
