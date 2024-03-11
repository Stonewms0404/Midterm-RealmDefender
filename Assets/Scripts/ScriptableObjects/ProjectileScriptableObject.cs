using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile", order = 3)]
public class ProjectileScriptableObject : ScriptableObject
{
    public enum ProjectileType
    {
        TOWERHEAL,
        ENEMYHEAL,
        DAMAGETOWER,
        DAMAGEENEMY
    }
    public enum SpecialEffect
    {
        NORMAL,
        HEALING,
        FREEZE,
        POISON,
        SWORD
    }

    public ProjectileType projType;
    public SpecialEffect specialEffect;
    public int useAmount;
    public float speed, lifetime, sightRange;
    public GameObject particles;
    public GameObject SFX;
}
