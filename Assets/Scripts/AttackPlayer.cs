using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPlayer : MonoBehaviour
{
    private Button attackBtn;
    private GameManager gameManager;
    
    public GameObject attackPanel;
    private AttackManager attackManager;
    
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        attackManager = GameObject.Find("AttackManager").GetComponent<AttackManager>();
        attackBtn = GetComponent<Button>();
        attackBtn.onClick.AddListener(StartAttack);
    }

    void StartAttack()
    {    
        if (!gameManager.gameOver)
        {
            attackPanel.gameObject.SetActive(true);
            attackManager.SetNewAttack();
            gameManager.activeTurnText.text = "Player Attack";            
        }        
    }
}
