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
            Defense defense = new Defense(1, 0, Constants.DEFENSE_HEALTH, Constants.DEFAULT_DEFENSE_RADIO, Constants.DEFAULT_DEFENSE_DAMAGE,
                Constants.DEFAULT_DEFENSE_RANGE, Constants.DEFAULT_DEFENSE_DISPERSION, Constants.DEFAULT_DEFENSE_ATTACKS_PER_SECOND,
                Constants.DEFAULT_DEFENSE_COST, position);

            Assert.IsTrue(defense.Type == 0);
            Assert.IsTrue(defense.Health == Constants.DEFENSE_HEALTH);
            Assert.IsTrue(defense.Radius == Constants.DEFAULT_DEFENSE_RADIO);
            Assert.IsTrue(defense.Damage == Constants.DEFAULT_DEFENSE_DAMAGE);
            Assert.IsTrue(defense.Range == Constants.DEFAULT_DEFENSE_RANGE);
            Assert.IsTrue(defense.Dispersion == Constants.DEFAULT_DEFENSE_DISPERSION);
            Assert.IsTrue(defense.AttacksPerSecond == Constants.DEFAULT_DEFENSE_ATTACKS_PER_SECOND);
            Assert.IsTrue(defense.Cost == Constants.DEFAULT_DEFENSE_COST);
            Assert.AreEqual(defense.Position, position);
        }

        [TestMethod]
        public void Defense_Take_Damage()
        {
            Position position = new Position(0, 1);
            Defense defense = new Defense(1, 0, Constants.DEFENSE_HEALTH, Constants.DEFAULT_DEFENSE_RADIO, Constants.DEFAULT_DEFENSE_DAMAGE,
                Constants.DEFAULT_DEFENSE_RANGE, Constants.DEFAULT_DEFENSE_DISPERSION, Constants.DEFAULT_DEFENSE_ATTACKS_PER_SECOND,
                Constants.DEFAULT_DEFENSE_COST, position);

            int health = defense.Health;
            defense.TakeDamage(90);

            Assert.IsTrue(health == defense.Health + 90);
        }

        [TestMethod]
        public void Defense_Take_Damage_More_Than_Health()
        {
            Position position = new Position(0, 1);
            Defense defense = new Defense(1, 0, 20, Constants.DEFAULT_DEFENSE_RADIO, Constants.DEFAULT_DEFENSE_DAMAGE,
                Constants.DEFAULT_DEFENSE_RANGE, Constants.DEFAULT_DEFENSE_DISPERSION, Constants.DEFAULT_DEFENSE_ATTACKS_PER_SECOND,
                Constants.DEFAULT_DEFENSE_COST, position);

            defense.TakeDamage(30);

            Assert.IsTrue(defense.Health == 0);
        }

        [TestMethod]
        public void Defense_Change_Position()
        {
            Position position = new Position(0, 1);
            Defense defense = new Defense(1, 0, Constants.DEFENSE_HEALTH, Constants.DEFAULT_DEFENSE_RADIO, Constants.DEFAULT_DEFENSE_DAMAGE,
                Constants.DEFAULT_DEFENSE_RANGE, Constants.DEFAULT_DEFENSE_DISPERSION, Constants.DEFAULT_DEFENSE_ATTACKS_PER_SECOND,
                Constants.DEFAULT_DEFENSE_COST, position);

            defense.ChangePosition(new Position(1, 0));

            Assert.IsTrue(defense.Position.X == 1);
            Assert.IsTrue(defense.Position.Y == 0);
        }

    }
}
