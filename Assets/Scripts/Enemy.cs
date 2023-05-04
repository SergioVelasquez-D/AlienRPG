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

    //-------Look to the humanPlayer------
    [SerializeField] float alertRange = 13; // tamaño de alerta del enemigo
    public LayerMask layerOfPlayer; // El player tiene un Layer Tag de "Player"
    public bool recognizePlayer; //Comprueba si el Player entro al rango de alerta
    public Transform humanPlayerToLook;  // Variable para girar la rotacion(cara) del enemigo.
    public float towardsPlayer; // variable angulo mirada


    public int moveDiceValue; //Dice value available for player movement

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        diceEnemy = GameObject.Find("Enemy Dice").GetComponent<DiceEnemy>();
        humanPlayer = GameObject.Find("Player").GetComponent<PlayerController>();

        // Update the start position of the element
        xPos = (int)transform.position.x;
        zPos = (int)transform.position.z;
        // Set the space taken to true
        gameManager.spaceTaken[xPos, zPos] = true;
        UpdateLive();
        UpdateStamina();
    }

    void Update()
    {
        DiscoverThePlayer();
        MoveEnemy();
        EnemyAttack();
    }

    void MoveEnemy()
    {

        if (moveDiceValue > 0)
        {
            Debug.Log("VALOR DADO ENEMIGO: " + moveDiceValue);
            for (int i = 0; i < moveDiceValue; i++)
            {
                Invoke("MovimientoEnemigo", 0.3f);
                ControlTurn();
            }

        }

    }

    void ControlTurn()
    {
        moveDiceValue--;
        diceEnemy.UpdateDice(moveDiceValue); // Update the UI display of the player dice

        // Switch turn
        if (moveDiceValue == 0) //&& !attackChance)
        {
            // Invoke method call the "SetTurn" method of the GameManager after one second
            gameManager.Invoke("SetTurn", 1f);
        }
        /*else if(moveDiceValue == 0) //&& attackChance)
        {
            endTurnBtn.gameObject.SetActive(true);
        }*/
    }

    void MovimientoEnemigo()
    {
        
        int distanceX = (humanPlayer.xPos - xPos); // Distance in X between the humanPlayer and the enemy
        int distanceZ = (humanPlayer.zPos - zPos); // Distance in z between the humanPlayer and the enemy

        if ((distanceX > 1 ) || (distanceX == 1 && distanceZ == -1) || (distanceX == 1 && distanceZ == 1) )
        {
            if (!(gameManager.spaceTaken[xPos+1, zPos])){
            transform.Translate(1, 0, 0, Space.World); // Move the enemy one step right
            xPos++;
            UpdateSpaces(3);
            }
            else if (zPos > 1 && (!(gameManager.spaceTaken[xPos+1, zPos-1]))){
                transform.Translate(0, 0, -1, Space.World);
                zPos--;
                transform.Translate(1, 0, 0, Space.World);
                xPos++;
                UpdateSpaces(5);
            }
            else if (zPos < 10 && (!(gameManager.spaceTaken[xPos+1, zPos+1]))){
                transform.Translate(0, 0, 1, Space.World);
                zPos++;
                transform.Translate(1, 0, 0, Space.World);
                xPos++;
                UpdateSpaces(6);
            }
        }
        else if (distanceX < -1 || (distanceX == -1 && distanceZ == -1) || (distanceX == -1 && distanceZ == 1))
        {
            if (!gameManager.spaceTaken[xPos-1, zPos]){
            transform.Translate(-1, 0, 0, Space.World); // Move the enemy one step to left
            xPos--;
            UpdateSpaces(4);
            }
            else if (zPos > 1 && (!(gameManager.spaceTaken[xPos-1, zPos-1]))){
                transform.Translate(0, 0, -1, Space.World);
                zPos--;
                transform.Translate(-1, 0, 0, Space.World);
                xPos--;
                UpdateSpaces(7);
            }
            else if (zPos < 10 && (!(gameManager.spaceTaken[xPos-1, zPos+1]))){
                transform.Translate(0, 0, 1, Space.World);
                zPos++;
                transform.Translate(-1, 0, 0, Space.World);
                xPos--;
                UpdateSpaces(8);
            }
        }
        else if (distanceZ > 1 || (distanceZ == 1 && distanceX == 1) || (distanceZ == 1 && distanceX == - 1))
        {
            if (!gameManager.spaceTaken[zPos+1, xPos]){
            transform.Translate(0, 0, 1, Space.World); // Move the enemy one step backward
            zPos++;
            UpdateSpaces(1);
            }
            else if (xPos > 1 && (!(gameManager.spaceTaken[xPos-1, zPos+1]))){
                transform.Translate(-1, 0, 0, Space.World);
                xPos--;
                transform.Translate(0, 0, 1, Space.World);
                zPos++;
                UpdateSpaces(8);
            }
            else if (xPos < 10 && (!(gameManager.spaceTaken[xPos+1, zPos+1]))){
                transform.Translate(1, 0, 0, Space.World);
                xPos++;
                transform.Translate(0, 0, 1, Space.World);
                zPos++;
                UpdateSpaces(6);
            }
        }
        else if (distanceZ < -1 || (distanceZ == -1 && distanceX == 1) || (distanceZ == -1 && distanceX == - 1))
        {
            if (!gameManager.spaceTaken[zPos-1, xPos]){
            transform.Translate(0, 0, -1, Space.World); // Move the enemy one step forward
            zPos--;
            UpdateSpaces(2);
            }
            else if (xPos > 1 && (!(gameManager.spaceTaken[xPos-1, zPos-1]))){
                transform.Translate(-1, 0, 0, Space.World);
                xPos--;
                transform.Translate(0, 0, -1, Space.World);
                zPos--;
                UpdateSpaces(7);
            }
            else if (xPos < 10 && (!(gameManager.spaceTaken[xPos+1, zPos-1]))){
                transform.Translate(1, 0, 0, Space.World);
                xPos++;
                transform.Translate(0, 0, -1, Space.World);
                zPos--;
                UpdateSpaces(5);
            }
        }
    }

    // Updates the enemy's position to taken
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
            case 5:
                gameManager.spaceTaken[xPos+1, zPos - 1] = false;
                break;
            case 6:
                gameManager.spaceTaken[xPos+1, zPos + 1] = false;
                break;
            case 7:
                gameManager.spaceTaken[xPos - 1, zPos-1] = false;
                break;
            case 8:
                gameManager.spaceTaken[xPos - 1, zPos+1] = false;
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

    void DiscoverThePlayer(){
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
            //attackChance = true;
            int throwValue = Random.Range(1, 7);
            //aqui se activaba el texto de ataque y daba el valor, podria ser el metodo UpdateStamina
        }
        else
        {
            //staminaText.gameObject.SetActive(false);
            //se termina el turno??
        }
        
    }

    public void UpdateStamina()
    {
        staminaText.gameObject.SetActive(true);
        staminaText.text = "Stamina: " + stamina;
        
    }

    public void UpdateLive()
    {
        liveText.text = "Live: " + live;
    }
}
