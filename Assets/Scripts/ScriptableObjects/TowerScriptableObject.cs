using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower", order = 3)]
public class TowerScriptableObject : ScriptableObject
{
    public enum TowerType
    {
        Slime,
        Knight,
        Archer,
        Cleric,
        Wizard
    }
    public TowerType towerType;
    public int costOfTower, health;
    public float useSpeed, sightRange;
    public GameObject deathParticles;
    public GameObject healParticels;
    public GameObject[] projectile;
}
