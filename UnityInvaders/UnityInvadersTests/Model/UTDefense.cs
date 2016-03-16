using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Model;

namespace UnityInvadersTests.Model
{
    [TestClass]
    public class UTDefense
    {
        [TestMethod]
        public void Create_Defense()
        {
            Position position = new Position(0, 1);
            Defense defense = new Defense(Constants.DEFENSE_HEALTH, Constants.DEFENSE_SIZE,
                LevelDefense.Incredible, 50, position);

            Assert.IsTrue(defense.Level == LevelDefense.Incredible);
            Assert.IsTrue(defense.Damage == 50);
            Assert.AreEqual(defense.Position, position);
            Assert.IsTrue(defense.Range == 6);
            Assert.IsTrue(defense.Width == Constants.DEFENSE_SIZE && defense.Height == Constants.DEFENSE_SIZE);
            Assert.IsTrue(defense.Health == Constants.DEFENSE_HEALTH);
        }

        [TestMethod]
        public void Defense_Take_Damage()
        {
            Position position = new Position(0, 1);
            Defense defense = new Defense(Constants.DEFENSE_HEALTH, Constants.DEFENSE_SIZE, 
                LevelDefense.Incredible, 20, position);

            int health = defense.Health;
            defense.TakeDamage(90);

            Assert.IsTrue(health == defense.Health + 90);
        }

        [TestMethod]
        public void Defense_Take_Damage_More_Than_Health()
        {
            Position position = new Position(0, 1);
            Defense defense = new Defense(20, Constants.DEFENSE_SIZE, 
                LevelDefense.Incredible, 20, position);
            
            defense.TakeDamage(30);

            Assert.IsTrue(defense.Health == 0);
        }
    }
}
