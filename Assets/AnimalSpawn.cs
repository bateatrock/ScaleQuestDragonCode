using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int numberOfPrefabs = 10;
    public Vector3 cubeSize = new Vector3(10f, 10f, 10f);

    void Start()
    {
        SpawnPrefabsRandomly();
    }

    void SpawnPrefabsRandomly()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 randomPosition = GetRandomPositionWithinCube();

            // Instantiate the prefab at the random position
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionWithinCube()
    {
        float randomX = Random.Range(transform.position.x - cubeSize.x / 2f, transform.position.x + cubeSize.x / 2f);
        float randomY = Random.Range(transform.position.y - cubeSize.y / 2f, transform.position.y + cubeSize.y / 2f);
        float randomZ = Random.Range(transform.position.z - cubeSize.z / 2f, transform.position.z + cubeSize.z / 2f);

        return new Vector3(randomX, randomY, randomZ);
    }
}
