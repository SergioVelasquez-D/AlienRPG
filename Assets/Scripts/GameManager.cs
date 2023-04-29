using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ActiveTurn { Player, Enemy }

public class GameManager : MonoBehaviour
{
    public ActiveTurn activeTurn;
    public TextMeshProUGUI activeTurnText;
    private DicePlayer dicePlayer;
    private DiceEnemy diceEnemy;
        
    void Start()
    {
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        diceEnemy = GameObject.Find("Enemy Dice").GetComponent<DiceEnemy>();
        diceEnemy.gameObject.SetActive(false);
        activeTurnText.text = "Player Turn";
    }

    
    void Update()
    {
       
    }
    // Method to switch turn
    public void SetTurn()
    {
        if (activeTurn == ActiveTurn.Player)
        {
            activeTurn = ActiveTurn.Enemy;
            dicePlayer.gameObject.SetActive(false);
            diceEnemy.gameObject.SetActive(true);
            activeTurnText.text = "Enemy Turn";
        }
        else if(activeTurn == ActiveTurn.Enemy)
        {            
            activeTurn = ActiveTurn.Player;
            diceEnemy.gameObject.SetActive(false);
            dicePlayer.gameObject.SetActive(true);
            dicePlayer.hasThrow = false;
            activeTurnText.text = "Player Turn";
        }
    }

 
}
