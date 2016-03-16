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
        DamageType Damage { get; }
        /// <summary>
        /// Rango de alcance de la defensa desde cada uno de sus bordes
        /// </summary>
        int Range { get; }
        /// <summary>
        /// Nivel de la defensa. Se utiliza para calcular el Rango de alcance.
        /// </summary>
        LevelDefense Level { get; }
        /// <summary>
        /// Salud de la defensa
        /// </summary>
        int Health { get; }
        /// <summary>
        /// Resta salud a la defensa indicado por damage
        /// </summary>
        /// <param name="damage"></param>
        void TakeDamage(DamageType damage);
        /// <summary>
        /// Cambia la posición de la defensa
        /// </summary>
        /// <param name="position"></param>
        void ChangePosition(Position position);
    }
}