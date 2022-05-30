using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {
	
	Health m_Health;
	
	void Start(){
		m_Health = GetComponent<Health>();
		m_Health.onDie += OnDie;
	}

	void OnDie(){
		Destroy(this.gameObject, 0.3f);
	}
}
