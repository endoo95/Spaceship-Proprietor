using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects / Ship")]
public class ShipScriptableObject : ScriptableObject
{
    public float flySpeed;
    public float turnSpeed;

    public int maxArmor;

    public int maxShield;

    public int maxHealth;
}