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

        public IMap GetEmptyMap(int size)
        {
            return new Map(size);
        }

        public void InitMap(IMap map)
        {
            PlaceObstacles(map);
            defenseController.PlaceDefenses(map);
        }       

        private void PlaceObstacles(IMap map)
        {
            int numOfObstacles = difficultController.GetNumberOfObstacles(map);

            while (numOfObstacles > 0)
            {
                IObstacle obstacle = objectManager.GenerateObstacle(map, Constants.MIN_OBSTACLE_RADIUS, Constants.MAX_OBSTACLE_RADIUS);

                if (obstacle == null)
                    return;

                map.AddObstacle(obstacle);

                numOfObstacles--;
            }
        }

        private void PlaceDefenses(IMap map)
        {
            int numDefenses = difficultController.GetNumberOfDefenses(map);

            while (numDefenses > 0)
            {
                IDefense defense = objectManager.GenerateDefense(map, Constants.DEFAULT_DEFENSE_RADIO);

                if (defense == null)
                    return;

                map.AddDefense(defense);

                numDefenses--;
            }
        }

        #endregion
    }
}
