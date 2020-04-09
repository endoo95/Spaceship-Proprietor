using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects / Meteor")]
public class MeteorScriptableObject : ScriptableObject
{
    public float flySpeed;
    public float turnSpeed;

    public int maxHealth;

    public string meteorSize;
}
