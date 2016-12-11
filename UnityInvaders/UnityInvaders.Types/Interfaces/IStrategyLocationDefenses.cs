using System.Collections.Generic;

public interface IStrategyLocationDefenses
{
    void InitStrategy(IList<IObstacle> obstacles, int defenseRadius, int mapSize, int cellSize);
    IPosition GetPositionForDefense(IList<IDefense> defenses);
}
