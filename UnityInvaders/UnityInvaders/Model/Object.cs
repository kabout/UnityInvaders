using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Entity : IEntity
    {
        #region Properties

        public int Id
        {
            get; private set;
        }

        public Position Position
        {
            get; protected set;
        }

        public int Radius
        {
            get; private set;
        }

        #endregion

        #region Constructors

        public Entity (int id, Position position, int radius)
        {
            Id = id;
            Position = position;
            Radius = radius;
        }

        #endregion
    }
}
