using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPlayer : MonoBehaviour
{
    private Button attackBtn;
    private GameManager gameManager;
    //public Button attackDicePlayer;
    public GameObject attackPanel;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        attackBtn = GetComponent<Button>();
        attackBtn.onClick.AddListener(StartAttack);
    }

    void StartAttack()
    {
        //attackDicePlayer.gameObject.SetActive(true);
        attackPanel.gameObject.SetActive(true);
        gameManager.activeTurnText.text = "Player Attack";
        Debug.Log("Attack Button pressed");
    }
}
