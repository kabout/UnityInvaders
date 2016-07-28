using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Alien : IAlien
    {
        #region Properties

        public int Damage { get; private set; }

        public LevelAlien Level { get; private set; }

        public Position Position { get; private set; }

        public int Range { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int Health { get; private set; }

        #endregion

        #region Constructors

        public Alien(int health, int alienSize, LevelAlien level, int damage, Position position)
        {
            Health = health;
            Width = alienSize;
            Height = alienSize;
            Level = level;
            Damage = damage;
            Position = position;
            Range = (int)level;
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
