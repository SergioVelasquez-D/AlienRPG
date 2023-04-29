using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DicePlayer : MonoBehaviour
{
    private Button diceButton;
    private PlayerController playerController;
    public TextMeshProUGUI diceNumber;
    private GameManager gameManager;
    public bool hasThrow; // Variable to avoid multiple rolls of the dice

    void Start()
    {
        hasThrow = false;
        diceButton = GetComponent<Button>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        diceButton.onClick.AddListener(DiceThrow);
    }

    
    void Update()
    {
        
    }

    void DiceThrow()
    {
        // Check if the dice has already been rolled this turn
        if (hasThrow)
        {
            // if "hasThrownDice" is true, the "DiceThrow" method will not throw the dice again and will simply end its execution
            return;
        }
        int throwValue = Random.Range(0, 5);     
        UpdateDice(throwValue);
        playerController.moveDiceValue = throwValue;
        hasThrow = true;

        // Switch turn
        if (throwValue == 0) 
        {
            gameManager.Invoke("SetTurn", 1f);
        }
    }

    // Method to update the UI display of the player dice

    public void UpdateDice(int numberToShow)
    {
        switch (numberToShow) 
        {
            case 1:
                diceNumber.text = "•";
                break;
            case 2:
                diceNumber.text = "••";
                break;
            case 3:
                diceNumber.text = "•••";
                break;
            case 4:
                diceNumber.text = "••\n••";
                break;
            default:
                diceNumber.text = "";
                break;

        }        
    }

}
