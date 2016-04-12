using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Controllers
{
    public class MapController : IMapController
    {
        #region Fields

        IDifficultController difficultController;
        IObjectManager objectManager;

        #endregion

        #region Constructors

        public MapController(IDifficultController difficultController, IObjectManager objectManager)
        {
            this.difficultController = difficultController;
            this.objectManager = objectManager;
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
            PlaceDefenses(map, difficultLevel);
        }

        private void PlaceDefenses(IMap map, DifficultLevel difficultLevel)
        {
            int numCellsOfDefenses = difficultController.GetNumberCellsOfDefenses(map, Constants.DEFENSE_SIZE, difficultLevel);

            int numDefenses = numCellsOfDefenses / (Constants.DEFENSE_SIZE * Constants.DEFENSE_SIZE);

            while (numDefenses > 0)
            {
                IDefense defense = objectManager.GenerateDefense(Constants.DEFENSE_SIZE, difficultLevel, map);
                map.AddDefense(defense);
            }
        }

        private void PlaceObstacles(IMap map, DifficultLevel difficultLevel)
        {
            int numCellsOfObstacles = difficultController.GetNumberCellsOfObstacles(map, difficultLevel);

            while (numCellsOfObstacles > 0)
            {
                IObstacle obstacle = objectManager.GenerateObstacle(numCellsOfObstacles, map);
                map.AddObstacle(obstacle);

                numCellsOfObstacles -= obstacle.Width * obstacle.Height;
            }
        }

        #endregion
    }
}
