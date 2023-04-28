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

    void Start()
    {
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
        int throwValue = Random.Range(0, 5);        
        UpdateDice(throwValue);
        playerController.moveDiceValue = throwValue;
        if (throwValue == 0)
        {
            gameManager.SetTurn();
        }
    }

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
                diceNumber.text = "-";
                break;

        }

        
    }

}
