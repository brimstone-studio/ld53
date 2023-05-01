using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    public float BlastRadius = 5f;
    public float ExplosionForce = 60f;
    public float ExplosionDuration = 2f;
    public int MaxRobotDamage = 50;

    void Start()
    {
        // Get all colliders within the blast radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, BlastRadius);

        // Apply force to all RigidBodies within the blast radius
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && !hit.CompareTag("Rocket"))
            {
                // Calculate force based on distance from the explosion center
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                float forceMultiplier = 1.0f - Mathf.Clamp01(distance / BlastRadius);

                // Apply the explosion force
                rb.AddExplosionForce(ExplosionForce * forceMultiplier, transform.position, BlastRadius, 0f, ForceMode.Impulse);
                
                // Apply damage to the robot
                RobotHealth robot = hit.GetComponent<RobotHealth>();
                if (robot != null)
                {
                    int damage = _calculateDamage(distance);
                    robot.Heath -= damage;
                }
            }
        }

        // Destroy the explosion object after the specified duration
        Destroy(gameObject, ExplosionDuration);
    }
    
    private int _calculateDamage(float distance)
    {
        // Calculate damage based on distance from the explosion center
        float damageMultiplier = 1.0f - Mathf.Clamp01(distance / BlastRadius);
        return Mathf.RoundToInt(MaxRobotDamage * damageMultiplier);
    }
}