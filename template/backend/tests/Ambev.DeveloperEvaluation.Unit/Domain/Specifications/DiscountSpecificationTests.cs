using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class DiscountSpecificationTests
{
    [Theory(DisplayName = "IsSatisfiedBy should return expected result based on quantity range")]
    [InlineData(5, 10, 5, true)]  
    [InlineData(5, 10, 10, true)] 
    [InlineData(5, 10, 7, true)]  
    [InlineData(5, 10, 4, false)] 
    [InlineData(5, 10, 11, false)] 
    public void IsSatisfiedBy_QuantityRange_ReturnsExpectedResult(int min, int max, int quantity, bool expected)
    {
        // Given
        var specification = new DiscountSpecification(min, max);
        var cartProduct = new CartProducts
        {
            Quantity = quantity
        };

        // When
        var result = specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().Be(expected);
    }

    [Fact(DisplayName = "RulesDiscountConfiguration should return predefined rules")]
    public void RulesDiscountConfiguration_GetRules_ReturnsExpectedRules()
    {
        // When
        var rules = RulesDiscountConfiguration.GetRules();

        // Then
        rules.Should().NotBeNull();
        rules.Should().HaveCount(2);

        // Rule 1: 10% for 5-9 items
        var rule1 = rules[0];
        rule1.Name.Should().Contain("Acima de 4");
        rule1.DiscountPercentage.Should().Be(10m);
        rule1.Specification.IsSatisfiedBy(new CartProducts { Quantity = 5 }).Should().BeTrue();
        rule1.Specification.IsSatisfiedBy(new CartProducts { Quantity = 4 }).Should().BeFalse();
        rule1.Specification.IsSatisfiedBy(new CartProducts { Quantity = 9 }).Should().BeTrue();
        rule1.Specification.IsSatisfiedBy(new CartProducts { Quantity = 10 }).Should().BeFalse();

        // Rule 2: 20% for 10-20 items
        var rule2 = rules[1];
        rule2.Name.Should().Contain("10 e 20");
        rule2.DiscountPercentage.Should().Be(20m);
        rule2.Specification.IsSatisfiedBy(new CartProducts { Quantity = 10 }).Should().BeTrue();
        rule2.Specification.IsSatisfiedBy(new CartProducts { Quantity = 9 }).Should().BeFalse();
        rule2.Specification.IsSatisfiedBy(new CartProducts { Quantity = 20 }).Should().BeTrue();
        rule2.Specification.IsSatisfiedBy(new CartProducts { Quantity = 21 }).Should().BeFalse();
    }
}
