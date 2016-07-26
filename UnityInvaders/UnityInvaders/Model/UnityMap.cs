using System.Collections.Generic;
using UnityEngine;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class UnityMap
    {
        #region Fields

        private GameObject floor;
        private IList<UnityObstacle> obstacles;
        private IList<UnityDefense> defenses;

        #endregion

        #region Properties

        /// <summary>
        /// Anchura del mapa
        /// </summary>
        public int Width { get { return (int)floor.transform.localScale.x; } }
        /// <summary>
        /// Altura del mapa
        /// </summary>
        public int Height { get { return (int)floor.transform.localScale.z; } }
        /// <summary>
        /// Lista de sólo lectura de los obstáculos del mapa
        /// </summary>
        public IList<UnityObstacle> Obstacles { get { return obstacles; } }
        /// <summary>
        /// Lista de sólo lectura de las defensas del mapa
        /// </summary>
		public IList<UnityDefense> Defenses { get { return defenses; } }

        #endregion

        #region Constructors

        public UnityMap(IList<UnityObstacle> obstacles, IList<UnityDefense> defenses, GameObject floor)
        {
            this.floor = floor;
            this.obstacles = obstacles;
            this.defenses = defenses;
        }

        #endregion
    }
}