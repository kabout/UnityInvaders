using System.Collections.Generic;

namespace UnityInvaders
{
    public interface IMap
    {
        int Width { get; }
        int Height { get; }
        List<IObstacle> Obstacles { get; }
    }
}
