using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IDefense
    {
        uint Width { get; }
        uint Height { get; }
        Position Position { get; }
        DamageType Damage { get; }
        uint Range { get; }
        LevelDefense Level { get; }
        uint Health { get; }
        void TakeDamage(DamageType damage);
    }
}