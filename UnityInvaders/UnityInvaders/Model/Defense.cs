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

        public uint Range { get; private set; }

        public uint Width { get; private set; }

        public uint Height { get; private set; }

        public uint Health { get; private set; }

        #endregion

        #region Constructors

        public Defense(uint health, uint defenseSize, uint defaultRange, LevelDefense level, DamageType damage, Position position)
        {
            Health = health;
            Width = defenseSize;
            Height = defenseSize;
            Level = level;
            Damage = damage;
            Position = position;
            Range = defaultRange + (uint)level;
        }

        #endregion

        #region Methods

        public void TakeDamage(DamageType damage)
        {
            uint damageNumeric = (uint)damage;

            if (damageNumeric > Health)
                Health = 0;
            else
                Health -= damageNumeric;
        }

        #endregion
    }
}
