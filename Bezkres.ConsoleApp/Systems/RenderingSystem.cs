using Bezkres.ConsoleApp.Components;
using Bezkres.ConsoleApp.Entities;
using Bezkres.ConsoleApp.Systems.Interfaces;

namespace Bezkres.ConsoleApp.Systems;

public class RenderingSystem : IRegisterSystem
{
    readonly List<Entity> _entities = new List<Entity>();
    public void RegisterEntity(Entity entity)
    {

        if(!entity.HasComponent<LoggerComponent>())
        {
            return;
        }

        if(!entity.HasComponent<LocalizationsComponent>())
        {
            return;
        }

        if(!entity.HasComponent<PositionComponent>())
        {
            return;
        }

        _entities.Add(entity);
    }

    public void UnregisterEntity(Entity entity)
    {
        _entities.Remove(entity);
    }

    public void Draw()
    {
        foreach(var entity in _entities)
        {
            var localizationsComponent = entity.GetComponent<LocalizationsComponent>();
            ArgumentNullException.ThrowIfNull(localizationsComponent);

            var positionComponent = entity.GetComponent<PositionComponent>();
            ArgumentNullException.ThrowIfNull(positionComponent);

            var localization = localizationsComponent.Localizations.FirstOrDefault(l => l.X == positionComponent.X && l.Y == positionComponent.Y);
            ArgumentNullException.ThrowIfNull(localization);

            var logComponent = entity.GetComponent<LoggerComponent>();
            ArgumentNullException.ThrowIfNull(logComponent);

            if(logComponent.Logger.Any())
            {
                foreach(var log in logComponent.Logger)
                {
                    Console.ForegroundColor = log.Color;
                    Console.WriteLine(log.Text);
                }

                logComponent.Logger.Clear();
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(localization.Name);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(localization.Description);
        }
    }
}
