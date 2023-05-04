using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceEnemy : MonoBehaviour
{
    private Button diceButton;
    private GameManager gameManager;
    private Enemy enemyMove;
    public TextMeshProUGUI diceNumber;


    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        diceButton = GetComponent<Button>();
        diceButton.onClick.AddListener(DiceThrow);
        enemyMove = GameObject.Find("Enemy").GetComponent<Enemy>();
    }


    public void DiceThrow()
    {   
        int throwValue = Random.Range(0, 5);
        UpdateDice(throwValue);
        enemyMove.moveDiceValue = throwValue;

        if (throwValue == 0)
        {
            gameManager.Invoke("SetTurn", 1f);
        }     
        // gameManager.SetTurn(); // Estaba en el viejo codigo
    }

    // Method to update the UI display of the player dice

    public void UpdateDice(int numberToShow)
    {
        switch (numberToShow)
        {
            case 1:
                diceNumber.text = "�";
                break;
            case 2:
                diceNumber.text = "��";
                break;
            case 3:
                diceNumber.text = "���";
                break;
            case 4:
                diceNumber.text = "��\n��";
                break;
            default:
                diceNumber.text = "";
                break;

        }
    }

}
