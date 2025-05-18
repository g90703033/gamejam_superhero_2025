using UnityEngine;
using System.Collections;

public class RandomTimedSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector2 spawnAreaMin = new Vector2(-10f, -10f);
    public Vector2 spawnAreaMax = new Vector2(10f, 10f);
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float z = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 spawnPosition = new Vector3(x, 0f, z) + transform.position; // assuming Y = 0
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
