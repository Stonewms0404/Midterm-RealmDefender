using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower", order = 3)]
public class TowerScriptableObject : ScriptableObject
{
    public enum TowerType
    {
        Snowman,
        Nymph,
        Archer,
        Ninja,
        Wizard
    }
    public TowerType towerType;
    public int costOfTower, health, useAmount;
    public float useSpeed;
}
