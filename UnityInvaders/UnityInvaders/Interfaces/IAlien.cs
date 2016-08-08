using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IAlien
    {
        /// <summary>
        /// Longitud del alien en el eje x
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Longitud del alien en el eje y
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Posición del alien dentro del mapa
        /// </summary>
        Position Position { get; }
        /// <summary>
        /// Nivel de daño que genera el alien
        /// </summary>
        int Damage { get; }
        /// <summary>
        /// Rango de alcance del alien.
        /// </summary>
        int Range { get; }
        /// <summary>
        /// Nivel del alien. Se utiliza para calcular el Rango de alcance.
        /// </summary>
        LevelAlien Level { get; }
        /// <summary>
        /// Coste del alien según su nivel
        /// </summary>
        int Cost { get; }
        /// <summary>
        /// Salud de la defensa
        /// </summary>
        int Health { get; }
        /// <summary>
        /// Resta salud al alien indicado por damage
        /// </summary>
        /// <param name="damage">Puntos de vida a restar</param>
        void TakeDamage(int damage);
        /// <summary>
        /// Cambia la posición del alien
        /// </summary>
        /// <param name="position">Nueva posición del alien</param>
        void ChangePosition(Position position);
        /// <summary>
        /// Indica si el alien tiene aún vida
        /// </summary>
        /// <returns>Devuelve true si tiene vida o false en caso contrario</returns>
        bool IsAlive();
    }
}