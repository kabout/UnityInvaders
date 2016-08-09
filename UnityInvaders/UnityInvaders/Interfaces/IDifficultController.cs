using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IDifficultController
    {
        /// <summary>
        /// Devuelve el número de defensas a situar dentro del mapa map
        /// </summary>
        /// <param name="map">Mapa</param>
        int GetNumberOfDefenses(IMap map);

        /// <summary>
        /// Devuelve el número de obstaculos a situar dentro del map map.
        /// </summary>
        /// <param name="map">Mapa</param>
        int GetNumberOfObstacles(IMap map);

        /// <summary>
        /// Devuelve el número de tipos de defensas
        /// </summary>
        /// <param name="numDefenses">Número de defensas del mapa</param>
        int GetNumberOfDefenseTypes (int numDefenses);

        /// <summary>
        /// Devuelve el tamaño mínimo del radio para los obstáculos.
        int GetMinRadiusOfObstacle ();

        /// <summary>
        /// Devuelve el tamaño máximo del radio para los obstáculos.
        /// </summary>
        int GetMaxRadiusOfObstacle ();
    }
}