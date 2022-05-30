using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGoal : MonoBehaviour
{
    [SerializeField] int target;
    [SerializeField] Color defaultColor = Color.red;
    [SerializeField] Color unlockColor = Color.blue;
    
    SpriteRenderer spriteRenderer;
    PuzzleWallet playerWallet;

    bool isUnlocked;

    void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;
        playerWallet = FindObjectOfType<PuzzleWallet>();
    }

    void Update()
    {
        //Check if player has enough coins to unlock the goal
        if(playerWallet.GetCoins() >= target)
        {
            isUnlocked = true;
            spriteRenderer.color = unlockColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Complete the level if the player has enough coins
        if (other.GetComponent<PuzzleWallet>() == playerWallet)
        {
            if(isUnlocked) {
                PuzzleLevelManager.Instance.ReloadLevel();
            }
        }
    }
}
