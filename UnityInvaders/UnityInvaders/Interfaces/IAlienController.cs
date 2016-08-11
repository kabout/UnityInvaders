using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IAlienController
    {
        /// <summary>
        /// Calcula el camino que debe seguir un alien para llegar a su destino
        /// </summary>
        /// <param name="source">Posición origen</param>
        /// <param name="target">Posición destino</param>
        /// <param name="map">Mapa por donde se mueve el alien</param>
        /// <returns>Devuelve una lista ordenada de posiciones por las que tiene que pasar el alien</returns>
        List<Position> CalculePath (Position source, Position target, IMap map);
    }
}
