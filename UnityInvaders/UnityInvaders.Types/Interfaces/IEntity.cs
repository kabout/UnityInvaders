using UnityEngine;

public interface IEntity
{
    /// <summary>
    /// Identificador del objeto
    /// </summary>
    int Id { get; }
    /// <summary>
    /// Posición del objeto
    /// </summary>
    Vector3 Position { get; }
    /// <summary>
    /// Radio del objeto
    /// </summary>
    float Radius { get; }
}