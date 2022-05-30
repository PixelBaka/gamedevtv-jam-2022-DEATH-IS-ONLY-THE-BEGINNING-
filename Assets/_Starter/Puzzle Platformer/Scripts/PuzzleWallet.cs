using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWallet : MonoBehaviour {
	
	int coins = 0;

	public int GetCoins() { return coins; }
	public void IncrementCoins() { coins++; }

}
