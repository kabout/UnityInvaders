using System;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Obstacle : Entity, IObstacle
    {
        #region Properties

        #endregion

        #region Constructors

        public Obstacle(int id, int radius, Position position) : 
            base(id, position, radius)
        {
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
