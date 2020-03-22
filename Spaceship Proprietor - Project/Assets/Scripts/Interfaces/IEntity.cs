using System;

public interface IEntity : IShip
{
    //All of the interfaces.
}

public interface IShip : IHaveHealth
{
    float ModifyArmor(float amount);

    int ModifyShield(int amount);
}

public interface IHaveHealth : ITakeDamage
{
    float ModifyHealth(float amount);
}

public interface ITakeDamage
{
    void TakeDamage(float amount);
}