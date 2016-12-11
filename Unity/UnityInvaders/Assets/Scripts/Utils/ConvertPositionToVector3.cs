using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class ConvertPosition
    {
        public static Vector3 Convert(IPosition position)
        {
            return new Vector3(position.X, position.Y, position.Z);
        }

        public static IPosition Convert(Vector3 position)
        {
            return new Position(position.x, position.y, position.z);
        }

        public static List<Vector3> Convert(List<IPosition> positions)
        {
            return positions.ConvertAll(x => Convert(x));
        }
    }
}
