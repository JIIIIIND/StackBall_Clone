using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticle : MonoBehaviour
{
    private ParticleSystem _particle;

    void Start()
    {
        _particle = this.GetComponent<ParticleSystem>();
        _particle.Play();
    }

    void Update()
    {
        if (_particle.isStopped)
            Destroy(this.gameObject);
    }
}
