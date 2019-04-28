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

    [HideInInspector]
    public bool playerMovementOptions;
    [HideInInspector]
    public int increasePlayerJumpHeightPercentage;
    public int increasePlayerMoveSpeedPercentage;

}


#if UNITY_EDITOR
[CustomEditor(typeof(PowerUpOptions))]
public class PowerUpOptions_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        PowerUpOptions options = (PowerUpOptions)target;

        options.playerMassOptions = EditorGUILayout.Foldout(options.playerMassOptions, "Edit player mass");
        if (options.playerMassOptions)
        {
            options.increasePlayerMassPercentage = EditorGUILayout.IntSlider("Increase mass by %", options.increasePlayerMassPercentage, 0, 100);
        }

        options.playerSizeOptions = EditorGUILayout.Foldout(options.playerSizeOptions, "Edit player size");
        if (options.playerSizeOptions)
        {
            options.increasePlayerSizePercentage = EditorGUILayout.IntSlider("Increase size by %", options.increasePlayerSizePercentage, 0, 100);
        }

        options.playerMovementOptions = EditorGUILayout.Foldout(options.playerMovementOptions, "Edit player movement");
        if (options.playerMovementOptions)
        {
            options.increasePlayerJumpHeightPercentage = EditorGUILayout.IntSlider("Increase jumpheight by %", options.increasePlayerJumpHeightPercentage, 0, 100);
            options.increasePlayerMoveSpeedPercentage = EditorGUILayout.IntSlider("Increase movespeed by %", options.increasePlayerMoveSpeedPercentage, 0, 100);
        }
    }
}
#endif