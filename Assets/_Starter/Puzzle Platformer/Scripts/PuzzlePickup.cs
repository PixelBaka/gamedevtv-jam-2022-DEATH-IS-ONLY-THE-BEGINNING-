using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        //Add the coin to the player wallet when collectd
        PuzzleWallet wallet = other.GetComponent<PuzzleWallet>();
        if(wallet != null)
        {
            wallet.IncrementCoins();
            Destroy(gameObject);
        }
    }
}
