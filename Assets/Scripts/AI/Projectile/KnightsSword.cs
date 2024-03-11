using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightsSword : MonoBehaviour
{
    public static event System.Action<GameObject, Vector2> _SpawnSFX;

    [SerializeField]
    private ProjectileScriptableObject knightsSwordSO;
    [SerializeField]
    private Rigidbody2D rb;

    float timer;
    private void Start()
    {
        _SpawnSFX(knightsSwordSO.SFX, transform.position);
    }

    void Update()
    {
        transform.eulerAngles = new(0, 0, Mathf.Lerp(transform.eulerAngles.z, 90 * knightsSwordSO.speed, knightsSwordSO.lifetime * Time.deltaTime));

        if (timer < knightsSwordSO.lifetime)
            timer += Time.deltaTime;
        else
            Destroy(gameObject);
    }

    public int GetUseAmount()
    {
        return knightsSwordSO.useAmount;
    }
}
