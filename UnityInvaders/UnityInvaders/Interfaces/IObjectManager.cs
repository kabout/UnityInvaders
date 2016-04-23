using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IObjectManager
    {
        /// <summary>
        /// Genera un obstáculo con medidas aleatorias según el nivel de dificultad.
       ///  No lo añade al mapa map.
        /// </summary>
        /// <param name="map">Mapa</param>
        /// <returns>Devuelve un obstaculo en una posición del mapa.</returns>
        IObstacle GenerateObstacle(IMap map);
        /// <summary>
        /// Genera una defensa de tamaño sizeDefense en el mapa map. No la añade al mapa map.
        /// </summary>
        /// <param name="map">Mapa</param>
        /// <returns>Devuelve una defensa en una posición del mapa.</returns>
        IDefense GenerateDefense(IMap map);
    }
}