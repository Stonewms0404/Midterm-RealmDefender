using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuParticleSystem : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    void Awake()
    {
        particles.Play(true);
    }
}
