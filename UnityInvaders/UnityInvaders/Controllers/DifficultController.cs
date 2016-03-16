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

        #endregion
    }
}
