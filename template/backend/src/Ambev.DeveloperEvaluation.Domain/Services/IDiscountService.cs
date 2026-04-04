using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IDiscountService
    {
        void ApplyDiscounts(List<CartProducts> items);
    }

    public class DiscountService
    {
        private readonly List<DiscountRule> _rules;
        public DiscountService(List<DiscountRule> rules)
        {
            _rules = rules;
        }
        public void ApplyDiscounts(List<CartProducts> items)
        {
            foreach (var item in items)
            {
                var applicableRules = _rules.Where(r => r.Specification.IsSatisfiedBy(item)).ToList();
                item.TotalPrice = item.CalculateTotalPrice();

                if (applicableRules.Any())
                {
                    var bestRule = applicableRules.OrderByDescending(r => r.DiscountPercentage).First();
                   
                    item.Discount = item.TotalPrice * (bestRule.DiscountPercentage / 100m);
                }
            }
        }
    }


}
