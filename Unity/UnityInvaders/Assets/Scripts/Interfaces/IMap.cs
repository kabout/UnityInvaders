using UnityEngine;
using System.Collections.Generic;

public interface IMap
{
    /// <summary>
    /// Dimensión del mapa, siendo un mapa de Size x Size
    /// </summary>
    int Size { get; }

    /// <summary>
    /// Tamaño de las celdas
    /// </summary>
    int CellSize { get; }
    /// <summary>
    /// Lista de sólo lectura de los obstáculos del mapa
    /// </summary>
    IList<IObstacle> Obstacles { get; }
    /// <summary>
    /// Lista de sólo lectura de las defensas del mapa
    /// </summary>
    IList<IDefense> Defenses { get; }
    /// <summary>
    /// Lista de sólo lectura de los aliens del mapa
    /// </summary>
    IList<IAlien> Aliens { get; }
    /// <summary>
    /// Devuelve el margen del mapa
    /// </summary>
    int Margin { get; }
    /// <summary>
    /// Añade un obstáculo al mapa
    /// </summary>
    /// <param name="obstacle">Obstáculo que se añade al mapa</param>
    /// <returns>Devuelve true si lo puede añadir o false en caso contrario</returns>
    bool AddObstacle(IObstacle obstacle);
    /// <summary>
    /// Añade una defensa al mapa
    /// </summary>
    /// <param name="defense">Defensa que se añade al mapa</param>
    /// <returns>Devuelve true si la puede añadir o false en caso contrario</returns>
    bool AddDefense(IDefense defense);
    /// <summary>
    /// Añade un alien al mapa
    /// </summary>
    /// <param name="alien">Alien que se añade al mapa</param>
    /// <returns>Devuelve true si lo puede añadir o false en caso contrario</returns>
    bool AddAlien(IAlien alien);
    /// <summary>
    /// Devuelve las posiciones vacías del mapa para un objeto de radio radius
    /// </summary>
    /// <param name="radius">Radio del objeto</param>
    /// <returns></returns>
    List<Vector3> GetFreePositions(float radius);
    /// <summary>
    /// Indica si el obstáculo está en una posición válida del mapa.
    /// </summary>
    /// <param name="obstacle">Obstáculo</param>
    /// <returns>Devuelve true si la posición es correcta o false en caso contrario</returns>
    bool IsValidPosition(IObstacle obstacle);
    /// <summary>
    /// Indica si el objeto con radio radius y posición position está en una posición válida del mapa.
    /// </summary>
    /// <param name="position">Posición a comprobar</param>
    /// <param name="radius">Radio del objeto</param>
    /// <returns>Devuelve true si la posición es correcta o false en caso contrario</returns>
    bool IsValidPosition(IPosition position, float radius);
    /// <summary>
    /// Indica si la defensa está en una posición válida del mapa.
    /// </summary>
    /// <param name="defense">Defensa</param>
    /// <returns>Devuelve true si la posición es correcta o false en caso contrario</returns>
    bool IsValidPosition(IDefense defense);
    /// <summary>
    /// Devuelve el mapa en forma de matriz
    /// 0: Espacio en blanco
    /// 1: Obstáculo
    /// 2: Defensa
    /// </summary>
    int[,] GetMap();
    /// <summary>
    /// Corregir casillas inalcanzables
    /// </summary>
    void CorrectCellUnReachables();
}
