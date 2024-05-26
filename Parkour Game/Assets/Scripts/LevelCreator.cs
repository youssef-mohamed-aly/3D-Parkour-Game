using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{    
    public GameObject[] pillarPrefabs;  // Array of pillar prefabs
    public GameObject[] wallPrefabs;    // Array of wall prefabs
    public GameObject finishPrefab;     // Finish point prefab
    public GameObject startPrefab;      // Start point prefab
    public GameObject groundPrefab;     // Ground prefab

    public int numberOfPillars = 10;    // Number of pillars to spawn
    public int numberOfWalls = 5;       // Number of walls to spawn

    public Vector3 levelSize = new Vector3(50, 10, 50); // Size of the level area
    public Vector3 startPosition = new Vector3(0, 0, 0); // Position for the start point

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        // Generate ground
        Vector3 groundPosition = new Vector3(0, -1, 0);
        Vector3 groundScale = new Vector3(levelSize.x, 1, levelSize.z);
        GameObject ground = Instantiate(groundPrefab, groundPosition, Quaternion.identity);
        ground.transform.localScale = groundScale;
        ground.transform.parent = transform; // Set the parent to keep the hierarchy clean

        // Generate pillars
        for (int i = 0; i < numberOfPillars; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject pillar = Instantiate(pillarPrefabs[Random.Range(0, pillarPrefabs.Length)], randomPosition, Quaternion.identity);
            pillar.transform.parent = transform; // Set the parent to keep the hierarchy clean
        }

        // Generate walls
        for (int i = 0; i < numberOfWalls; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], randomPosition, Quaternion.identity);
            wall.transform.parent = transform; // Set the parent to keep the hierarchy clean
        }

        // Generate finish point
        Vector3 finishPosition = GetRandomPosition();
        GameObject finish = Instantiate(finishPrefab, finishPosition, Quaternion.identity);
        finish.transform.parent = transform; // Set the parent to keep the hierarchy clean

        // Generate start point
        GameObject start = Instantiate(startPrefab, startPosition, Quaternion.identity);
        start.transform.parent = transform; // Set the parent to keep the hierarchy clean
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-levelSize.x / 2, levelSize.x / 2);
        float y = Random.Range(0, levelSize.y);
        float z = Random.Range(-levelSize.z / 2, levelSize.z / 2);
        return new Vector3(x, y, z);
    }
}
