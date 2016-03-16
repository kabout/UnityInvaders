using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Controllers
{
    public class MapController : IMapController
    {
        #region Fields

        IDifficultController difficultController;
        IObjectGenerator objectGenerator;

        #endregion

        #region Constructors

        public MapController(IDifficultController difficultController, IObjectGenerator objectGenerator)
        {
            this.difficultController = difficultController;
            this.objectGenerator = objectGenerator;
        }

        #endregion

        #region Methods

        public IMap GetEmptyMap(int width, int height)
        {
            return new Map(width, height);
        }

        public void InitMap(IMap map, DifficultLevel difficultLevel)
        {
            InitObstacles(map, difficultLevel);
            InitDefenses(map, difficultLevel);
        }

        private void InitDefenses(IMap map, DifficultLevel difficultLevel)
        {
            int numCellsOfDefenses = difficultController.GetNumberCellsOfDefenses(map, Constants.DEFENSE_SIZE, difficultLevel);

            int numDefenses = numCellsOfDefenses / (Constants.DEFENSE_SIZE * Constants.DEFENSE_SIZE);

            while (numDefenses > 0)
            {
                IDefense defense = objectGenerator.GenerateDefense(Constants.DEFENSE_SIZE, map);
                map.AddDefense(defense);
            }
        }

        private void InitObstacles(IMap map, DifficultLevel difficultLevel)
        {
            int numCellsOfObstacles = difficultController.GetNumberCellsOfObstacles(map, difficultLevel);

            while (numCellsOfObstacles > 0)
            {
                IObstacle obstacle = objectGenerator.GenerateObstacle(numCellsOfObstacles, map);
                map.AddObstacle(obstacle);

                numCellsOfObstacles -= obstacle.Width * obstacle.Height;
            }
        }

        #endregion
    }
}
