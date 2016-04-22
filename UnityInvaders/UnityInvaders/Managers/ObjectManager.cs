using System;
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

        public IDefense GenerateDefense(int sizeDefense, DifficultLevel difficulLevel, IMap map, int maxUcos = int.MaxValue)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            int maxHeight = map.Height - sizeDefense;
            int maxWidth = map.Width - sizeDefense;
            int minHeight = 0 + sizeDefense;
            int minWidth = minHeight;

            Position position = new Position(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
            LevelDefense levelDefense = difficultController.GetLevelDefense(difficulLevel);
            IDefense defense = new Defense(Constants.DEFENSE_HEALTH, sizeDefense, levelDefense,
                difficultController.GetDefenseDamage(levelDefense), position);

            while (!map.IsValidPosition(defense))
            {
                position = new Position(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
                defense.ChangePosition(position);
            }

            return defense;
        }

        /// <summary>
        /// Hay que comprobar que los obstaculos sean como mínimo de 1x1.
        /// </summary>
        /// <param name="numCellsOfObstacles"></param>
        /// <param name="map"></param>
        /// <param name="maxUcos"></param>
        /// <returns></returns>
        public IObstacle GenerateObstacle(int numCellsOfObstacles, IMap map, int maxUcos = int.MaxValue)
        {
            int numCellsObstacle = RandomManager.GetRandomNumber(1, Math.Min(map.Width / 2, numCellsOfObstacles));

            if (numCellsObstacle == 1)
                return null;

            int width = RandomManager.GetRandomNumber(1, numCellsObstacle / 2);
            int height = 1;

            numCellsObstacle -= width;

            while (numCellsObstacle > 0 && height < map.Height / 2)
            {
                numCellsObstacle -= width;
                height++;
            }

            int maxHeight = map.Height - height;
            int maxWidth = map.Width - width;
            int minHeight = 0 + height;
            int minWidth = 0 + width;

            Position position = new Position(RandomManager.GetRandomNumber(minWidth, maxWidth), RandomManager.GetRandomNumber(minHeight, maxHeight));
            IObstacle obstacle = new Obstacle(width, height, position);

            while (!map.IsValidPosition(obstacle))
            {
                position = new Position(RandomManager.GetRandomNumber(minWidth, maxWidth), RandomManager.GetRandomNumber(minHeight, maxHeight));
                obstacle.ChangePosition(position);
            }

            return obstacle;
        }
    }
}
