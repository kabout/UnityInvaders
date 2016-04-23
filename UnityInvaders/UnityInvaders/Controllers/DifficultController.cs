using System;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;
using UnityInvaders.Utils;

namespace UnityInvaders.Controllers
{
    public class DifficultController : IDifficultController
    {
        #region Fields

        DifficultLevel difficultLevel;

        #endregion

        #region Constructors

        public DifficultController(DifficultLevel difficultLevel)
        {
            this.difficultLevel = difficultLevel;
        }

        #endregion

        #region Methods

        public int GetNumberOfDefenses(IMap map)
        {
            int numCellsDefense = Constants.DEFENSE_SIZE * Constants.DEFENSE_SIZE;
            int numDefenses = (map.Width * map.Height) / (4 * numCellsDefense);

            switch (difficultLevel)
            {
                case DifficultLevel.VeryEasy: return (int)(numDefenses * 0.1);
                case DifficultLevel.Easy: return (int)(numDefenses * 0.3);
                case DifficultLevel.Normal: return (int)(numDefenses * 0.4);
                case DifficultLevel.Difficult: return (int)(numDefenses * 0.5);
                case DifficultLevel.VeryDifficult: return (int)(numDefenses * 0.6);
                case DifficultLevel.God: return (int)(numDefenses * 0.7);
                default: return 0;
            }
        }

        public int GetNumberOfObstacles(IMap map)
        {
            int numObstacles = (map.Width * map.Height) / 100;

            switch (difficultLevel)
            {
                case DifficultLevel.VeryEasy: return (int)(numObstacles * 0.1);
                case DifficultLevel.Easy: return (int)(numObstacles * 0.3);
                case DifficultLevel.Normal: return (int)(numObstacles * 0.5);
                case DifficultLevel.Difficult: return (int)(numObstacles * 0.6);
                case DifficultLevel.VeryDifficult: return (int)(numObstacles * 0.8);
                case DifficultLevel.God: return numObstacles;
                default: return 0;
            }
        }

        public LevelDefense GetLevelDefense()
        {
            int minLevel = 0;
            int maxLevel = 0;

            switch (difficultLevel)
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

            RandomManager.Seed = DateTime.Now.Millisecond;
            return (LevelDefense)RandomManager.GetRandomNumber(minLevel, maxLevel);
        }

        public int GetNumbersOfUcosForDefenses()
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

        public int GetNumbersOfUcosForAliens()
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

        public int GetMinSizeObstacle()
        {
            switch (difficultLevel)
            {
                case DifficultLevel.VeryEasy: return 6;
                case DifficultLevel.Easy: return 5;
                case DifficultLevel.Normal: return 4;
                case DifficultLevel.Difficult: return 3;
                case DifficultLevel.VeryDifficult: return 2;
                case DifficultLevel.God: return 2;
                default: return 0;
            }
        }

        public int GetMaxSizeObstacle()
        {
            switch (difficultLevel)
            {
                case DifficultLevel.VeryEasy: return 17;
                case DifficultLevel.Easy: return 15;
                case DifficultLevel.Normal: return 13;
                case DifficultLevel.Difficult: return 10;
                case DifficultLevel.VeryDifficult: return 7;
                case DifficultLevel.God: return 5;
                default: return 0;
            }
        }

        #endregion
    }
}
