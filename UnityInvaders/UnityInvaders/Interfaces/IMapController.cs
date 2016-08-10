using System.Drawing;
using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IMapController
    {
        /// <summary>
        /// Devuelve un mapa vacío con tamaño size x size
        /// </summary>
        /// <param name="size">Tamaño del mapa</param>
        /// <returns></returns>
        IMap GetEmptyMap(int size);
        /// <summary>
        /// Inicializa el mapa map para un nivel de dificultad difficultLevel.
        /// Añade los obstaculos y las defensas.
        /// El mapa debe estar vacío para poder inicializarlo.
        /// </summary>
        /// <param name="map">Mapa a inicializar</param>
        void InitMap(IMap map);
    }
}