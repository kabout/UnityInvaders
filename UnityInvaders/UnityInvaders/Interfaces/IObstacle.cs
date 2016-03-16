using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IObstacle
    {
        /// <summary>
        /// Longitud del obstáculo en el eje x
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Longitud del obstáculo en el eje y
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Posición del obstaculo en el mapa
        /// </summary>
        Position Position { get; }
        /// <summary>
        /// CAmbia la posición del obstáculo
        /// </summary>
        /// <param name="position">Buena posición</param>
        void ChangePosition(Position position);
    }
}
