using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityInvaders.Model;

namespace UnityInvadersTests
{
    [TestClass]
    public class UTObstacle
    {
        [TestMethod]
        public void Create_Obstacle()
        {
            Position position = new Position(0,1);
            Obstacle obstacle = new Obstacle(10, 20, position);

            Assert.IsTrue(obstacle.Width == 10);
            Assert.IsTrue(obstacle.Height == 20);
            Assert.IsTrue(obstacle.Position.X == 0);
            Assert.IsTrue(obstacle.Position.Y == 1);
        }
    }
}
