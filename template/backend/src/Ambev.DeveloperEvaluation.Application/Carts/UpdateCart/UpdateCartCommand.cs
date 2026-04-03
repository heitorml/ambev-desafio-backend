using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommand: IRequest<UpdateCartResult>
    {
        public Guid cartsId { get; set; }

        public Guid UserId { get; set; }

        public string Branch { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new List<Product>();


        public ValidationResultDetail Validate()
        {
            var validator = new UpdateCartValidador();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
