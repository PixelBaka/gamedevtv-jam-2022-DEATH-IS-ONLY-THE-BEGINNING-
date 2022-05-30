using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetting : MonoBehaviour {
	
	private void Start() {
		//Set Cursor to not be visible
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
	}
}
