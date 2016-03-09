using System.Collections.Generic;

namespace UnityInvaders
{
    public interface IMap
    {
        uint Width { get; }
        uint Height { get; }
        List<IObstacle> Obstacles { get; }
    }
}
