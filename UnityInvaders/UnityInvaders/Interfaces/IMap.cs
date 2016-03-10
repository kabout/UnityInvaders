using System.Collections.Generic;

namespace UnityInvaders.Interfaces
{
    public interface IMap
    {
        uint Width { get; }
        uint Height { get; }
        List<IObstacle> Obstacles { get; }
    }
}
