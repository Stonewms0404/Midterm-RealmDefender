using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightsSword : MonoBehaviour
{
    [SerializeField]
    private ProjectileScriptableObject knightsSwordSO;
    [SerializeField]
    private Rigidbody2D rb;

    void Update()
    {
        transform.eulerAngles = new(0, 0, Mathf.Lerp(transform.rotation.z, knightsSwordSO.speed * 360, knightsSwordSO.lifetime * Time.deltaTime));
    }
}
