using System;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class ObjectGenerator : IObjectGenerator
    {
        public IDefense GenerateDefense(int sizeDefense, DifficultLevel difficulLevel, IMap map)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            int maxHeight = map.Height - sizeDefense;
            int maxWidth = map.Width - sizeDefense;
            int minHeight = 0 + sizeDefense;
            int minWidth = minHeight;

            Position position = new Position(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
            IDefense defense = new Defense(Constants.DEFENSE_HEALTH, sizeDefense, GetLevelDefense(difficulLevel),
                GetDamageType(difficulLevel), position);

            while (!map.IsValidPosition(defense))
            {
                position = new Position(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
                defense.ChangePosition(position);
            }

            return defense;
        }

        private DamageType GetDamageType(DifficultLevel difficulLevel)
        {
            int minLevel = 0;
            int maxLevel = 0;

            switch (difficulLevel)
            {
                case DifficultLevel.VeryEasy:
                    {
                        minLevel = 0;
                        maxLevel = 0;
                    }
                    break;
                case DifficultLevel.Easy:
                    {
                        minLevel = 0;
                        maxLevel = 1;
                    }
                    break;
                case DifficultLevel.Normal:
                    {
                        minLevel = 1;
                        maxLevel = 1;
                    }
                    break;
                case DifficultLevel.Difficult:
                    {
                        minLevel = 1;
                        maxLevel = 2;
                    }
                    break;
                case DifficultLevel.VeryDifficult:
                case DifficultLevel.God:
                    {
                        minLevel = 2;
                        maxLevel = 2;
                    }
                    break;
            }

            Random random = new Random(DateTime.Now.Millisecond);

            switch(random.Next(minLevel, maxLevel))
            {
                case 0: return DamageType.Low;
                case 1: return DamageType.Medium;
                case 2: return DamageType.High;
                default: return DamageType.Low;
            }
        }

        private LevelDefense GetLevelDefense(DifficultLevel difficulLevel)
        {
            int minLevel = 0;
            int maxLevel = 0;

            switch(difficulLevel)
            {
                case DifficultLevel.VeryEasy:
                    {
                        minLevel = 0;
                        maxLevel = 3;
                    } break;
                case DifficultLevel.Easy:
                    {
                        minLevel = 1;
                        maxLevel = 4;
                    } break;
                case DifficultLevel.Normal:
                    {
                        minLevel = 2;
                        maxLevel = 5;
                    } break;
                case DifficultLevel.Difficult:
                    {
                        minLevel = 4;
                        maxLevel = 7;
                    } break;
                case DifficultLevel.VeryDifficult:
                    {
                        minLevel = 5;
                        maxLevel = 9;
                    } break;
                case DifficultLevel.God:
                    {
                        minLevel = 7;
                        maxLevel = 9;
                    }
                    break;
            }

            Random random = new Random(DateTime.Now.Millisecond);
            return (LevelDefense)random.Next(minLevel, maxLevel);
        }

        public IObstacle GenerateObstacle(int numCellsOfObstacles, IMap map)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int numCellsObstacle = random.Next(0, numCellsOfObstacles);
            int width = random.Next(0, numCellsObstacle);
            int height = 1;

            numCellsObstacle -= width;

            while (numCellsObstacle > 0)
            {
                numCellsObstacle -= width;
                height++;
            }

            int maxHeight = map.Height - height;
            int maxWidth = map.Width - width;
            int minHeight = 0 + height;
            int minWidth = 0 + width;

            Position position = new Position(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
            IObstacle obstacle = new Obstacle(width, height, position);

            while (!map.IsValidPosition(obstacle))
            {
                position = new Position(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
                obstacle.ChangePosition(position);
            }

            return obstacle;
        }
    }
}
