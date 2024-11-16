using crud2_newfeature.Dtos.Products;
using crudAPI.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crud_api2.Dtos.Products
{
    public class ProductDtoValidation : AbstractValidator<CreateProductDto>
    {
        public ProductDtoValidation()
        {
            // Validation for Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.") 
                .MinimumLength(5).WithMessage("Name must be at least 5 characters long.")
                .MaximumLength(30).WithMessage("Name must not exceed 30 characters.");

         
            RuleFor(x => x.price)
                .NotEmpty().WithMessage("Price is required.") 
                .InclusiveBetween(20, 3000).WithMessage("Price must be between 20 and 3000.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.") 
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.");
        }

       

    }
}
