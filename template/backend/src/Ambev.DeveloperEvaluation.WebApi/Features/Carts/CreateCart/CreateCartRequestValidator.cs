using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartRequestValidator()
        {
            RuleFor(Cart => Cart.Branch).NotEmpty().NotNull();
            RuleFor(Cart => Cart.Products)
                .NotEmpty()
                .WithMessage("A lista de produtos não pode ser vazia.")
                .Custom(ValidateMaxPerProducts);
        }

        private void ValidateMaxPerProducts(
            List<ProductResquest> produtos,
            ValidationContext<CreateCartRequest> context)
        {
            if (produtos == null || !produtos.Any())
                return;

            var produtosInvalidos = produtos
                .GroupBy(p => p.Id)
                .Select(grupo => new { ProdutoId = grupo.Key, Quantidade = grupo.Count() })
                .Where(p => p.Quantidade > 20);

            foreach (var invalido in produtosInvalidos)
            {
                context.AddFailure(
                   nameof(Cart.Products),
                   $"O produto com ID {invalido.ProdutoId} excedeu o limite. Encontrados {invalido.Quantidade} registros, mas o limite é 20."
               );
            }
        }
    }
}
