using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPlayer : MonoBehaviour
{
    private Button attackBtn;
    public Button attackDicePlayer;
    
    void Start()
    {
        attackBtn = GetComponent<Button>();
        attackBtn.onClick.AddListener(StartAttack);
    }

    
    void Update()
    {
        
    }

    void StartAttack()
    {
        attackDicePlayer.gameObject.SetActive(true);
        Debug.Log("Attack Button pressed");
    }
}
