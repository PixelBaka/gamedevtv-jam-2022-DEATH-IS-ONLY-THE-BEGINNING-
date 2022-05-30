using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Win : MonoBehaviour {
	
	public GameObject Curser;
	public GameObject WinText;
	
	public GameObject player;
	
	bool win;
	PlayerInput playerInput;
	
	private void Start() {
		playerInput = player.GetComponent<PlayerInput>();
	}
	
	private void Update() {
		//if(!win) return;
		
		if( Keyboard.current[Key.Escape].wasPressedThisFrame ){
			Debug.Log("Exit Game");
			Application.Quit();
		}
		
	}
	
	private void OnTriggerEnter(Collider other) {
		playerInput.actions.FindActionMap("Player").Disable();
		
		Curser.SetActive(false);
		WinText.SetActive(true);
		
		win = true;
		
		Debug.Log("You Win");
	}
}
