using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackDicePlayer : MonoBehaviour
{
    private Button attackDiceBtn;
    public TextMeshProUGUI diceNumber;
    public bool hasThrow; // Variable to avoid multiple rolls of the dice

    void Start()
    {
        hasThrow = false;
        attackDiceBtn = GetComponent<Button>();
        attackDiceBtn.onClick.AddListener(DiceThrow);
    }

    void DiceThrow()
    {
        // Check if the dice has already been rolled this turn
        if (hasThrow)
        {
            // if "hasThrownDice" is true, the "DiceThrow" method will not throw the dice again and will simply end its execution
            return;
        }

        int throwValue = Random.Range(1, 7);
        UpdateDice(throwValue);
        //hasThrow = true;
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
            case 5:
                diceNumber.text = "••\n•\n••";
                break;
            case 6:
                diceNumber.text = "•••\n•••";
                break;
            default:
                diceNumber.text = "";
                break;

        }
    }


}
