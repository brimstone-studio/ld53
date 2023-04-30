using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Rigidbody RocketRigidBody;
    public GameObject ExplosionPrefab;
    void Start()
    {
        RocketRigidBody.velocity = transform.forward * 20f;
        // Explode the rocket after 6 seconds
        Invoke("Explode", 6.0f);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer != 7)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Instantiate explosion effect at the rocket's position
        if (ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);
        }

        // Destroy the rocket
        Destroy(this.gameObject);
    }
    
}
