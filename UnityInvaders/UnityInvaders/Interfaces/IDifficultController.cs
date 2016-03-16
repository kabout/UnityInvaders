using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IDifficultController
    {
        /// <summary>
        /// Devuelve el número de celdas que deben ocupar las defensas dentro del mapa map para
        /// el nivel de dificultad difficultLevel.
        /// </summary>
        /// <param name="map">Mapa</param>
        /// <param name="sizeDefense">Anchura de la defensa</param>
        /// <param name="difficultLevel">Nivel de dificultad</param>
        /// <returns>Devuelve el número de celdas que ocupan las defensas o 0 si algo sale mal</returns>
        int GetNumberCellsOfDefenses(IMap map, int sizeDefense, DifficultLevel difficultLevel);

        /// <summary>
        /// Devuelve el número de celdas que deben ocupar los obstáculos dentro del map map para el nivel de dificultad difficultLevel.
        /// </summary>
        /// <param name="map">Mapa</param>
        /// <param name="difficultLevel">Nivel de dificultad</param>
        /// <returns>Devuelve el número de celdas que ocupan los obstáculos o 0 si algo sale mal</returns>
        int GetNumberCellsOfObstacles(IMap map,  DifficultLevel difficultLevel);
    }
}