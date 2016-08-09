using System;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Defense : Entity, IDefense
    {
        #region Fields

        private float secondsPerAttack, timeForNextAttack;

        #endregion

        #region Properties

        public int Damage { get; private set; }

        public int Range { get; private set; }

        public int Health { get; private set; }

        public int Cost { get; private set; }

        public int Dispersion { get; private set; }

        public float AttacksPerSecond { get; private set; }

        public int Type { get; private set; }

        #endregion

        #region Constructors

        public Defense(int id, int type, int health, int defenseRadius, int damage, int range, int dispersion, float attacksPerSecond, int cost, Position position) : 
            base(id, position, defenseRadius)
        {
            Type = type;
            Health = health;
            Damage = damage;
            Range = range;
            Dispersion = dispersion;
            AttacksPerSecond = attacksPerSecond;
            timeForNextAttack = 0;
            secondsPerAttack = 1 / attacksPerSecond;
            Cost = cost;
        }

        #endregion

        #region Methods

        public void TakeDamage(int damage)
        {
            if (damage > Health)
                Health = 0;
            else
                Health -= damage;
        }

        public void ChangePosition(Position position)
        {
            Position = position;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        #endregion
    }
}
