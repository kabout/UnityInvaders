using System;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Controllers
{
    public class DifficultController : IDifficultController
    {
        #region Methods

        public int GetNumberCellsOfDefenses(IMap map, int sizeDefense, DifficultLevel difficultLevel)
        {
            int numCellsDefense = sizeDefense * sizeDefense;
            int numDefenses = (map.Width * map.Height) / (4 * numCellsDefense);

            switch (difficultLevel)
            {
                case DifficultLevel.VeryEasy: return (int)(numDefenses * 0.1) * numCellsDefense;
                case DifficultLevel.Easy: return (int)(numDefenses * 0.3) * numCellsDefense;
                case DifficultLevel.Normal: return (int)(numDefenses * 0.4) * numCellsDefense;
                case DifficultLevel.Difficult: return (int)(numDefenses * 0.5) * numCellsDefense;
                case DifficultLevel.VeryDifficult: return (int)(numDefenses * 0.6) * numCellsDefense;
                case DifficultLevel.God: return (int)(numDefenses * 0.7) * numCellsDefense;
                default: return 0;
            }
        }

        public int GetNumberCellsOfObstacles(IMap map, DifficultLevel difficultLevel)
        {
            int numCells = (map.Width * map.Height) / 4;

            switch (difficultLevel)
            {
                case DifficultLevel.VeryEasy: return (int)(numCells * 0.2);
                case DifficultLevel.Easy: return (int)(numCells * 0.4);
                case DifficultLevel.Normal: return (int)(numCells * 0.5);
                case DifficultLevel.Difficult: return (int)(numCells * 0.6);
                case DifficultLevel.VeryDifficult: return (int)(numCells * 0.8);
                case DifficultLevel.God: return numCells;
                default: return 0;
            }
        }

        public DamageType GetDamageType(DifficultLevel difficulLevel)
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

            switch (random.Next(minLevel, maxLevel))
            {
                case 0: return DamageType.Low;
                case 1: return DamageType.Medium;
                case 2: return DamageType.High;
                default: return DamageType.Low;
            }
        }

        public LevelDefense GetLevelDefense(DifficultLevel difficulLevel)
        {
            int minLevel = 0;
            int maxLevel = 0;

            switch (difficulLevel)
            {
                case DifficultLevel.VeryEasy:
                    {
                        minLevel = 0;
                        maxLevel = 3;
                    }
                    break;
                case DifficultLevel.Easy:
                    {
                        minLevel = 1;
                        maxLevel = 4;
                    }
                    break;
                case DifficultLevel.Normal:
                    {
                        minLevel = 2;
                        maxLevel = 5;
                    }
                    break;
                case DifficultLevel.Difficult:
                    {
                        minLevel = 4;
                        maxLevel = 7;
                    }
                    break;
                case DifficultLevel.VeryDifficult:
                    {
                        minLevel = 5;
                        maxLevel = 9;
                    }
                    break;
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

        public int GetNumbersOfUcosForDefenses(DifficultLevel difficultLevel)
        {
            switch(difficultLevel)
            {
                case DifficultLevel.VeryEasy: return 10000;
                case DifficultLevel.Easy: return 8000;
                case DifficultLevel.Normal: return 7000;
                case DifficultLevel.Difficult: return 5000;
                case DifficultLevel.VeryDifficult: return 4000;
                case DifficultLevel.God: return 2000;                
            }

            return 0;
        }

        public int GetNumbersOfUcosForAliens(DifficultLevel difficultLevel)
        {
            switch (difficultLevel)
            {
                case DifficultLevel.VeryEasy: return 5000;
                case DifficultLevel.Easy: return 3000;
                case DifficultLevel.Normal: return 2000;
                case DifficultLevel.Difficult: return 1000;
                case DifficultLevel.VeryDifficult: return 800;
                case DifficultLevel.God: return 500;
                default: return 0;
            }
        }

        public int GetDefenseDamage(LevelDefense levelDefense)
        {
            switch(levelDefense)
            {
                case LevelDefense.Tragic: return 5;
                case LevelDefense.Poor: return 10;
                case LevelDefense.Weak: return 15;
                case LevelDefense.Good: return 20;
                case LevelDefense.Excellent: return 25;
                case LevelDefense.Formidable: return 30;
                case LevelDefense.Incredible: return 35;
                case LevelDefense.Brilliant: return 40;
                case LevelDefense.Unearthly: return 45;
                case LevelDefense.Divine: return 50;
                default: return 0;
            }
        }

        #endregion
    }
}
