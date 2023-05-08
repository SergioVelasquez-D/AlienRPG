using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class AttackManager : MonoBehaviour
{
    public TextMeshProUGUI moveToStamText;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI enemyDiceText;
    public TextMeshProUGUI diceNumberMoveToSpam;
    public Button moveToStamBtn;
    public Button powerupBtn;
    public Button launchBtn;
    public Button powerupEnemyBtn;
    public TextMeshProUGUI powerupEnemyText;
    public GameObject attackPanel;
    private PlayerController playerController;
    private DicePlayer dicePlayer;
    private AttackDicePlayer attackDicePlayer;
    private Enemy enemy;

    private bool hasThrowMoveToSpam;


    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();        
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    public void SetNewAttack()
    {
        attackDicePlayer = GameObject.Find("Attack Dice Player").GetComponent<AttackDicePlayer>();
        ShowMoveToStamina();        
        ShowPowerup();
        launchBtn.gameObject.SetActive(true);
        StaminaEnemy();
        EnemyHasPowerup();
    }

    // Generates random stamina value of the enemy
    void StaminaEnemy()
    {
        int staminaEnemy = Random.Range(1, 7);
        enemy.stamina = staminaEnemy;
        enemy.UpdateStamina();
        UpdateEnemyDice(staminaEnemy);
    }

    // Displays the stamina value of the enemy on the dice
    void UpdateEnemyDice(int numberToShow)
    {
        switch (numberToShow)
        {
            case 1:
                enemyDiceText.text = "•";
                break;
            case 2:
                enemyDiceText.text = "••";
                break;
            case 3:
                enemyDiceText.text = "•••";
                break;
            case 4:
                enemyDiceText.text = "••\n••";
                break;
            case 5:
                enemyDiceText.text = "••\n•\n••";
                break;
            case 6:
                enemyDiceText.text = "•••\n•••";
                break;
            default:
                enemyDiceText.text = "";
                break;

        }
    }

    // Check if the enemy has the stamina powerup and apply the powerup action
    public void EnemyHasPowerup()
    {
        if (enemy.hasPowerUp)
        {
            powerupEnemyBtn.gameObject.SetActive(true);
            powerupEnemyText.gameObject.SetActive(true);
            enemy.stamina *= 2;
            enemy.UpdateStamina();
            enemy.hasPowerUp = false;
        }
        else
        {
            powerupEnemyBtn.gameObject.SetActive(false);
            powerupEnemyText.gameObject.SetActive(false);
        }
    }

    // Check if the player has movement points and show the dice to convert them to stamina points
    public void ShowMoveToStamina()
    {        
        if (playerController.moveDiceValue > 0)
        {
            moveToStamText.gameObject.SetActive(true);
            moveToStamBtn.gameObject.SetActive(true);
        }        
    }

    // Converts the player's movement points to stamina points
    public void MoveToStamina()
    {
        if (!hasThrowMoveToSpam)
        {
            int movePoints = playerController.moveDiceValue;
            playerController.stamina += playerController.moveDiceValue;
            playerController.moveDiceValue = 0;
            playerController.UpdateStamina();
            dicePlayer.UpdateDice(playerController.moveDiceValue);
            UpdateMoveToStamDice(movePoints);
            hasThrowMoveToSpam = true;
        }             
    }

    public void UpdateMoveToStamDice(int numberToShow)
    {
        switch (numberToShow)
        {
            case 1:
                diceNumberMoveToSpam.text = "•";
                break;
            case 2:
                diceNumberMoveToSpam.text = "••";
                break;
            case 3:
                diceNumberMoveToSpam.text = "•••";
                break;
            case 4:
                diceNumberMoveToSpam.text = "••\n••";
                break;
            default:
                diceNumberMoveToSpam.text = "";
                break;
        }
    }

    // Check if the player has the stamina powerup
    public void ShowPowerup()
    {
        if (playerController.hasPowerUp)
        {
            powerupBtn.gameObject.SetActive(true);
            powerupText.gameObject.SetActive(true);
        }
        else
        {
            powerupBtn.gameObject.SetActive(false);
            powerupText.gameObject.SetActive(false);
        }
    }

    // Apply the powerup action to player
    public void Powerup()
    {
        playerController.stamina *= 2;
        playerController.UpdateStamina();
        playerController.hasPowerUp = false;
        powerupBtn.gameObject.SetActive(false);
        powerupText.gameObject.SetActive(false);
    }

    public void LaunchAttack()
    {
        int enemyStam = enemy.stamina;
        int playerStam = playerController.stamina;

        if (playerStam > enemyStam)
        {            
            enemy.live -= playerStam - enemyStam;
            enemy.stamina = 0;
            playerController.stamina = 0;
            UpdateData();            
            Debug.Log("Player Win");
        }
        else if (playerStam < enemyStam)
        {
            playerController.live -= enemyStam - playerStam;
            playerController.stamina = 0;
            enemy.stamina = 0;
            UpdateData();
            Debug.Log("Enemy Win");
        }
        else
        {
            playerController.live -= 1;
            playerController.stamina = 0;
            enemy.stamina = 0;
            UpdateData();
            Debug.Log("Tie attack, player lose 1 live");
        }

        launchBtn.gameObject.SetActive(false);
        attackPanel.gameObject.SetActive(false);
    }

    void UpdateData()
    {
        playerController.UpdateStamina();
        playerController.UpdateLive();
        enemy.UpdateStamina();
        enemy.UpdateLive();
        attackDicePlayer.hasThrow = false;
        attackDicePlayer.UpdateDice(0);
        UpdateMoveToStamDice(0);
        moveToStamText.gameObject.SetActive(false);
        moveToStamBtn.gameObject.SetActive(false);
        hasThrowMoveToSpam = false;

        if (playerController.moveDiceValue == 0)
        {
            playerController.endTurnBtn.gameObject.SetActive(true);
        }
    }
}
