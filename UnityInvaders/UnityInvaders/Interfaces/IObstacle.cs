namespace UnityInvaders
{
    public interface IObstacle
    {
        uint Width { get; }
        uint Height { get; }
        Position Position { get; }
    }
}
