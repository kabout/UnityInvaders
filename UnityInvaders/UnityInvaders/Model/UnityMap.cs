using System.Collections.Generic;
using UnityEngine;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class UnityMap
    {
        #region Fields

        Terrain terrain;
        IList<UnityObstacle> obstacles;

        #endregion

        #region Properties

        /// <summary>
        /// Anchura del mapa
        /// </summary>
        public int Width { get { return (int)terrain.transform.localScale.x; } }
        /// <summary>
        /// Altura del mapa
        /// </summary>
        public int Height { get { return (int)terrain.transform.localScale.z; } }
        /// <summary>
        /// Lista de sólo lectura de los obstáculos del mapa
        /// </summary>
        public IReadOnlyList<UnityObstacle> Obstacles { get { return (IReadOnlyList<UnityObstacle>)obstacles; } }
        /// <summary>
        /// Lista de sólo lectura de las defensas del mapa
        /// </summary>
        public IReadOnlyList<IDefense> Defenses { get; }

        #endregion

        #region Constructors

        public UnityMap(IList<UnityObstacle> obstacles, Terrain terrain)
        {
            this.terrain = terrain;
            this.obstacles = obstacles;
        }

        #endregion
    }
}