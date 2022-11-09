using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad]
public static class LevelsControllerToolbarButton
{
    private const string enabledText = "Toggle level testing\n\nLevel is in test";
    private const string disanabledText = "Toggle level testing\n\nLevel is NOT in test";
    private const string TEST_SCENE_ENABLED_KEY = "SceneTesterEnabled";

    private static bool sceneTesterEnabled;

    static LevelsControllerToolbarButton()
    {
        EditorApplication.update += Init;
    }

    static void Init()
    {
        EditorApplication.update -= Init;
        ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
    }

    static void OnToolbarGUI()
    {
        var image = EditorGUIUtility.IconContent(@"UnityEditor.GameView@2x").image;
        sceneTesterEnabled = GUILayout.Toggle(
            sceneTesterEnabled,
            new GUIContent(null, image, sceneTesterEnabled ? enabledText : disanabledText),
            "Command"
        );

        SceneTester.SetEnabled(sceneTesterEnabled);
        EditorPrefs.SetBool(TEST_SCENE_ENABLED_KEY, sceneTesterEnabled);
    }



    [DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        sceneTesterEnabled = EditorPrefs.GetBool(TEST_SCENE_ENABLED_KEY, false);
    }
}
