using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class DiscountSpecification : ISpecification<CartProducts>
    {
        private readonly int _min;
        private readonly int _max;

        public DiscountSpecification(int min, int max)
        {
            _min = min;
            _max = max; // Para o caso de não ter limite, podemos passar int.MaxValue
        }

        public bool IsSatisfiedBy(CartProducts entity)
        {
            return entity.Quantity >= _min && entity.Quantity <= _max;
        }
    }

    public class DiscountRule
    {
        public string Name { get; set; } = string.Empty;
        public ISpecification<CartProducts> Specification { get; set; } = null!;
        public decimal DiscountPercentage { get; set; }
    }

    public static class RulesDiscountConfiguration
    {
        public static List<DiscountRule> GetRules()
        {
            return new List<DiscountRule>
        {
            // REGRA 1: Acima de 4 (Mínimo de 5, até o limite de 9 para não encavalar com a próxima regra)
            new DiscountRule
            {
                Name = "Desconto Atacado Leve - Acima de 4 itens",
                DiscountPercentage = 10m, 
                // Exige entre 5 e 9 unidades iguais
                Specification = new DiscountSpecification(min: 5, max: 9)
            },
            // REGRA 2: Entre 10 e 20 itens
            new DiscountRule
            {
                Name = "Desconto Atacado Padrão - Entre 10 e 20 itens",
                DiscountPercentage = 20m, 
                // Exige entre 10 e 20 unidades iguais
                Specification = new DiscountSpecification(min: 10, max: 20)
            }
        };
        }
    }

}
