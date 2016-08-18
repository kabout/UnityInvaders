using UnityEngine;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class UnityDefense: MonoBehaviour
    {
        #region Fields
        
        public IDefense defense;

        #endregion

        #region Properties

        public int Damage { get { return defense.Damage; } }

        public int Range { get { return defense.Range; } }

        public int Health { get { return defense.Health; } }

        public int Radius
        {
            get { return defense.Radius; }
        }

        public Position Position
        {
            get { return new Position((int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.z); }
        }

        public int Cost { get { return defense.Cost; } }

        public int Dispersion { get { return defense.Dispersion; } }

        public float AttacksPerSecond { get { return defense.AttacksPerSecond; } }

        public int Type { get { return defense.Type; } }

        #endregion

        #region Methods

        public void ChangePosition(Position position)
        {
            gameObject.transform.localScale += new Vector3(position.X, 0, position.Y);
        }

        public void TakeDamage(int damage)
        {
            defense.TakeDamage(damage);
        }

        public bool IsAlive()
        {
            return defense.IsAlive();
        }

        #endregion
    }
}