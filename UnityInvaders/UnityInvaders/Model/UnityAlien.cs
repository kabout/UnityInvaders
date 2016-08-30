using System;
using UnityEngine;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class UnityAlien : MonoBehaviour, IAlien
    {
        #region Fields

        public float damage;
        public int range;
        public float health;
        public int cost;
        public int id;

        #endregion

        #region Properties

        public float Damage { get { return damage; } }

        public int Range { get { return range; } }

        public float Health { get { return health; } }

        public int Width
        {
            get { return (int)transform.localScale.x; }
        }

        public int Height
        {
            get { return (int)transform.localScale.z; }
        }

        public Vector3 Position
        {
            get { return transform.position; }
        }

        public int Cost
        {
            get
            {
                return cost;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public float Radius
        {
            get
            {
                return transform.localScale.x / 2;
            }
        }

        #endregion

        #region Methods

        public void ChangePosition(Vector3 position)
        {
            transform.position = position;
        }

        public void TakeDamage(float damage)
        {
            if (damage > Health)
                health = 0;
            else
                health -= damage;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}