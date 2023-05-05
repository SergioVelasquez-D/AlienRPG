using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{

    // Variables that store the element's position
    public int xPos;
    public int zPos;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Update the start position of the element
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        // Set the space taken to true
        gameManager.spaceTaken[xPos, zPos] = true;
        //gameManager.PrintTakenSpaces();
    }

}
