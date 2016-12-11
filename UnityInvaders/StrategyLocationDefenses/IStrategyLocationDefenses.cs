using System.Collections.Generic;
using UnityEngine;

namespace StrategyLocationDefenses
{
    public interface IStrategyLocationDefenses
    {
        void InitStrategy(IList<IObstacle> obstacles, int defenseRadius, int mapSize, int cellSize);
        Vector3 GetPositionForDefense(IList<IDefense> defenses);
    }
}
