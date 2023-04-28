using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveTurn { Player, Enemy }

public class GameManager : MonoBehaviour
{
    public ActiveTurn activeTurn;
    private DicePlayer dicePlayer;
    private DiceEnemy diceEnemy;
        
    void Start()
    {
        dicePlayer = GameObject.Find("Player Dice").GetComponent<DicePlayer>();
        diceEnemy = GameObject.Find("Enemy Dice").GetComponent<DiceEnemy>();
    }

    
    void Update()
    {
       
    }

    public void SetTurn()
    {
        if (activeTurn == ActiveTurn.Player)
        {
            activeTurn = ActiveTurn.Enemy;
            dicePlayer.gameObject.SetActive(false);
            diceEnemy.gameObject.SetActive(true);
        }
        else if(activeTurn == ActiveTurn.Enemy)
        {            
            activeTurn = ActiveTurn.Player;
            diceEnemy.gameObject.SetActive(false);
            dicePlayer.gameObject.SetActive(true);
        }
    }

 
}
