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
<<<<<<< Updated upstream
=======
    private DiceEnemy diceEnemy; // Comunication with DiceEnemy script
    private PlayerController humanPlayer; // variables that are needed to know the position of the player
    public GameObject attackEnemyPanel;

    //-------Look to the humanPlayer------
    [SerializeField] float alertRange = 13; // tamaño de alerta del enemigo
    public LayerMask layerOfPlayer; // El player tiene un Layer Tag de "Player"
    public bool recognizePlayer; //Comprueba si el Player entro al rango de alerta
    public Transform humanPlayerToLook;  // Variable para girar la rotacion(cara) del enemigo.
    public float towardsPlayer; // variable angulo mirada

    public bool attackChance;
    public int moveDiceValue; //Dice value available for player movement
>>>>>>> Stashed changes

    public bool hasPowerUp;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
<<<<<<< Updated upstream
=======
        diceEnemy = GameObject.Find("Enemy Dice").GetComponent<DiceEnemy>();
        humanPlayer = GameObject.Find("Player").GetComponent<PlayerController>();        
>>>>>>> Stashed changes

        // Update the start position of the element
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        // Set the space taken to true
        gameManager.spaceTaken[xPos, zPos] = true;
        UpdateLive();
        UpdateStamina();
        hasPowerUp = false;
    }

<<<<<<< Updated upstream
=======
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
            attackChance = true;
            gameManager.activeTurnText.text = "Enemy Attack";
            attackEnemyPanel.gameObject.SetActive(true);
        }     
    }

>>>>>>> Stashed changes
    public void UpdateStamina()
    {
        staminaText.text = "Stamina: " + stamina;
    }

    public void UpdateLive()
    {
        liveText.text = "Live: " + live;
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
