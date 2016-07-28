using UnityEngine;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class UnityAlien
    {
        #region Fields

        public GameObject alien;
        private IAlien alienObject;

        #endregion

        #region Properties

        public int Damage { get { return alienObject.Damage; } }

        public LevelAlien Level { get { return alienObject.Level; } }

        public int Range { get { return alienObject.Range; } }

        public int Health { get { return alienObject.Health; } }

        public int Width
        {
            get { return (int)alien.transform.localScale.x; }
        }

        public int Height
        {
            get { return (int)alien.transform.localScale.z; }
        }

        public Position Position
        {
            get { return new Position((int)alien.transform.localPosition.x, (int)alien.transform.localPosition.z); }
        }

        #endregion

        #region Constructors

        public UnityAlien(GameObject alien, IAlien alienObject)
        {
            this.alien = alien;
            this.alienObject = alienObject;
        }

        #endregion

        #region Methods

        public void ChangePosition(Position position)
        {
            alien.transform.localScale += new Vector3(position.X, 0, position.Y);
        }

        public void TakeDamage(int damage)
        {
            alienObject.TakeDamage(damage);
        }

        public bool IsAlive()
        {
            return alienObject.IsAlive();
        }

        #endregion
    }
}