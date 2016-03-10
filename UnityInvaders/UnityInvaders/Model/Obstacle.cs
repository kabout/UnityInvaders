using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Obstacle : IObstacle
    {
        #region Properties

        public uint Width
        {
            get;
            private set;
        }

        public uint Height
        {
            get;
            private set;
        }

        public Position Position
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public Obstacle(uint width, uint height, Position position)
        {
            Width = width;
            Height = height;
            Position = position;
        }

        #endregion
    }
}
