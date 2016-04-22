using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Controllers
{
    public class MapController : IMapController
    {
        #region Fields

        IDifficultController difficultController;
        IObjectManager objectManager;
        IDefenseController defenseController;

        #endregion

        #region Constructors

        public MapController(IDefenseController defenseController, IDifficultController difficultController, IObjectManager objectManager)
        {
            this.difficultController = difficultController;
            this.objectManager = objectManager;
            this.defenseController = defenseController;
        }

        #endregion

        #region Methods

        public IMap GetEmptyMap(int width, int height)
        {
            return new Map(width, height);
        }

        public void InitMap(IMap map, DifficultLevel difficultLevel)
        {
            PlaceObstacles(map, difficultLevel);
            defenseController.PlaceDefenses(map, difficultLevel);
        }       

        private void PlaceObstacles(IMap map, DifficultLevel difficultLevel)
        {
            int numCellsOfObstacles = difficultController.GetNumberCellsOfObstacles(map, difficultLevel);

            while (numCellsOfObstacles > 0)
            {
                IObstacle obstacle = objectManager.GenerateObstacle(numCellsOfObstacles, map);

                if (obstacle == null)
                    return;

                map.AddObstacle(obstacle);

                numCellsOfObstacles -= obstacle.Width * obstacle.Height;
            }
        }

        #endregion
    }
}
