using System.Collections.Generic;

public interface IStrategyAlienAttack
{
    /// <summary>
    /// Calcula el camino que debe seguir un alien para llegar a su destino
    /// </summary>
    /// <param name="source">Posición origen</param>
    /// <param name="target">Posición destino</param>
    /// <param name="map">Mapa por donde se mueve el alien</param>
    /// <returns>Devuelve una lista ordenada de posiciones por las que tiene que pasar el alien</returns>
    List<IPosition> CalculatePath(IPosition source, IPosition target, IList<IObstacle> obstacles, IList<IDefense> defenses, int sizeMap, int cellSize);
}
