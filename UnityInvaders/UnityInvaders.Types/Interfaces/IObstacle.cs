using UnityEngine;

public interface IObstacle : IEntity
{
    /// <summary>
    /// CAmbia la posición del obstáculo
    /// </summary>
    /// <param name="position">Buena posición</param>
    void ChangePosition(Vector3 position);
}