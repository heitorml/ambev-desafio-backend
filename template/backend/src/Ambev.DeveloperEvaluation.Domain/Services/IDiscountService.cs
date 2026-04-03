using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IDiscountService
    {
        void ApplyDiscounts(List<Product> items);
    }

    public class DiscountService
    {
        private readonly List<DiscountRule> _rules;
        public DiscountService(List<DiscountRule> rules)
        {
            _rules = rules;
        }
        public void ApplyDiscounts(List<Product> items)
        {
            foreach (var item in items)
            {
                // Busca todas as regras que o item atende
                var applicableRules = _rules.Where(r => r.Specification.IsSatisfiedBy(item)).ToList();
                if (applicableRules.Any())
                {
                    // Estratégia: Aplicar a regra que dá o MAIOR desconto para o cliente
                    var bestRule = applicableRules.OrderByDescending(r => r.DiscountPercentage).First();

                    item.Discount =  (item.Quantity * item.UnitPrice) * (bestRule.DiscountPercentage / 100m);

                    Console.WriteLine($"[Sistema] Regra '{bestRule.Name}' aplicada no {item.ProductName}. " +
                                      $"{bestRule.DiscountPercentage}% de desconto concedido.");
                }
            }
        }
    }


}
