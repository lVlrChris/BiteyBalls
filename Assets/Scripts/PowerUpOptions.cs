using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Power Up/Options")]
public class PowerUpOptions : ScriptableObject
{
    [HideInInspector]
    public bool playerMassOptions;
    [HideInInspector]
    public int increasePlayerMassPercentage;

    [HideInInspector]
    public bool playerSizeOptions;
    [HideInInspector]
    public int increasePlayerSizePercentage;
}


#if UNITY_EDITOR
[CustomEditor(typeof(PowerUpOptions))]
public class PowerUpOptions_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        PowerUpOptions options = (PowerUpOptions)target;
        options.playerMassOptions = EditorGUILayout.Foldout(options.playerMassOptions, "Edit Mass");
        if (options.playerMassOptions)
        {
            options.increasePlayerMassPercentage = EditorGUILayout.IntSlider("Increase mass by %", options.increasePlayerMassPercentage, 0, 100);
        }
        options.playerSizeOptions = EditorGUILayout.Foldout(options.playerSizeOptions, "Edit Size");
        if (options.playerSizeOptions)
        {
            options.increasePlayerSizePercentage = EditorGUILayout.IntSlider("Increase size by %", options.increasePlayerSizePercentage, 0, 100);
        }
    }
}
#endif