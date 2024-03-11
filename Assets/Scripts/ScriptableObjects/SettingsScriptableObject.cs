using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 4)]
public class SettingsScriptableObject : ScriptableObject
{
    public bool music, sfx, particles, fullscreen;
    public int sceneIndex;
}
