using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackPlayer : MonoBehaviour {
	
	GameObject Player;
	NavMeshAgent agent;
	
	private void Start() {
		agent = GetComponent<NavMeshAgent>();
	}
	
	private void FixedUpdate() {
		if(!Player)
			return;
		
		agent.SetDestination(Player.transform.position);
	}
	
	private void OnTriggerEnter(Collider other) {
		if(other.tag != "Player")
			return;
		
		Player = other.gameObject;
	}

}
