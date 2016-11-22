namespace UnityInvaders.Interfaces
{
    public interface IDefense : IEntity
    {
        /// <summary>
        /// Nivel de daño que genera la defensa
        /// </summary>
        float Damage { get; }
        /// <summary>
        /// Dispersión del disparo de la defensa
        /// </summary>
        int Dispersion { get; }
        /// <summary>
        /// Rango de alcance de la defensa desde cada uno de sus bordes
        /// </summary>
        int Range { get; }
        /// <summary>
        /// Coste de la Defensa, según su nivel.
        /// </summary>
        int Cost { get; }
        /// <summary>
        /// Salud de la defensa
        /// </summary>
        float Health { get; }
        /// <summary>
        /// Tipo de defensa
        /// </summary>
        int Type { get; }
        /// <summary>
        /// Resta salud a la defensa indicado por damage
        /// </summary>
        /// <param name="damage">Puntos de vida a restar</param>
        void TakeDamage(float damage);
        /// <summary>
        /// Indica si la defensa tiene aún vida
        /// </summary>
        /// <returns>Devuelve true si tiene vida o false en caso contrario</returns>
        bool IsAlive();
    }
}