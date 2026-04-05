using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class ActiveUserSpecificationTests
{
    private readonly ActiveUserSpecification _specification;

    public ActiveUserSpecificationTests()
    {
        _specification = new ActiveUserSpecification();
    }

    [Fact(DisplayName = "IsSatisfiedBy should return true when user is active")]
    public void IsSatisfiedBy_ActiveUser_ReturnsTrue()
    {
        // Given
        var user = new User
        {
            Status = UserStatus.Active
        };

        // When
        var result = _specification.IsSatisfiedBy(user);

        // Then
        result.Should().BeTrue();
    }

    [Theory(DisplayName = "IsSatisfiedBy should return false when user is not active")]
    [InlineData(UserStatus.Inactive)]
    [InlineData(UserStatus.Suspended)]
    public void IsSatisfiedBy_InactiveOrSuspendedUser_ReturnsFalse(UserStatus status)
    {
        // Given
        var user = new User
        {
            Status = status
        };

        // When
        var result = _specification.IsSatisfiedBy(user);

        // Then
        result.Should().BeFalse();
    }
}
