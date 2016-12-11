using System.Collections.Generic;
using UnityEngine;

namespace StrategyAlienAttack
{
    public interface IStrategyAlienAttack
    {
        /// <summary>
        /// Calcula el camino que debe seguir un alien para llegar a su destino
        /// </summary>
        /// <param name="source">Posición origen</param>
        /// <param name="target">Posición destino</param>
        /// <param name="map">Mapa por donde se mueve el alien</param>
        /// <returns>Devuelve una lista ordenada de posiciones por las que tiene que pasar el alien</returns>
        List<Vector3> CalculatePath(Vector3 source, Vector3 target, IList<IObstacle> obstacles, IList<IDefense> defenses, int sizeMap, int cellSize);
    }
}
