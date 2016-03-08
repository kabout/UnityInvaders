using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityInvaders
{
    public interface IMap
    {
        int[,] Map { get; private set; }
        int Width { get; private set; }
        int Height { get; private set; }
        List<IObstacle> Obstacles { get; private set; }
    }
}
