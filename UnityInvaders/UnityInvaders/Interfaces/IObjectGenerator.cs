namespace UnityInvaders.Interfaces
{
    public interface IObjectGenerator
    {
        /// <summary>
        /// Genera un obstáculo de como máximo numCellsOfObstacles celdas
        /// Con anchura y altura aleatorias. No lo añade al mapa map.
        /// </summary>
        /// <param name="numCellsOfObstacles">Número máximo de celdas que ocupa el obstáculo</param>
        /// <param name="map">Mapa</param>
        /// <returns>Devuelve un obstaculo en una posición del mapa.</returns>
        IObstacle GenerateObstacle(int numCellsOfObstacles, IMap map);
        /// <summary>
        /// Genera una defensa de tamaño sizeDefense en el mapa map. No la añade al mapa map.
        /// </summary>
        /// <param name="sizeDefense">Anchura de la defensa</param>
        /// <param name="map">Mapa</param>
        /// <returns>Devuelve una defensa en una posición del mapa.</returns>
        IDefense GenerateDefense(int sizeDefense, IMap map);
    }
}