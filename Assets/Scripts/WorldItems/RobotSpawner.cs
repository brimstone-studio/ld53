using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    public GameObject robotPrefab;
    public Vector3 boxSize = new Vector3(10f, 1f, 10f);

    void Start()
    {
        // the spawners add themselves to the gamemode manager
        GamemodeManager.Instance.Spawners.Add(this);
    }
    
    public void Spawn(int numberOfRobots)
    {
        for (int i = 0; i < numberOfRobots; i++)
        {
            Vector3 spawnPosition = GetRandomPositionInBounds();
            var robot = Instantiate(robotPrefab, spawnPosition, Quaternion.identity);
            robot.SetActive(true);
        }
    }

    private Vector3 GetRandomPositionInBounds()
    {
        float x = Random.Range(-boxSize.x / 2f, boxSize.x / 2f);
        float y = Random.Range(-boxSize.y / 2f, boxSize.y / 2f);
        float z = Random.Range(-boxSize.z / 2f, boxSize.z / 2f);

        return transform.position + new Vector3(x, y, z);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
        Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, boxSize);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
#endif
}