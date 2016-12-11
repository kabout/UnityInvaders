public interface IObjectManager
{
    /// <summary>
    /// Genera un obstáculo con medidas entre minRadius y maxRadius.
    ///  No lo añade al mapa map.
    /// </summary>
    /// <param name="map">Mapa</param>
    /// <param name="minRadius">Mínimo radio del obstáculo</param>
    /// <param name="maxRadius">Máximo radio del obstáculo</param>
    /// <returns>Devuelve un obstaculo en una posición del mapa.</returns>
    IObstacle GenerateObstacle(IMap map, int minRadius, int maxRadius);
    /// <summary>
    /// Genera una defensa de tamaño sizeDefense en el mapa map. No la añade al mapa map.
    /// </summary>
    /// <param name="position">Posición de la defensa</param>
    /// <returns>Devuelve una defensa en la posición position del mapa.</returns>
    IDefense GenerateDefense(IPosition position);
}