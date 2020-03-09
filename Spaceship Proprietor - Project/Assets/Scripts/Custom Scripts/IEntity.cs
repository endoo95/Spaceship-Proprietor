using System;

public interface IEntity
{

}

public interface IShip : IHaveHealth
{
    float flySpeed { get; set; }
    float turnSpeed { get; set; }

    float armor { get; set; }
    int maxArmor { get; }
    float ModifyArmor(float amount);

    int shield { get; set; }
    int maxShield { get; }
    int ModifyShield(int amount);
}

public interface IHaveHealth
{
    float health { get; set; }
    int maxHealth { get; }
    float ModifyHealth(float amount);
}

