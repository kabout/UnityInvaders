using UnityEngine;

public interface IAlien : IEntity
{
    /// <summary>
    /// Nivel de daño que genera el alien
    /// </summary>
    float Damage { get; }
    /// <summary>
    /// Rango de alcance del alien.
    /// </summary>
    int Range { get; }
    /// <summary>
    /// Coste del alien según su nivel
    /// </summary>
    int Cost { get; }
    /// <summary>
    /// Salud de la defensa
    /// </summary>
    float Health { get; }
    /// <summary>
    /// Resta salud al alien indicado por damage
    /// </summary>
    /// <param name="damage">Puntos de vida a restar</param>
    void TakeDamage(float damage);
    /// <summary>
    /// Cambia la posición del alien
    /// </summary>
    /// <param name="position">Nueva posición del alien</param>
    void ChangePosition(Vector3 position);
    /// <summary>
    /// Indica si el alien tiene aún vida
    /// </summary>
    /// <returns>Devuelve true si tiene vida o false en caso contrario</returns>
    bool IsAlive();
}
