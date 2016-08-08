using System.Collections.Generic;
using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IDefenseController
    {
        /// <summary>
        /// Coloca las defensas en el mapa
        /// </summary>
        /// <param name="map">Instancia del mapa</param>
        void PlaceDefenses(IMap map);

        /// <summary>
        /// Devuelve una lista con los alien que están dentro del rango de una defensa
        /// </summary>
        /// <param name="map">Instancia del mapa</param>
        /// <param name="defense">Defensa a evaluar</param>
        IList<IAlien> GetAliensInRange (IMap map,IDefense defense);
    }
}