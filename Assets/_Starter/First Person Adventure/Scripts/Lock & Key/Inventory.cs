using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	
	List<KeyType> keys = new List<KeyType>();
	
	public void AddKey(KeyType keyType) { keys.Add(keyType); }
	public void RemoveKey(KeyType keyType) { keys.Remove(keyType); }
	public bool IsHoldingKey(KeyType keyType) { return keys.Contains(keyType); }
}
