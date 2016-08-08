using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IDefense
    {
        /// <summary>
        /// Longitud de la defensa en el eje x
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Longitud de la defensa en el eje y
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Posición de la defensa dentro del mapa
        /// </summary>
        Position Position { get; }
        /// <summary>
        /// Nivel de daño que genera la defensa
        /// </summary>
        int Damage { get; }
        /// <summary>
        /// Rango de alcance de la defensa desde cada uno de sus bordes
        /// </summary>
        int Range { get; }
        /// <summary>
        /// Nivel de la defensa. Se utiliza para calcular el Rango de alcance.
        /// </summary>
        LevelDefense Level { get; }
        /// <summary>
        /// Coste de la Defensa, según su nivel.
        /// </summary>
        int Cost { get; }
        /// <summary>
        /// Salud de la defensa
        /// </summary>
        int Health { get; }
        /// <summary>
        /// Resta salud a la defensa indicado por damage
        /// </summary>
        /// <param name="damage">Puntos de vida a restar</param>
        void TakeDamage(int damage);
        /// <summary>
        /// Cambia la posición de la defensa
        /// </summary>
        /// <param name="position">Nueva posición de la defensa</param>
        void ChangePosition(Position position);
        /// <summary>
        /// Indica si la defensa tiene aún vida
        /// </summary>
        /// <returns>Devuelve true si tiene vida o false en caso contrario</returns>
        bool IsAlive();
    }
}