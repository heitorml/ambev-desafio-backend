using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidador : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidador()
        {
            RuleFor(sale => sale.Branch).NotEmpty().Length(3, 50);
            RuleFor(sale => sale.SalesId).NotNull().NotEmpty();
            RuleFor(sale => sale.UserId).NotNull().NotEmpty();
            
            RuleFor(sale => sale.Products)
              .NotEmpty()
              .WithMessage("A lista de produtos não pode ser vazia.")
              .Custom(ValidateMaxProducts); 
        }


        private void ValidateMaxProducts(
            List<Product> produtos, 
            ValidationContext<UpdateSaleCommand> context)
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
