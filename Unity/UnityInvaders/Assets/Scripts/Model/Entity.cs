using UnityEngine;

public class Entity : IEntity
{
    #region Properties

    public int Id
    {
        get; private set;
    }

    public IPosition Position
    {
        get; private set;
    }

    public float Radius
    {
        get; private set;
    }

    #endregion

    #region Constructors

    public Entity(int id, IPosition position, int radius)
    {
        Id = id;
        Position = position;
        Radius = radius;
    }

    #endregion
}
