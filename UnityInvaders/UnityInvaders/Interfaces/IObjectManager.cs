using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IObjectManager
    {
        /// <summary>
        /// Genera un obstáculo de como máximo numCellsOfObstacles celdas
        /// Con anchura y altura aleatorias. No lo añade al mapa map.
        /// </summary>
        /// <param name="numCellsOfObstacles">Número máximo de celdas que ocupa el obstáculo</param>
        /// <param name="map">Mapa</param>
        /// <param name="maxUcos">Máximo dinero a gastar</param>
        /// <returns>Devuelve un obstaculo en una posición del mapa.</returns>
        IObstacle GenerateObstacle(int numCellsOfObstacles, IMap map, int maxUcos = int.MaxValue);
        /// <summary>
        /// Genera una defensa de tamaño sizeDefense en el mapa map. No la añade al mapa map.
        /// </summary>
        /// <param name="sizeDefense">Anchura de la defensa</param>
        /// <param name="map">Mapa</param>
        /// <param name="maxUcos">Máximo dinero a gastar</param>
        /// <returns>Devuelve una defensa en una posición del mapa.</returns>
        IDefense GenerateDefense(int sizeDefense, DifficultLevel difficulLevel, IMap map, int maxUcos = int.MaxValue);
    }
}