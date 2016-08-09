using System;
using System.Collections.Generic;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;
using UnityInvaders.Utils;

namespace UnityInvaders.Managers
{
    public class ObjectManager : IObjectManager
    {
        #region Fields

        IDifficultController difficultController;
        private static int nextDefenseId = 1;
        private static int nextObstacleId = 1;

        #endregion

        public ObjectManager(IDifficultController difficultController)
        {
            RandomManager.Seed = DateTime.Now.Millisecond;
            this.difficultController = difficultController;
        }

        public IDefense GenerateDefense(IMap map, int radiusDefense)
        {
            IList<Position> availablePositions = map.GetFreePositionsForDefense();

            if (availablePositions.Count == 0)
                return null;
            
            int index = RandomManager.GetRandomNumber(0, availablePositions.Count);
            Position position = availablePositions[index];

            IDefense defense = new Defense(nextDefenseId, 0, Constants.DEFENSE_HEALTH, radiusDefense, Constants.DEFAULT_DEFENSE_DAMAGE,
                Constants.DEFAULT_DEFENSE_RANGE, Constants.DEFAULT_DEFENSE_DISPERSION, Constants.DEFAULT_DEFENSE_ATTACKS_PER_SECOND,
                Constants.DEFAULT_DEFENSE_COST, position);

            nextDefenseId++;

            return defense;
        }
        
        public IObstacle GenerateObstacle(IMap map, int minRadius, int maxRadius)
        {
            //int minRadius = difficultController.GetMinRadiusOfObstacle();
            //int maxRadius = difficultController.GetMaxRadiusOfObstacle();

            int radius = RandomManager.GetRandomNumber(minRadius, maxRadius);

            IList<Position> availablePositions = map.GetFreePositionsForObstacle(radius);

            if (availablePositions.Count == 0)
                return null;

            int index = RandomManager.GetRandomNumber(0, availablePositions.Count);
            Position position = availablePositions[index];

            IObstacle obstacle = new Obstacle(nextObstacleId, radius, position);

            nextObstacleId++;

            return obstacle;
        }
    }
}
