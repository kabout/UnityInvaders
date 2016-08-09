using System;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;
using UnityInvaders.Utils;

namespace UnityInvaders.Controllers
{
    public class DifficultController : IDifficultController
    {
        #region Fields

        int difficultLevel;

        #endregion

        #region Constructors

        public DifficultController(int difficultLevel)
        {
            this.difficultLevel = difficultLevel;
        }

        #endregion

        #region Methods

        public int GetNumberOfDefenses(IMap map)
        {
            return (int)Math.Round(map.Size * map.Size * Constants.MAX_OBSTACLES_PER_AREA_UNIT * (difficultLevel / 999.0f));
        }

        public int GetNumberOfObstacles(IMap map)
        {
            return (int)Math.Round(map.Size * map.Size * Constants.MAX_OBSTACLES_PER_AREA_UNIT * (difficultLevel / 999.0f));
        }

        public int GetNumberOfDefenseTypes(int numDefenses)
        {
            return (int)(numDefenses * Constants.DEFENSE_TYPES_PER_DEFENSE) + 1;
        }

        public int GetMinRadiusOfObstacle()
        {
            return Constants.MIN_OBSTACLE_RADIUS;
        }

        public int GetMaxRadiusOfObstacle ()
        {
            return Constants.MAX_OBSTACLE_RADIUS;
        }

        #endregion
    }
}
