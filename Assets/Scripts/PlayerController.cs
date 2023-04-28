using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables that store the player's position
    public int xPos;
    public int zPos;
    
    public int moveDiceValue; //Dice value available for player movement
    
    private Quaternion currentRotation = Quaternion.identity; //current player rotation
    private DicePlayer dicePlayer; // Comunication with DicePlayer script
    private GameManager gameManager;

    void Start()
    {
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        currentRotation = transform.rotation;
    }

    
    void Update()
    {
        Move();
    }

    //Method in charge of moving and rotating the player depending on the value of the dice
    void Move()
    {
        if (moveDiceValue > 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && zPos < 10)
            {
                currentRotation = Quaternion.Euler(0f, 0f, 0f);
                transform.rotation = currentRotation;               
                transform.Translate(Vector3.forward, Space.World);
                zPos++;
                ControlTurn();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && zPos > 1)
            {
                currentRotation = Quaternion.Euler(0f, 180f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.back, Space.World);
                zPos--;
                ControlTurn();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && xPos < 10)
            {
                currentRotation = Quaternion.Euler(0f, 90f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.right, Space.World);
                xPos++;
                ControlTurn();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && xPos > 1)
            {
                currentRotation = Quaternion.Euler(0f, 270f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.left, Space.World);
                xPos--;
                ControlTurn();
            }
        }       
    }

    void ControlTurn()
    {
        moveDiceValue--;
        dicePlayer.UpdateDice(moveDiceValue);
        if (moveDiceValue == 0)
        {
            gameManager.SetTurn();
        }        
    }
}
