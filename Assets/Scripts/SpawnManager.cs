using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] elementPrefabs;
    public GameObject[] powerupPrefabs;
    public int numElements = 5; // Number of elements on the board
    public int numPowerups = 5; // Number of powerups on the board

    List<Vector3> positions = new List<Vector3>(); // List that stores the positions taken

    void Start()
    {
        SpawnElements();
        SpawnPowerUps();
    }

    // Spawns elements at random positions
    void SpawnElements()
    {       
        for (int i = 0; i < numElements; i++)
        {
            // Generate a random position
            Vector3 randomPosition = new Vector3(Random.Range(2, 10), 0, Random.Range(2, 10));

            // Check if the generated random position is already in the list
            while (positions.Contains(randomPosition))
            {
                // If the position is already in the list it generates a new random position
                randomPosition = new Vector3(Random.Range(2, 10), 0, Random.Range(2, 10));
            }
            
            positions.Add(randomPosition); // Add the random position to the list

            int index = Random.Range(0, elementPrefabs.Length);
            Instantiate(elementPrefabs[index], randomPosition, Quaternion.identity);
        }     
    }

    void SpawnPowerUps()
    {
        for (int i = 0; i < numPowerups; i++)
        {
            // Generate a random position
            Vector3 randomPosition = new Vector3(Random.Range(2, 10), 0, Random.Range(2, 10));

            while (positions.Contains(randomPosition))
            {
                // If the position is already in the list it generates a new random position
                randomPosition = new Vector3(Random.Range(2, 10), 0, Random.Range(2, 10));
            }

            positions.Add(randomPosition); // Add the random position to the list

            int index = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[index], randomPosition, Quaternion.identity);
        }
    }
}
