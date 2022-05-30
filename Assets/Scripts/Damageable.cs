using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour{
	
	[Tooltip("Multiplier to apply to the received damage")]
	public float damageMultiplier = 1f;
	
	public Health health { get; private set; }
	
	void Awake() {
		// find the health component either at the same level, or higher in the hierarchy
		health = GetComponent<Health>();
		if (!health) {
			health = GetComponentInParent<Health>();
		}
	}
	
	public void InflictDamage(float damage) {
		if (health) {
			// apply the damages
			health.TakeDamage(damage);
		}
	}
}
