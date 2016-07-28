using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvadersTests.Model
{
    [TestClass]
    public class UTAlien
    {
        [TestMethod]
        public void Create_Alien()
        {
            Position position = new Position(0, 1);
            IAlien alien = new Alien(Constants.ALIEN_HEALTH, Constants.ALIEN_SIZE, LevelAlien.Incredible, 50, position);

            Assert.IsTrue(alien.Level == LevelAlien.Incredible);
            Assert.IsTrue(alien.Damage == 50);
            Assert.AreEqual(alien.Position, position);
            Assert.IsTrue(alien.Range == 6);
            Assert.IsTrue(alien.Width == Constants.ALIEN_SIZE && alien.Height == Constants.ALIEN_SIZE);
            Assert.IsTrue(alien.Health == Constants.ALIEN_HEALTH);
        }

        [TestMethod]
        public void Alien_Take_Damage()
        {
            Position position = new Position(0, 1);
            IAlien alien = new Alien(Constants.ALIEN_HEALTH, Constants.ALIEN_SIZE,
                LevelAlien.Incredible, 20, position);

            int health = alien.Health;
            alien.TakeDamage(90);

            Assert.IsTrue(health == alien.Health + 90);
        }

        [TestMethod]
        public void Defense_Take_Damage_More_Than_Health()
        {
            Position position = new Position(0, 1);
            IAlien alien = new Alien(20, Constants.ALIEN_SIZE,
                LevelAlien.Incredible, 20, position);

            alien.TakeDamage(30);

            Assert.IsTrue(alien.Health == 0);
        }

        [TestMethod]
        public void Defense_Change_Position()
        {
            Position position = new Position(0, 1);
            IAlien alien = new Alien(Constants.ALIEN_HEALTH, Constants.ALIEN_SIZE,
                LevelAlien.Incredible, 20, position);

            alien.ChangePosition(new Position(1, 0));

            Assert.IsTrue(alien.Position.X == 1);
            Assert.IsTrue(alien.Position.Y == 0);
        }
    }
}
