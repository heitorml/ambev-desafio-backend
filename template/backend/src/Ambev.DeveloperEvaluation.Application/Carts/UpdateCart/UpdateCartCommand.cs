using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommand: IRequest<UpdateCartResult>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Branch { get; set; } = string.Empty;

        public CartStatus Status { get; set; }

        public List<UpdateCartItemCommand> Products { get; set; } = new List<UpdateCartItemCommand>();


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

    public class UpdateCartItemCommand
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
