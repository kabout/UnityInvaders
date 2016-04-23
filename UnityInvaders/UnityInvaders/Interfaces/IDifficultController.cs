using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IDifficultController
    {
        /// <summary>
        /// Devuelve el número de defensas dentro del mapa map para
        /// el nivel de dificultad difficultLevel.
        /// </summary>
        /// <param name="map">Mapa</param>
        /// <returns>Devuelve el número de celdas que ocupan las defensas o 0 si algo sale mal</returns>
        int GetNumberOfDefenses(IMap map);

        /// <summary>
        /// Devuelve el número de recursos para crear defensas para un nivel de dificultad difficultLevel
        /// </summary>
        int GetNumbersOfUcosForDefenses();

        /// <summary>
        /// Devuelve el número de recursos para crear aliens para un nivel de dificultad difficultLevel
        /// </summary>
        /// <param name="difficultLevel">Nivel de dificultad</param>
        int GetNumbersOfUcosForAliens();

        /// <summary>
        /// Devuelve el nivel de defensa según la dificultad del juego.
        /// </summary>
        /// <returns></returns>
        LevelDefense GetLevelDefense();

        /// <summary>
        /// Devuelve el daño que genera la defensa en función del nivel d
        /// </summary>
        /// <param name="levelDefense"></param>
        /// <returns></returns>
        int GetDefenseDamage(LevelDefense levelDefense);

        /// <summary>
        /// Devuelve el número de obstaculos a situar dentro del map map para el nivel de dificultad difficultLevel.
        /// </summary>
        /// <param name="map">Mapa</param>
        /// <returns>Devuelve el número de celdas que ocupan los obstáculos o 0 si algo sale mal</returns>
        int GetNumberOfObstacles(IMap map);

        /// <summary>
        /// Devuelve el tamaño mínimo de los obstáculos según el nivel de dificultad.
        /// <returns></returns>
        int GetMinSizeObstacle();

        /// <summary>
        /// Devuelve el tamaño mínimo de los obstáculos según el nivel de dificultad.
        /// </summary>
        int GetMaxSizeObstacle();
    }
}