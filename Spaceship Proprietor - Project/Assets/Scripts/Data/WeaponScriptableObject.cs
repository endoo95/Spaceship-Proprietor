
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Objects / Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public string weaponType; //Projectile, Laser, Phaser, Rocket, Torpedo
    public enum WeaponType
    {
        Projectile,
        Laser,
        Phaser,
        Rocket,
        Torpedo
    }; //Poprawić! Zrobić dropdown menu
    

    public float shootingSpeed;
    public float weaponDamage;

    public int heatCount;
}