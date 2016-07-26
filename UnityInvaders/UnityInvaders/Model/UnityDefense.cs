using UnityEngine;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class UnityDefense
    {
        #region Fields

        public GameObject defense;
        private IDefense defenseObject;

        #endregion

        #region Properties

        public int Damage { get { return defenseObject.Damage; } }

        public LevelDefense Level { get { return defenseObject.Level; } }

        public int Range { get { return defenseObject.Range; } }

        public int Health { get { return defenseObject.Health; } }

        public int Width
        {
            get { return (int)defense.transform.localScale.x; }
        }

        public int Height
        {
            get { return (int)defense.transform.localScale.z; }
        }

        public Position Position
        {
            get { return new Position((int)defense.transform.localPosition.x, (int)defense.transform.localPosition.z); }
        }

        #endregion

        #region Constructors

        public UnityDefense(GameObject defense, IDefense defenseObject)
        {
            this.defense = defense;
            this.defenseObject = defenseObject;
        }

        #endregion

        #region Methods

        public void ChangePosition(Position position)
        {
            defense.transform.localScale += new Vector3(position.X, 0, position.Y);
        }

        public void TakeDamage(int damage)
        {
            defenseObject.TakeDamage(damage);
        }

        public bool IsAlive()
        {
            return defenseObject.IsAlive();
        }

        #endregion
    }
}