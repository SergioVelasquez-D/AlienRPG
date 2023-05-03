using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    public TextMeshProUGUI moveToStamText;
    public TextMeshProUGUI powerupText;
    public Button moveToStamBtn;
    public Button powerupBtn;
    private PlayerController playerController;
    private DicePlayer dicePlayer;


    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        ShowMoveToStamina();
        Debug.Log("Attack Panel Activated");
    }

    void ShowMoveToStamina()
    {
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

    public void Powerup()
    {
        playerController.stamina *= 2;
        playerController.UpdateStamina();
        powerupBtn.gameObject.SetActive(false);
        powerupText.gameObject.SetActive(false);
    }
}
