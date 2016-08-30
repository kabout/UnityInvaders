using UnityEngine;
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

        public Vector3 Position
        {
            get; private set;
        }

        public float Radius
        {
            get; private set;
        }

        #endregion

        #region Constructors

        public Entity(int id, Vector3 position, int radius)
        {
            Id = id;
            Position = position;
            Radius = radius;

        }

        #endregion
    }
}
