using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityInvaders
{
    public interface IObstacle
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Position Position { get; private set; }
    }
}
