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

        #endregion

        public ObjectManager(IDifficultController difficultController)
        {
            RandomManager.Seed = DateTime.Now.Millisecond;
            this.difficultController = difficultController;
        }

        public IDefense GenerateDefense(IMap map)
        {
            IList<Position> availablePositions = map.GetFreePositionsForDefense();

            LevelDefense levelDefense = difficultController.GetLevelDefense();
            int index = RandomManager.GetRandomNumber(0, availablePositions.Count);
            Position position = availablePositions[index];

            IDefense defense = new Defense(Constants.DEFENSE_HEALTH, Constants.DEFENSE_SIZE, levelDefense,
                difficultController.GetDefenseDamage(levelDefense), position);

            return defense;
        }
        
        public IObstacle GenerateObstacle(IMap map)
        {
            int minSize = difficultController.GetMinSizeObstacle();
            int maxSize = difficultController.GetMaxSizeObstacle();

            int width = RandomManager.GetRandomNumber(minSize, maxSize);
            int height = RandomManager.GetRandomNumber(minSize, maxSize);

            IList<Position> availablePositions = map.GetFreePositionsForObstacle(width, height);
            int index = RandomManager.GetRandomNumber(0, availablePositions.Count);
            Position position = availablePositions[index];

            IObstacle obstacle = new Obstacle(width, height, position);

            return obstacle;
        }
    }
}
