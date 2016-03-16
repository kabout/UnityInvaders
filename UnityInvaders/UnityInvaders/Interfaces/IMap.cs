using System.Collections.Generic;

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
        /// Lista de sólo lectura de las defensas del mapa
        /// </summary>
        IReadOnlyList<IDefense> Defenses { get; }
        /// <summary>
        /// Añade un obstáculo al mapa
        /// </summary>
        /// <param name="obstacle">Obstáculo que se añade al mapa</param>
        /// <returns>Devuelve true si lo puede añadir o false en caso contrario</returns>
        bool AddObstacle(IObstacle obstacle);
        /// <summary>
        /// Indica si el obstáculo está en una posición válida del mapa.
        /// </summary>
        /// <param name="obstacle">Obstáculo</param>
        /// <returns>Devuelve true si la posición es correcta o false en caso contrario</returns>
        bool IsValidPosition(IObstacle obstacle);
        /// <summary>
        /// Indica si la defensa está en una posición válida del mapa.
        /// </summary>
        /// <param name="defense">Defensa</param>
        /// <returns>Devuelve true si la posición es correcta o false en caso contrario</returns>
        bool IsValidPosition(IDefense defense);
        /// <summary>
        /// Añade una defensa al mapa
        /// </summary>
        /// <param name="defense">Defensa que se añade al mapa</param>
        /// <returns>Devuelve true si la puede añadir o false en caso contrario</returns>
        bool AddDefense(IDefense defense);
    }
}
