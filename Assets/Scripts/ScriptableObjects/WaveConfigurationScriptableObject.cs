using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfiguration", menuName = "ScriptableObjects/WaveConfiguration", order = 2)]
public class WaveConfigurationScriptableObject : ScriptableObject
{
    public int waveNumber, moneyReward, sprint, heavy, flying, exploding;
    public float spawnTimer;
}
