using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Managers;
using Bezkres.ConsoleApp.Systems.PlayState;

namespace Bezkres.Tests.Systems;

public class InputSystemTests
{
    [Theory]
    [InlineData("w", CommandTypes.MoveToWest)]
    [InlineData("zach", CommandTypes.MoveToWest)]
    [InlineData("west", CommandTypes.MoveToWest)]
    [InlineData("zachod", CommandTypes.MoveToWest)]
    [InlineData("zachód", CommandTypes.MoveToWest)]
    [InlineData("e", CommandTypes.MoveToEast)]
    [InlineData("east", CommandTypes.MoveToEast)]
    [InlineData("wsch", CommandTypes.MoveToEast)]
    [InlineData("wschod", CommandTypes.MoveToEast)]
    [InlineData("wschód", CommandTypes.MoveToEast)]
    [InlineData("n", CommandTypes.MoveToNorth)]
    [InlineData("north", CommandTypes.MoveToNorth)]
    [InlineData("północ", CommandTypes.MoveToNorth)]
    [InlineData("polnoc", CommandTypes.MoveToNorth)]
    [InlineData("pn", CommandTypes.MoveToNorth)]
    [InlineData("s", CommandTypes.MoveToSouth)]
    [InlineData("south", CommandTypes.MoveToSouth)]
    [InlineData("poludnie", CommandTypes.MoveToSouth)]
    [InlineData("południe", CommandTypes.MoveToSouth)]
    [InlineData("pd", CommandTypes.MoveToSouth)]
    [InlineData("help", CommandTypes.Help)]
    [InlineData("pomoc", CommandTypes.Help)]
    public void Update_ShouldSetCommandType_WhenValidCommandIsEntered(string input, CommandTypes suspectCommand)
    {
        // Arrange
        var entityManager = new EntityManager();
        var inputSystem = new InputSystem();
        entityManager.AddSystem(inputSystem);
        var player = new Entity();
        player.AddComponent(new LoggerComponent());
        player.AddComponent(new CommandComponent());
        entityManager.RegisterEntity(player);

        using(var consoleInput = new StringReader(input))
        {
            Console.SetIn(consoleInput);

            // Act
            inputSystem.Update();

            // Assert
            var commandComponent = player.GetComponent<CommandComponent>();
            Assert.NotNull(commandComponent);
            Assert.Equal(suspectCommand, commandComponent.CommandTypes);
        }
    }

    [Fact]
    public void Update_ShouldSetCommandTypeToNone_WhenUnknownCommandIsEntered()
    {
        // Arrange
        var inputSystem = new InputSystem();
        var player = new Entity();
        player.AddComponent(new CommandComponent());


        // Symulacja wejścia użytkownika
        var input = "unknown";
        using(var consoleInput = new StringReader(input))
        {
            Console.SetIn(consoleInput);

            // Act
            inputSystem.Update();

            // Assert
            var commandComponent = player.GetComponent<CommandComponent>();
            Assert.NotNull(commandComponent);
            Assert.Equal(CommandTypes.None, commandComponent.CommandTypes);
        }
    }

    [Fact]
    public void Update_ShouldDoNothing_WhenPlayerEntityIsNotFound()
    {
        // Arrange
        var inputSystem = new InputSystem();

        // Symulacja wejścia użytkownika
        var input = "w";
        using(var consoleInput = new StringReader(input))
        {
            Console.SetIn(consoleInput);

            // Act
            inputSystem.Update();

            // Assert
            // Nie ma encji Player, więc nic się nie powinno wydarzyć
        }
    }

    [Fact]
    public void Update_ShouldDoNothing_WhenCommandComponentIsNotFound()
    {
        // Arrange
        var inputSystem = new InputSystem();
        var player = new Entity();


        // Symulacja wejścia użytkownika
        var input = "w";
        using(var consoleInput = new StringReader(input))
        {
            Console.SetIn(consoleInput);

            // Act
            inputSystem.Update();

            // Assert
            // Brak CommandComponent, więc nic się nie powinno wydarzyć
        }
    }

    [Fact]
    public void Update_ShouldHandleEmptyInput()
    {
        // Arrange
        var inputSystem = new InputSystem();
        var player = new Entity();
        player.AddComponent(new CommandComponent());


        // Symulacja pustego wejścia użytkownika
        var input = "";
        using(var consoleInput = new StringReader(input))
        {
            Console.SetIn(consoleInput);

            // Act
            inputSystem.Update();

            // Assert
            var commandComponent = player.GetComponent<CommandComponent>();
            Assert.NotNull(commandComponent);
            Assert.Equal(CommandTypes.None, commandComponent.CommandTypes);
        }
    }
}
