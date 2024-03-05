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

    public ProjectileType projType;
    public int useAmount;
    public float speed, lifetime;
    public GameObject particles;
}
