using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;
using UnityInvaders.Transforms;

namespace UnityInvadersTests.Model
{
    [TestClass]
    public class UTDefenseToUnity
    {
        [TestMethod]
        public void Create_UnityDefense()
        {
            Position position = new Position(0, 1);
            Defense defense = new Defense(1, 0, Constants.DEFENSE_HEALTH, Constants.DEFAULT_DEFENSE_RADIO, Constants.DEFAULT_DEFENSE_DAMAGE,
                Constants.DEFAULT_DEFENSE_RANGE, Constants.DEFAULT_DEFENSE_DISPERSION, Constants.DEFAULT_DEFENSE_ATTACKS_PER_SECOND,
                Constants.DEFAULT_DEFENSE_COST, position);
        }
    }
}
