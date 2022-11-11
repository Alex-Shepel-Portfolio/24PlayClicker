using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="TextPresetsToWrite",menuName="CustomTools/Writer/TextPresetsToWrite", order = 1)]
public class TextPresetsToWrite : ScriptableObject
{
    public List<TextPreset> TextsPreset = new List<TextPreset>();

    public TextPreset GetRandomPreset()
    {
        var textPreset = TextsPreset;
        textPreset.Shuffle();
        return textPreset[0];
    }
}

[System.Serializable]
public struct TextPreset
{
    public string TextToWrite;
    public float WriteSpeedOffset;
}
