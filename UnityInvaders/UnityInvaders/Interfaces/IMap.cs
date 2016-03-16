using System.Collections.Generic;
using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IMap
    {
        /// <summary>
        /// Anchura del mapa
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Altura del mapa
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Lista de sólo lectura de los obstáculos del mapa
        /// </summary>
        IReadOnlyList<IObstacle> Obstacles { get; }
        /// <summary>
        /// Añade un obstáculo al mapa
        /// </summary>
        /// <param name="obstacle">Obstáculo que se añade al mapa</param>
        /// <returns>Devuelve true si lo puede añadir o false en caso contrario</returns>
        bool AddObstacle(IObstacle obstacle);
        /// <summary>
        /// Indica si la posición está disponible en el mapa para un objeto
        /// </summary>
        /// <param name="position">Posición de la esquina izquierda del objeto</param>
        /// <param name="width">Longitud del objecto en el eje x</param>
        /// <param name="height">Longitud del objecto en el eje y</param>
        /// <returns>Devuelve true si la posición es correcta o false en caso contrario</returns>
        bool IsValidPosition(Position position, int width, int height);
        /// <summary>
        /// Añade una defensa al mapa
        /// </summary>
        /// <param name="defense">Defensa que se añade al mapa</param>
        /// <returns>Devuelve true si la puede añadir o false en caso contrario</returns>
        bool AddDefense(IDefense defense);
    }
}
