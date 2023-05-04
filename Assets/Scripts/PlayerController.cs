using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Variables that store the player's position
    public int xPos;
    public int zPos;    
    public int moveDiceValue; //Dice value available for player movement
    public int live = 20;
    public TextMeshProUGUI liveText;
    public int stamina;
    public TextMeshProUGUI staminaText;
    
    private Quaternion currentRotation = Quaternion.identity; // Current player rotation
    private DicePlayer dicePlayer; // Comunication with DicePlayer script
    private GameManager gameManager;
    private Enemy enemy;
    private AttackPlayer attackPlayerBtn;

    private bool attackChance;

    public Button endTurnBtn;

    //PowerUp
    public bool hasPowerUp = false; //Bool hasPowerUp to know if the player have or not the powerUp

    void Start()
    {
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();        
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        attackPlayerBtn = GameObject.Find("Attack Player Button").GetComponent<AttackPlayer>();
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        gameManager.spaceTaken[xPos, zPos] = true;
        currentRotation = transform.rotation;
        attackPlayerBtn.gameObject.SetActive(false);
        attackChance = false;
        liveText.text = "Live: " + live;
        staminaText.text = "Stamina: " + stamina;
    }

    
    void Update()
    {
        Move();

        if (gameManager.activeTurn == ActiveTurn.Player)
        {
            AttackChance();
        }            
    }

    // Method to moving and rotating the player depending on the value of the dice
    void Move()
    {
        if (moveDiceValue > 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && zPos < 10 && !gameManager.spaceTaken[xPos, zPos + 1])
            {
                currentRotation = Quaternion.Euler(0f, 0f, 0f); //Rotation value to forward
                transform.rotation = currentRotation; // Set forward rotation               
                transform.Translate(Vector3.forward, Space.World); // Move the player one step forward
                zPos++;
                UpdateSpaces(1);
                Invoke("ControlTurn", 0.2f); // Execute method with 0.2 second delay
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && zPos > 1 && !gameManager.spaceTaken[xPos, zPos - 1])
            {
                currentRotation = Quaternion.Euler(0f, 180f, 0f); // Rotation value to back
                transform.rotation = currentRotation;
                transform.Translate(Vector3.back, Space.World);
                zPos--;
                UpdateSpaces(2);
                Invoke("ControlTurn", 0.2f);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && xPos < 10 && !gameManager.spaceTaken[xPos + 1, zPos])
            {
                currentRotation = Quaternion.Euler(0f, 90f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.right, Space.World);
                xPos++;
                UpdateSpaces(3);
                Invoke("ControlTurn", 0.2f);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && xPos > 1 && !gameManager.spaceTaken[xPos -1, zPos])
            {
                currentRotation = Quaternion.Euler(0f, 270f, 0f);
                transform.rotation = currentRotation;
                transform.Translate(Vector3.left, Space.World);
                xPos--;
                UpdateSpaces(4);
                Invoke("ControlTurn", 0.2f);
            }
        }       
    }

    // Updates the player's position to taken
    void UpdateSpaces(int value)
    {
        // Set space left to false
        switch (value)
        {
            case 1:
                gameManager.spaceTaken[xPos, zPos - 1] = false;
                break;
            case 2:
                gameManager.spaceTaken[xPos, zPos + 1] = false;
                break;
            case 3:
                gameManager.spaceTaken[xPos - 1, zPos] = false;
                break;
            case 4:
                gameManager.spaceTaken[xPos + 1, zPos] = false;
                break;
        }

        // Set the player's current square to true
        gameManager.spaceTaken[xPos, zPos] = true;

        //// Set all spaces to false
        //for (int x = 1; x < 11; x++)
        //{
        //    for (int y = 1; y < 11; y++)
        //    {
        //        gameManager.spaceTaken[x, y] = false;
        //    }
        //}
    }

    // Check the distance with the enemy to detect attack chance
    void AttackChance()
    {
        int distanceX = Mathf.Abs(xPos - enemy.xPos); // Absolute value of the distance in X between the player and the enemy
        int distanceZ = Mathf.Abs(zPos - enemy.zPos); // Absolute value of the distance in Z between the player and the enemy        
        
        // If player and enemy are in adjacent spaces on X or Z do something
        if ((distanceX == 1 && distanceZ == 0) || (distanceX == 0 && distanceZ == 1))
        {            
            attackChance = true;
            attackPlayerBtn.gameObject.SetActive(true);
        }
        else
        {
            attackChance = false;
            attackPlayerBtn.gameObject.SetActive(false);
        }
        
    }

    // Switch turn
    void ControlTurn()
    {
        moveDiceValue--;
        dicePlayer.UpdateDice(moveDiceValue); // Update the UI display of the player dice

        
        if (moveDiceValue == 0 && !attackChance)
        {
            // Invoke method call the "SetTurn" method of the GameManager after one second
            gameManager.Invoke("SetTurn", 1f);
        }
        else if(moveDiceValue == 0 && attackChance)
        {
            endTurnBtn.gameObject.SetActive(true);
        }
    }

    public void EndTurn()
    {
        endTurnBtn.gameObject.SetActive(false);
        attackPlayerBtn.gameObject.SetActive(false);
        gameManager.SetTurn();
    }

    public void UpdateStamina()
    {
        staminaText.text = "Stamina: " + stamina;
    }

    private void OnTriggerEnter(Collider other) //Void to verify if the player collides with the powerups
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            gameManager.GameOver();
        }
    }
}

