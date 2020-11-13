public interface IDamagable
{
    CharacterStats BaseStats { get; }

    void TakeDamage(int value);
    void SlowDownMovementSpeed(float mitigationPercent, float time);
}
