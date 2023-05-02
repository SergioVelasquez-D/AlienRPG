using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceEnemy : MonoBehaviour
{
    private Button diceButton;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        diceButton = GetComponent<Button>();
        diceButton.onClick.AddListener(DiceThrow);
    }


    void DiceThrow()
    {        
        gameManager.SetTurn();
    }

}
