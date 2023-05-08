using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    // Variables that store the enemy's position
    public int xPos;
    public int zPos;
    public int live;
    public TextMeshProUGUI liveText;
    public int stamina;
    public TextMeshProUGUI staminaText;

    private GameManager gameManager;
    private DiceEnemy diceEnemy; // Comunication with DiceEnemy script
    private PlayerController humanPlayer; // variables that are needed to know the position of the player
    private AttackEnemyManager attackEnemyManager;
    public GameObject attackEnemyPanel;

    //-------Look to the humanPlayer------
    [SerializeField] float alertRange = 13; // Enemy alert size
    public LayerMask layerOfPlayer; // El player tiene un Layer Tag de "Player"
    public bool recognizePlayer; //Comprueba si el Player entro al rango de alerta
    public Transform humanPlayerToLook;  // Variable para girar la rotacion(cara) del enemigo.
    public float towardsPlayer; // variable angulo mirada

    public bool attackChance;
    public bool attackExecuted = false;
    public int moveDiceValue; //Dice value available for player movement

    public bool hasPowerUp;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        diceEnemy = GameObject.Find("Enemy Dice").GetComponent<DiceEnemy>();
        humanPlayer = GameObject.Find("Player").GetComponent<PlayerController>();
        attackEnemyManager = GameObject.Find("Attack Enemy Manager").GetComponent<AttackEnemyManager>();

        // Update the start position of the element
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        // Set the space taken to true
        gameManager.spaceTaken[xPos, zPos] = true;
        UpdateLive();
        UpdateStamina();
        hasPowerUp = false;
    }

    void Update()
    {
        DiscoverThePlayer();

        if (gameManager.activeTurn == ActiveTurn.Enemy)
        {
            EnemyAttack();
        }
    }

    public void MoveEnemy(int diceThrow)
    {
        StartCoroutine(MoveEnemyCoroutine(diceThrow));        
    }

    IEnumerator MoveEnemyCoroutine(int diceThrow)
    {
        for (int i = 1; i < diceThrow + 1; i++)            
        {
            EnemyAttack();

            if (!attackChance)
            {
                yield return new WaitForSeconds(0.7f);
                MoveManager();
            }
        }
    }

    void ControlTurn()
    {      
        // Switch turn
        if (moveDiceValue == 0 && !attackChance)
        {
            // Invoke method call the "SetTurn" method of the GameManager after one second
            gameManager.Invoke("SetTurn", 1f);
        }       
    }

    void MoveManager()
    {
        int distanceX = (humanPlayer.xPos - xPos); // Distance in X between the humanPlayer and the enemy
        int distanceZ = (humanPlayer.zPos - zPos); // Distance in z between the humanPlayer and the enemy
        Vector3 moveDirection = Vector3.zero;

        bool foundSpace = false;        

        if (Mathf.Abs(distanceX) > Mathf.Abs(distanceZ))
        {
            int xDirection = distanceX > 0 ? 1 : -1;
            
            if (!gameManager.spaceTaken[xPos + xDirection, zPos])
            {
                foundSpace = true;
                moveDirection = new Vector3(xDirection, 0, 0);
                transform.Translate(moveDirection, Space.World);
                xPos += xDirection;
                if (xDirection > 0)
                {
                    UpdateSpaces(3);
                }
                else
                {
                    UpdateSpaces(4);
                }
            }
        }

        if (!foundSpace)
        {
            int zDirection = distanceZ > 0 ? 1 : -1;
            if (!gameManager.spaceTaken[xPos, zPos + zDirection])
            {
                foundSpace = true;
                moveDirection = new Vector3(0, 0, zDirection);
                transform.Translate(moveDirection, Space.World);
                zPos += zDirection;
                if (zDirection > 0)
                {
                    UpdateSpaces(1);
                }
                else
                {
                    UpdateSpaces(2);
                }
            }
        }

        if (!foundSpace)
        {
            while (!foundSpace)
            {
                int randomAxis = Random.Range(0, 2);
                //int randomDirection = (Random.Range(0, 2) * 2 - 1);

                if (randomAxis == 0)
                {
                    int randomDirection = (distanceX > 0 ? 1 : -1);

                    if (!gameManager.spaceTaken[xPos + randomDirection, zPos])
                    {
                        foundSpace = true;
                        Vector3 randomXDir = new Vector3(randomDirection, 0, 0);
                        transform.Translate(randomXDir, Space.World);
                        xPos += randomDirection;
                        if (randomDirection > 0)
                        {
                            UpdateSpaces(3);
                        }
                        else
                        {
                            UpdateSpaces(4);
                        }
                    }

                }
                else if(randomAxis == 1)
                {
                    int randomDirection = (distanceZ > 0 ? 1 : -1);

                    if (!gameManager.spaceTaken[xPos, zPos + randomDirection])
                    {
                        foundSpace = true;
                        Vector3 randomZDir = new Vector3(0, 0, randomDirection);
                        transform.Translate(randomZDir, Space.World);
                        zPos += randomDirection;
                        if (randomDirection > 0)
                        {
                            UpdateSpaces(1);
                        }
                        else
                        {
                            UpdateSpaces(2);
                        }
                    }

                }
                else
                {
                    // Check a random direction
                    int randomAxis2 = Random.Range(0, 2);

                    if (randomAxis2 == 0)
                    {
                        // Check the X axis
                        int randomDirection = (Random.Range(0, 2) * 2 - 1);
                        if (!gameManager.spaceTaken[xPos + randomDirection, zPos])
                        {
                            foundSpace = true;
                            moveDirection = new Vector3(randomDirection, 0, 0);
                            transform.Translate(moveDirection, Space.World);
                            xPos += randomDirection;
                            if (randomDirection > 0)
                            {
                                UpdateSpaces(3);
                            }
                            else
                            {
                                UpdateSpaces(4);
                            }
                        }
                    }
                    else
                    {
                        // Check the Z axis
                        int randomDirection = (Random.Range(0, 2) * 2 - 1);
                        if (!gameManager.spaceTaken[xPos, zPos + randomDirection])
                        {
                            foundSpace = true;
                            moveDirection = new Vector3(0, 0, randomDirection);
                            transform.Translate(moveDirection, Space.World);
                            zPos += randomDirection;
                            if (randomDirection > 0)
                            {
                                UpdateSpaces(1);
                            }
                            else
                            {
                                UpdateSpaces(2);
                            }
                        }
                    }
                }
            }
        }

        Invoke("ControlTurn", 0.2f);     
    }

    void MoveManagerOld()
    {

        int distanceX = (humanPlayer.xPos - xPos); // Distance in X between the humanPlayer and the enemy
        int distanceZ = (humanPlayer.zPos - zPos); // Distance in z between the humanPlayer and the enemy        

        if (distanceZ < 0 && zPos > 1 && !gameManager.spaceTaken[xPos, zPos - 1])
        {
            transform.Translate(Vector3.back, Space.World);
            zPos--;
            UpdateSpaces(2);
            Invoke("ControlTurn", 0.2f);
        }
        else if (distanceZ > 0 && zPos < 10 && !gameManager.spaceTaken[xPos, zPos + 1])
        {
            transform.Translate(Vector3.forward, Space.World);
            zPos++;
            UpdateSpaces(1);
            Invoke("ControlTurn", 0.2f);
        }
        else if (distanceX < 0 && xPos > 1 && !gameManager.spaceTaken[xPos - 1, zPos])
        {
            transform.Translate(Vector3.left, Space.World);
            xPos--;
            UpdateSpaces(4);
            Invoke("ControlTurn", 0.2f);
        }
        else if (distanceX > 0 && xPos < 10 && !gameManager.spaceTaken[xPos + 1, zPos])
        {
            transform.Translate(Vector3.right, Space.World);
            xPos++;
            UpdateSpaces(3);
            Invoke("ControlTurn", 0.2f);
        }
        else
        {
            int index = Random.Range(1, 4);
            switch (index)
            {
                case 1:
                    if (zPos > 1 && !gameManager.spaceTaken[xPos, zPos - 1])
                    {
                        transform.Translate(Vector3.back, Space.World);
                        zPos--;
                        UpdateSpaces(2);
                        Invoke("ControlTurn", 0.2f);
                    }
                    else
                    {
                        goto case 2;
                    }
                    break;

                case 2:
                    if (zPos < 10 && !gameManager.spaceTaken[xPos, zPos + 1])
                    {
                        transform.Translate(Vector3.forward, Space.World);
                        zPos++;
                        UpdateSpaces(1);
                        Invoke("ControlTurn", 0.2f);
                    }
                    else
                    {
                        goto case 3;
                    }
                    break;

                case 3:
                    if (xPos > 1 && !gameManager.spaceTaken[xPos - 1, zPos])
                    {
                        transform.Translate(Vector3.left, Space.World);
                        xPos--;
                        UpdateSpaces(4);
                        Invoke("ControlTurn", 0.2f);
                    }
                    else
                    {
                        goto case 4;
                    }
                    break;

                case 4:
                    if (xPos < 10 && !gameManager.spaceTaken[xPos + 1, zPos])
                    {
                        transform.Translate(Vector3.right, Space.World);
                        xPos++;
                        UpdateSpaces(3);
                        Invoke("ControlTurn", 0.2f);
                    }
                    else
                    {
                        goto case 1;
                    }
                    break;
            }
        }        
    }

    // Updates the enemy's position to taken
    void UpdateSpaces(int value)
    {
        moveDiceValue--;
        diceEnemy.UpdateDice(moveDiceValue);

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

        // Set the enemy's current square to true
        gameManager.spaceTaken[xPos, zPos] = true;

    }


    //Dibuja el rango de alerta del enemigo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }

    void DiscoverThePlayer()
    {
        recognizePlayer = Physics.CheckSphere(transform.position, alertRange, layerOfPlayer);

        if (recognizePlayer == true)
        {
            Vector3 posHumanPlayer = new Vector3(humanPlayerToLook.position.x, transform.position.y, humanPlayerToLook.position.z);
            //look to the player
            transform.LookAt(posHumanPlayer);
        }
    }


    void EnemyAttack()
    {
        int distanceX = Mathf.Abs(humanPlayer.xPos - xPos); // Absolute value of the distance in X between the humanPlayer and the enemy
        int distanceZ = Mathf.Abs(humanPlayer.zPos - zPos); // Absolute value of the distance in Z between the humanPlayer and the enemy        

        // If player and enemy are in adjacent spaces on X or Z do something
        if ((distanceX == 1 && distanceZ == 0) || (distanceX == 0 && distanceZ == 1))
        {       
            if (!attackExecuted)
            {
                attackChance = true;
                gameManager.activeTurnText.text = "Enemy Attack";
                attackEnemyPanel.gameObject.SetActive(true);
                attackEnemyManager.SetNewEnemyAttack();
                attackExecuted = true;
            }
        }     
    }

    public void UpdateStamina()
    {
        staminaText.gameObject.SetActive(true);
        staminaText.text = "Stamina: " + stamina;
    }

    public void UpdateLive()
    {
        liveText.text = "Human Live: " + live;
    }

    private void OnTriggerEnter(Collider other) //Void to verify if the player collides with the powerups
    {
        if (other.CompareTag("PowerupDamage")) //PowerUp of Damage
        {
            Destroy(other.gameObject);
            live -= 5;
            UpdateLive();
            Debug.Log("Enemy Take powerupDamage");
        }

        if (other.CompareTag("Powerup")) //Powerup of Stamina
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            Debug.Log("Enemy Take powerupStamina");
        }

        if (other.CompareTag("PowerupHealth")) //PowerUp of Heal
        {
            Destroy(other.gameObject);
            Debug.Log("Enemy Take powerupHealth");

            if (live < 20)
            {
                live += 5;
                UpdateLive();
            }
            else
            {
                live = 20;
            }
        }
    }
}
