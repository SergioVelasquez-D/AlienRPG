using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables that store the player's position
    public int xPos;
    public int zPos;
    
    public int moveDiceValue; //Dice value available for player movement
    
    private Quaternion currentRotation = Quaternion.identity; //Current player rotation
    private DicePlayer dicePlayer; // Comunication with DicePlayer script
    private GameManager gameManager;

    //PowerUp



    public bool hasPowerUp = false; //Bool hasPowerUp to know if the player have or not the powerUp

    void Start()
    {
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        gameManager.spaceTaken[xPos, zPos] = true;
        currentRotation = transform.rotation;
    }

    
    void Update()
    {
        Move();
    }

    // Method to moving and rotating the player depending on the value of the dice
    void Move()
    {
        if (moveDiceValue > 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && zPos < 10)
            {
                currentRotation = Quaternion.Euler(0f, 0f, 0f); //Rotation value to forward
                transform.rotation = currentRotation; // Set forward rotation               
                transform.Translate(Vector3.forward, Space.World); // Move the player one step forward
                zPos++;
                UpdateSpaces();                
                ControlTurn();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && zPos > 1)
            {
                currentRotation = Quaternion.Euler(0f, 180f, 0f); // Rotation value to back
                transform.rotation = currentRotation;
                transform.Translate(Vector3.back, Space.World);
                zPos--;
                UpdateSpaces();
                ControlTurn();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && xPos < 10)
            {
                currentRotation = Quaternion.Euler(0f, 90f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.right, Space.World);
                xPos++;
                UpdateSpaces();
                ControlTurn();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && xPos > 1)
            {
                currentRotation = Quaternion.Euler(0f, 270f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.left, Space.World);
                xPos--;
                UpdateSpaces();
                ControlTurn();
            }
        }       
    }

    // Updates the player's position to taken
    void UpdateSpaces()
    {
        // Set all spaces to false
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    gameManager.spaceTaken[x, y] = false;
                }
            }
        }

        // Set the player's current square to true
        gameManager.spaceTaken[xPos, zPos] = true;
    }

    void ControlTurn()
    {
        moveDiceValue--;
        dicePlayer.UpdateDice(moveDiceValue); // Update the UI display of the player dice

        // Switch turn
        if (moveDiceValue == 0)
        {
            // Invoke method call the "SetTurn" method of the GameManager after one second
            gameManager.Invoke("SetTurn", 1f);
        }        
    }

    private void OnTriggerEnter(Collider other) //Void to verify if the player collides with the powerups
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
        }
    }
}

