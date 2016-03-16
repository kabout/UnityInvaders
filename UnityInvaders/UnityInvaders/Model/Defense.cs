using System;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Defense : IDefense
    {
        #region Properties

        public DamageType Damage { get; private set; }

        public LevelDefense Level { get; private set; }

        public Position Position { get; private set; }

        public int Range { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int Health { get; private set; }

        #endregion

        #region Constructors

        public Defense(int health, int defenseSize, LevelDefense level, DamageType damage, Position position)
        {
            Health = health;
            Width = defenseSize;
            Height = defenseSize;
            Level = level;
            Damage = damage;
            Position = position;
            Range = (int)level;
        }

        #endregion

        #region Methods

        public void TakeDamage(DamageType damage)
        {
            int damageNumeric = (int)damage;

            if (damageNumeric > Health)
                Health = 0;
            else
                Health -= damageNumeric;
        }

        public void ChangePosition(Position position)
        {
            Position = position;
        }

        #endregion
    }
}
