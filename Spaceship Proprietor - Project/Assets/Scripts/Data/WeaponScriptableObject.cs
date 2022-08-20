using UnityEngine;
using System.Collections.Generic;
//Only if the Unity Editor is available
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Scriptable Objects / Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    //Description
    public string weaponName;
    [TextArea] public string description;

    //Weapon usage dictionary
    public WeaponType weaponType;
    public ShootType shootType;

    public enum WeaponType
    {
        Energy,
        Kinetic,
        Explosive
    };

    public enum ShootType
    {
        RenderLine,
        Projectile
    };

    //Damage Types
    public bool isKineticDamage;
    public bool isThermalDamage;
    public bool isSpecialDamage;

    //IsKineticDamage
    public float kineticDamage;

    //IsThermalDamage
    public float thermalDamage;

    //IsSpecialDamage
    public int slowProcentage;
    public int weaponOverchage;

    public List<string> targetList;
    private bool showTargetList = false;

    public float weaponFireRate;
    public int weaponCost;

    #region Editor
    //Region to hide this element
#if UNITY_EDITOR
    [CustomEditor(typeof(WeaponScriptableObject))]
    public class WeaponScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            //Assign WeaponScriptableObject as target
            WeaponScriptableObject weaponScriptableObject = (WeaponScriptableObject)target;

            DrawBasicInfo(weaponScriptableObject);
            EditorGUILayout.Space();
            DrawTargetList(weaponScriptableObject);
            EditorGUILayout.Space();
            DrawAdvancedInfo(weaponScriptableObject);

        }

        //More advanced info like: type, shoot type, cost, etc.
        private static void DrawAdvancedInfo(WeaponScriptableObject weaponScriptableObject)
        {
            EditorGUILayout.LabelField("Weapon Type Info");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Weapon Type", GUILayout.MaxWidth(150));
            weaponScriptableObject.weaponType = (WeaponType)EditorGUILayout.EnumPopup(weaponScriptableObject.weaponType, GUILayout.MaxWidth(150));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Shoot Type", GUILayout.MaxWidth(150));
            weaponScriptableObject.shootType = (ShootType)EditorGUILayout.EnumPopup(weaponScriptableObject.shootType, GUILayout.MaxWidth(150));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            //More advanced about damege type
            EditorGUILayout.LabelField("Damage Info");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Is Kinetic", GUILayout.MaxWidth(100));
            weaponScriptableObject.isKineticDamage = EditorGUILayout.Toggle(weaponScriptableObject.isKineticDamage);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Is Thermal", GUILayout.MaxWidth(100));
            weaponScriptableObject.isThermalDamage = EditorGUILayout.Toggle(weaponScriptableObject.isThermalDamage);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Is Special", GUILayout.MaxWidth(100));
            weaponScriptableObject.isSpecialDamage = EditorGUILayout.Toggle(weaponScriptableObject.isSpecialDamage);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            //Only if statement is true
            if (weaponScriptableObject.isKineticDamage)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Kinetic Damage", GUILayout.MaxWidth(100));
                weaponScriptableObject.kineticDamage = EditorGUILayout.FloatField(weaponScriptableObject.kineticDamage, GUILayout.MaxWidth(200));
                EditorGUILayout.EndHorizontal();
            }

            if (weaponScriptableObject.isThermalDamage)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Thermal Damage", GUILayout.MaxWidth(100));
                weaponScriptableObject.thermalDamage = EditorGUILayout.FloatField(weaponScriptableObject.thermalDamage, GUILayout.MaxWidth(200));
                EditorGUILayout.EndHorizontal();
            }

            if (weaponScriptableObject.isSpecialDamage)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Slow Procentage", GUILayout.MaxWidth(100));
                weaponScriptableObject.slowProcentage = EditorGUILayout.IntField(weaponScriptableObject.slowProcentage, GUILayout.MaxWidth(200));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Weapon Overcharge", GUILayout.MaxWidth(100));
                weaponScriptableObject.weaponOverchage = EditorGUILayout.IntField(weaponScriptableObject.weaponOverchage, GUILayout.MaxWidth(200));
                EditorGUILayout.EndHorizontal();
            }
        }

        //Target List in foldout
        private static void DrawTargetList(WeaponScriptableObject weaponScriptableObject)
        {
            weaponScriptableObject.showTargetList = EditorGUILayout.Foldout(weaponScriptableObject.showTargetList, "Target List", true);
            if (weaponScriptableObject.showTargetList)
            {
                List<string> list = weaponScriptableObject.targetList;
                int size = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count));

                EditorGUI.indentLevel++;
                while (size > list.Count)
                {
                    list.Add(null);
                }

                while (size < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = EditorGUILayout.TextField("Target " + i, list[i]);
                }
                EditorGUI.indentLevel--;
            }
        }

        //Basic info about weapon
        private static void DrawBasicInfo(WeaponScriptableObject weaponScriptableObject)
        {
            EditorGUILayout.LabelField("Basic Info");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Weapon Name", GUILayout.MaxWidth(100));
            weaponScriptableObject.weaponName = EditorGUILayout.TextField(weaponScriptableObject.weaponName, GUILayout.MaxWidth(200));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Description", GUILayout.MaxWidth(100));
            weaponScriptableObject.description = EditorGUILayout.TextArea(weaponScriptableObject.description, GUILayout.MaxWidth(300), GUILayout.MaxHeight(50));

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Fire Rate", GUILayout.MaxWidth(100));
            weaponScriptableObject.weaponFireRate = EditorGUILayout.FloatField(weaponScriptableObject.weaponFireRate, GUILayout.MaxWidth(200));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Cost", GUILayout.MaxWidth(100));
            weaponScriptableObject.weaponCost = EditorGUILayout.IntField(weaponScriptableObject.weaponCost, GUILayout.MaxWidth(200));
            EditorGUILayout.EndHorizontal();
        }
    }
#endif
    #endregion
}