namespace UnityInvaders
{
    public interface IObstacle
    {
        int Width { get; }
        int Height { get; }
        Position Position { get; }
    }
}
