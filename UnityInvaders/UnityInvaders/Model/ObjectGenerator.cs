using System;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class ObjectGenerator : IObjectGenerator
    {
        public IDefense GenerateDefense(int sizeDefense, IMap map)
        {
            throw new NotImplementedException();
        }

        public IObstacle GenerateObstacle(int numCellsOfObstacles, IMap map)
        {
            Random random = new Random();
            int numCellsObstacle = random.Next(0, numCellsOfObstacles);
            int width = random.Next(0, numCellsObstacle);
            int height = 1;

            numCellsObstacle -= width;

            while (numCellsObstacle > 0)
            {
                numCellsObstacle -= width;
                height++;
            }

            Position position = new Position(random.Next(0 + width, map.Width - width), random.Next(0 + height, map.Height - height));

            while(!map.IsValidPosition(position, width, height))
                position = new Position(random.Next(0 + width, map.Width - width), random.Next(0 + height, map.Height - height));

            return new Obstacle(width, height, position);
        }
    }
}
