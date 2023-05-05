using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceEnemy : MonoBehaviour
{
    //private Button diceButton;
    private GameManager gameManager;
<<<<<<< Updated upstream
=======
    private Enemy enemy;
    public TextMeshProUGUI diceNumber;
>>>>>>> Stashed changes

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
<<<<<<< Updated upstream
        diceButton = GetComponent<Button>();
        diceButton.onClick.AddListener(DiceThrow);
    }


    void DiceThrow()
    {        
        gameManager.SetTurn();
=======
        //diceButton = GetComponent<Button>();
        //diceButton.onClick.AddListener(DiceThrow);
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }


    public void DiceThrow()
    {     
        int throwValue = Random.Range(0, 5);
        UpdateDice(throwValue);
        enemy.moveDiceValue = throwValue;
        Debug.Log("Enemy Dice Throw: " + throwValue);
        enemy.MoveEnemy(throwValue);

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
>>>>>>> Stashed changes
    }

}
