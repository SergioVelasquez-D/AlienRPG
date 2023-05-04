using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

enum AttackStatus { firstAttack, newAttack }

public class AttackManager : MonoBehaviour
{
    public TextMeshProUGUI moveToStamText;
    public TextMeshProUGUI powerupText;
    public Button moveToStamBtn;
    public Button powerupBtn;
    public Button launchBtn;
    private PlayerController playerController;
    private DicePlayer dicePlayer;
    private AttackDicePlayer attackDicePlayer;
    private Enemy enemy;

    private AttackStatus attackStatus;


    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        attackDicePlayer = GameObject.Find("Attack Dice Player").GetComponent<AttackDicePlayer>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        ShowMoveToStamina();
        ShowPowerup();
        StaminaEnemy();
    }

    private void OnEnable()
    {
        ShowMoveToStamina();
        ShowPowerup();
        launchBtn.gameObject.SetActive(true);
        StaminaEnemy();
    }

    void StaminaEnemy()
    {
        int staminaEnemy = Random.Range(1, 7);
        enemy.stamina = staminaEnemy;
        enemy.UpdateStamina();
    }

    public void ShowMoveToStamina()
    {
        Debug.Log("Player Move Dice Value: " + playerController.moveDiceValue);
        if (playerController.moveDiceValue > 0)
        {
            moveToStamText.gameObject.SetActive(true);
            moveToStamBtn.gameObject.SetActive(true);
        }        
    }

    public void MoveToStamina()
    {
        playerController.stamina += playerController.moveDiceValue;
        playerController.moveDiceValue = 0;
        playerController.UpdateStamina();
        dicePlayer.UpdateDice(playerController.moveDiceValue);
        moveToStamText.gameObject.SetActive(false);
        moveToStamBtn.gameObject.SetActive(false);
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
        powerupBtn.gameObject.SetActive(false);
        powerupText.gameObject.SetActive(false);
    }

    public void LaunchAttack()
    {
        int enemyStam = enemy.stamina;
        int playerStam = playerController.stamina;

        if (playerStam > enemyStam)
        {
            playerController.stamina -= enemyStam;
            enemy.live -= playerStam - enemyStam;
            enemy.stamina = 0;
            UpdateData();            
            Debug.Log("Player Win");
        }
        else if (playerStam <= enemyStam)
        {
            enemy.stamina -= playerStam;
            playerController.live -= enemyStam - playerStam;
            playerController.stamina = 0;
            UpdateData();
            Debug.Log("Enemy Win");
        }

        launchBtn.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    void UpdateData()
    {
        playerController.UpdateStamina();
        playerController.UpdateLive();
        enemy.UpdateStamina();
        enemy.UpdateLive();
        attackDicePlayer.hasThrow = false;
        attackDicePlayer.UpdateDice(0);
        if (playerController.moveDiceValue == 0)
        {
            playerController.endTurnBtn.gameObject.SetActive(true);
        }
    }
}
