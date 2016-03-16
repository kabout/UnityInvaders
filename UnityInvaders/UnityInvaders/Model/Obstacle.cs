using System;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Obstacle : IObstacle
    {
        #region Properties

        public int Width
        {
            get;
            private set;
        }

        public int Height
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

        public Obstacle(int width, int height, Position position)
        {
            Width = width;
            Height = height;
            Position = position;
        }

        #endregion

        #region Methods

        public void ChangePosition(Position position)
        {
            Position = position;
        }

        #endregion
    }
}
