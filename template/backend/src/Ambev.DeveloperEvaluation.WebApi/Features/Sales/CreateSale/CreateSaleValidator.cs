using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.UserId).NotEmpty();

            RuleFor(sale => sale.Products)
                .NotEmpty()
                .WithMessage("A lista de produtos não pode ser vazia.")
                .Custom(ValidateMaxPerProducts);
        }

        private void ValidateMaxPerProducts(
            List<ProductResquest> produtos,
            ValidationContext<CreateSaleRequest> context)
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
                   nameof(Sale.Products),
                   $"O produto com ID {invalido.ProdutoId} excedeu o limite. Encontrados {invalido.Quantidade} registros, mas o limite é 20."
               );
            }
        }
    }
}
