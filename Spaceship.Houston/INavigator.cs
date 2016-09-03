namespace Autopilot
{
    public interface INavigator
    {
        Coordinate[] Route(Coordinate spaceshipPosition, Coordinate[] destinations);
    }
}