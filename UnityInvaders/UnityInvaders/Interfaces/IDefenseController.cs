using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IDefenseController
    {
        void PlaceDefenses(IMap map, DifficultLevel difficultLevel);
    }
}