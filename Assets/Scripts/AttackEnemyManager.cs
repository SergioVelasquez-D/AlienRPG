using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttackEnemyManager : MonoBehaviour
{
    public TextMeshProUGUI powerupText;
    public Button powerupBtn;

    public TextMeshProUGUI enemyDiceText;

    public Button powerupEnemyBtn;
    public TextMeshProUGUI powerupEnemyText;
    public Button moveToStamEnemyBtn;
    public TextMeshProUGUI moveToStamEnemyText;
    public TextMeshProUGUI moveToStamEnemyDiceText;

    public Button launchEnemyBtn;

    private Enemy enemy;
    private PlayerController playerController;
    private GameManager gameManager;
    private AttackDicePlayer attackDicePlayer;

    void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        attackDicePlayer = GameObject.Find("Attack Dice Player 2").GetComponent<AttackDicePlayer>();

        StaminaEnemy();
        MoveEnemyToStamina();
        EnemyHasPowerup();
        ShowPowerup();        
    }

    private void OnEnable()
    {        
        StaminaEnemy();
        MoveEnemyToStamina();
        EnemyHasPowerup();
        ShowPowerup();
        launchEnemyBtn.gameObject.SetActive(true);
    }

    void StaminaEnemy()
    {
        int staminaEnemy = Random.Range(1, 7);
        enemy.stamina = staminaEnemy;
        enemy.UpdateStamina();
        UpdateEnemyDice(staminaEnemy);
    }

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

    public void Powerup()
    {
        playerController.stamina *= 2;
        playerController.UpdateStamina();
        playerController.hasPowerUp = false;
        powerupBtn.gameObject.SetActive(false);
        powerupText.gameObject.SetActive(false);
    }

    public void MoveEnemyToStamina()
    {
        if (enemy.moveDiceValue > 0)
        {
            enemy.stamina += enemy.moveDiceValue;
            enemy.UpdateStamina();
            UpdateEnemyMoveDice(enemy.moveDiceValue);
            moveToStamEnemyBtn.gameObject.SetActive(true);
            moveToStamEnemyText.gameObject.SetActive(true);
        }
        else
        {
            moveToStamEnemyBtn.gameObject.SetActive(false);
            moveToStamEnemyText.gameObject.SetActive(false);
        }
    }

    void UpdateEnemyMoveDice(int numberToShow)
    {
        switch (numberToShow)
        {
            case 1:
                moveToStamEnemyDiceText.text = "•";
                break;
            case 2:
                moveToStamEnemyDiceText.text = "••";
                break;
            case 3:
                moveToStamEnemyDiceText.text = "•••";
                break;
            case 4:
                moveToStamEnemyDiceText.text = "••\n••";
                break;
            default:
                moveToStamEnemyDiceText.text = "";
                break;

        }
    }

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
            enemy.live -= 1;
            playerController.stamina = 0;
            enemy.stamina = 0;
            UpdateData();
            Debug.Log("Tie attack, player lose 1 live");
        }

        launchEnemyBtn.gameObject.SetActive(false);
        gameObject.SetActive(false);
        gameManager.SetTurn();
        enemy.attackChance = false;
    }

    void UpdateData()
    {
        playerController.UpdateStamina();
        playerController.UpdateLive();
        enemy.UpdateStamina();
        enemy.UpdateLive();
        attackDicePlayer.hasThrow = false;
        attackDicePlayer.UpdateDice(0);
    }

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
}
