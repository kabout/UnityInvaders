using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IObstacle
    {
        uint Width { get; }
        uint Height { get; }
        Position Position { get; }
    }
}
