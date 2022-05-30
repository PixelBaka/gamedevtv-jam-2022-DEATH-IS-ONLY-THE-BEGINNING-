
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Doors : MonoBehaviour {
	
	public GameObject enemys;
	public GameObject text;
	
	List<Transform> enemysList = new List<Transform>();
	Text CountText;
	
	bool exit = false;
	
	private void Start() {
		CountText = text.GetComponent<Text>();
	}
	
	private void Update() {
		if (!exit){
			foreach (Transform child in enemys.transform) {
				enemysList.Add(child);
			}
			
			if(enemysList.Count == 0){
				exit = true;
				CountText.text = "You can move through this door";
			}
			else{
				CountText.text = enemysList.Count + " enemies remaining";
			}
			
			enemysList.Clear();
		}
	}
	private void OnTriggerEnter(Collider other) {
		if(!exit)
			return;
		
		Destroy(gameObject);
	}
	
	

}
