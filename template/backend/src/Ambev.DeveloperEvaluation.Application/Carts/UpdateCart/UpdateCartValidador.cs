using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartValidador : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidador()
        {
            RuleFor(cart => cart.Branch).NotEmpty().Length(3, 50);
            RuleFor(cart => cart.Id).NotNull().NotEmpty();
            RuleFor(cart => cart.UserId).NotNull().NotEmpty();

            RuleFor(cart => cart.Products)
              .NotEmpty()
              .WithMessage("A lista de produtos não pode ser vazia.")
              .Custom(ValidateMaxProducts);
        }


        private void ValidateMaxProducts(
            List<UpdateCartItemCommand> produtos,
            ValidationContext<UpdateCartCommand> context)
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
                   nameof(Products),
                   $"O produto com ID {invalido.ProdutoId} excedeu o limite. Encontrados {invalido.Quantidade} registros, mas o limite é 20."
               );
            }
        }
    }
}
