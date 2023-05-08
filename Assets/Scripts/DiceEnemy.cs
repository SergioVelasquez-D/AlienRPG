using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceEnemy : MonoBehaviour
{
    //private Button diceButton;
    private GameManager gameManager;
    private Enemy enemy;
    public TextMeshProUGUI diceNumber;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    // Enemy movement dice roll
    public void DiceThrow()
    {     
        int throwValue = Random.Range(0, 5);
        UpdateDice(throwValue);
        enemy.moveDiceValue = throwValue;        
        enemy.MoveEnemy(throwValue);

        if (throwValue == 0)
        {
            gameManager.Invoke("SetTurn", 1f);
        }
    }

    // Update the UI display of the enemy movement dice
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
