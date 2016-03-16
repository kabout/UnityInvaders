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
            Defense defense = new Defense(Constants.DEFENSE_HEALTH, Constants.DEFENSE_SIZE, Constants.DEFENSE_RANGE_DEFAULT,
                LevelDefense.Incredible, DamageType.Medium, position);

            Assert.IsTrue(defense.Level == LevelDefense.Incredible);
            Assert.IsTrue(defense.Damage == DamageType.Medium);
            Assert.AreEqual(defense.Position, position);
            Assert.IsTrue(defense.Range == Constants.DEFENSE_RANGE_DEFAULT + (int)defense.Level);
            Assert.IsTrue(defense.Width == Constants.DEFENSE_SIZE && defense.Height == Constants.DEFENSE_SIZE);
            Assert.IsTrue(defense.Health == Constants.DEFENSE_HEALTH);
        }

        [TestMethod]
        public void Defense_Take_Damage()
        {
            Position position = new Position(0, 1);
            Defense defense = new Defense(Constants.DEFENSE_HEALTH, Constants.DEFENSE_SIZE, Constants.DEFENSE_RANGE_DEFAULT, 
                LevelDefense.Incredible, DamageType.Medium, position);

            int health = defense.Health;
            defense.TakeDamage(DamageType.High);

            Assert.IsTrue(health == defense.Health + (int)DamageType.High);
        }

        [TestMethod]
        public void Defense_Take_Damage_More_Than_Health()
        {
            Position position = new Position(0, 1);
            Defense defense = new Defense(20, Constants.DEFENSE_SIZE, Constants.DEFENSE_RANGE_DEFAULT, 
                LevelDefense.Incredible, DamageType.Medium, position);
            
            defense.TakeDamage(DamageType.High);

            Assert.IsTrue(defense.Health == 0);
        }
    }
}
