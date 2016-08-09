using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvadersTests.Model
{
    [TestClass]
    public class UTObject
    {
        [TestMethod]
        public void Create_Object()
        {
            Position position = new Position(0, 1);
            IEntity object1 = new Entity(1, position, 10);

            Assert.IsTrue(object1.Id == 1);
            Assert.AreEqual(object1.Position, position);
            Assert.IsTrue(object1.Radius == 10);
        }
    }
}
