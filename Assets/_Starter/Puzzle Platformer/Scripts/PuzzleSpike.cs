using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSpike : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        PuzzleLevelManager.Instance.ReloadLevel();    
    }
}
