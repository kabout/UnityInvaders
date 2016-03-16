using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IMapController
    {
        /// <summary>
        /// Devuelve un mapa vacío
        /// </summary>
        /// <param name="width">Anchura del mapa</param>
        /// <param name="height">Altura del mapa</param>
        /// <returns></returns>
        IMap GetEmptyMap(int width, int height);
        /// <summary>
        /// Inicializa el mapa map para un nivel de dificultad difficultLevel.
        /// Añade los obstaculos y las defensas.
        /// El mapa debe estar vacío para poder inicializarlo.
        /// </summary>
        /// <param name="map">Mapa a inicializar</param>
        /// <param name="difficultLevel">Nivel de dificultad</param>
        void InitMap(IMap map, DifficultLevel difficultLevel);
    }
}