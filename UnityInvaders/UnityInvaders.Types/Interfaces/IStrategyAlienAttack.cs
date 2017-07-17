using System.Collections.Generic;

public interface IStrategyAlienAttack
{
    /// <summary>
    /// Calcula el camino que debe seguir un alien para llegar a su destino
    /// </summary>
    /// <param name="alienPosition">Posición del alien</param>
    /// <param name="defensePosition">Posición de la defensa</param>
    /// <param name="obstacles">Obstáculos del mapa</param>
    /// <param name="defenses">Defensas del mapa</param>
    /// <param name="sizeMap">Tamaño del mapa</param>
    /// <param name="cellSize">Tamaño de la celda</param>
    /// <returns>Devuelve una lista ordenada de posiciones por las que tiene que pasar el alien</returns>
    List<IPosition> CalculatePath(IPosition alienPosition, IPosition defensePosition, IList<IObstacle> obstacles, IList<IDefense> defenses, int sizeMap, int cellSize);

    /// <summary>
    /// Devuelve el próximo objetivo para un alien.
    /// </summary>
    /// <param name="alienPosition">Posición del alien</param>
    /// <param name="obstacles">Obstáculos del mapa</param>
    /// <param name="defenses">Defensas del mapa</param>
    /// <param name="sizeMap">Tamaño del mapa</param>
    /// <param name="cellSize">Tamaño de la celda</param>
    /// <returns></returns>
    IDefense GetNextDefenseToAttack(IPosition alienPosition, IList<IObstacle> obstacles, IList<IDefense> defenses, int sizeMap, int cellSize);
}
