public interface IObstacle : IEntity
{
    /// <summary>
    /// CAmbia la posición del obstáculo
    /// </summary>
    /// <param name="position">Buena posición</param>
    void ChangePosition(IPosition position);
}