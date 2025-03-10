using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;

namespace Bezkres.Tests.Entities;

public class EntityTests
{
    [Fact]
    public void AddComponent_ShouldAddComponent()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);
        var healthComponent = new HealthComponent() { Health = 100 };

        // Act
        entity.AddComponent(healthComponent);

        // Assert
        var component = entity.GetComponent<HealthComponent>();
        Assert.NotNull(component);
        Assert.Equal(100, component.Health);
    }

    [Fact]
    public void AddComponent_ShouldThrowException_WhenComponentAlreadyExists()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);

        var healthComponent = new HealthComponent();
        entity.AddComponent(healthComponent);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => entity.AddComponent(new HealthComponent()));
        Assert.Equal($"Component of type {healthComponent.GetType()} already exists.", exception.Message);
    }

    [Fact]
    public void AddComponents_WithMultipleComponents_ShouldAddAllComponents()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);

        var healthComponent = new HealthComponent();
        var nutritionComponent = new NutritionComponent();

        // Act
        entity.AddComponents(new List<IComponent> { healthComponent, nutritionComponent });

        // Assert
        Assert.NotNull(entity.GetComponent<HealthComponent>());
        Assert.NotNull(entity.GetComponent<NutritionComponent>());
    }

    [Fact]
    public void GetComponent_ShouldReturnNull_WhenComponentDoesNotExist()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);


        // Act
        var component = entity.GetComponent<HealthComponent>();

        // Assert
        Assert.Null(component);
    }

    [Fact]
    public void RemoveComponent_ShouldRemoveComponent()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);

        var healthComponent = new HealthComponent();
        entity.AddComponent(healthComponent);

        // Act
        var result = entity.RemoveComponent<HealthComponent>();

        // Assert
        Assert.True(result);
        Assert.Null(entity.GetComponent<HealthComponent>());
    }

    [Fact]
    public void RemoveComponent_ShouldReturnFalse_WhenComponentDoesNotExist()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);


        // Act
        var result = entity.RemoveComponent<HealthComponent>();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasComponent_ShouldReturnTrue_WhenComponentExists()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);

        var healthComponent = new HealthComponent();
        entity.AddComponent(healthComponent);

        // Act
        var result = entity.HasComponent<HealthComponent>();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasComponent_ShouldReturnFalse_WhenComponentDoesNotExist()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);


        // Act
        var result = entity.HasComponent<HealthComponent>();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddComponent_ShouldThrowArgumentNullException_WhenComponentIsNull()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);


        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => entity.AddComponent(null));
        Assert.Equal("component", exception.ParamName);
    }

    [Fact]
    public void AddComponent_WithMultipleComponents_ShouldThrowException_WhenAnyComponentAlreadyExists()
    {
        // Arrange
        var entity = new Entity(EntityTypes.Player);
        var healthComponent = new HealthComponent();
        entity.AddComponent(healthComponent);

        var newComponents = new List<IComponent> { new HealthComponent(), new NutritionComponent() };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => entity.AddComponents(newComponents));
        Assert.Equal($"Component of type {healthComponent.GetType()} already exists.", exception.Message);
    }
}
