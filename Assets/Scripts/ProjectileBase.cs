using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {
	
	[Header("General")]
	[Tooltip("Radius of this projectile's collision detection")]
	public float radius = 0.01f;
	[Tooltip("Transform representing the root of the projectile (used for accurate collision detection)")]
	public Transform root;
	[Tooltip("Transform representing the tip of the projectile (used for accurate collision detection)")]
	public Transform tip;
	[Tooltip("LifeTime of the projectile")]
	public float maxLifeTime = 5f;
	[Tooltip("VFX prefab to spawn upon impact")]
	public GameObject impactVFX;
	[Tooltip("LifeTime of the VFX before being destroyed")]
	public float impactVFXLifetime = 5f;
	[Tooltip("Offset along the hit normal where the VFX will be spawned")]
	public float impactVFXSpawnOffset = 0.1f;
	[Tooltip("Clip to play on impact")]
	public AudioClip impactSFXClip;
	[Tooltip("Layers this projectile can collide with")]
	public LayerMask hittableLayers = -1;
	
	[Header("Movement")]
	[Tooltip("Speed of the projectile")]
	public float speed = 20f;
	
	[Header("Damage")]
	[Tooltip("Damage of the projectile")]
	public float damage = 40f;
	
	ProjectileBase m_ProjectileBase;
	Vector3 m_LastRootPosition;
	Vector3 m_Velocity;
	bool m_HasTrajectoryOverride;
	float m_ShootTime;
	Vector3 m_TrajectoryCorrectionVector;
	Vector3 m_ConsumedTrajectoryCorrectionVector;
	List<Collider> m_IgnoredColliders;
	
	const QueryTriggerInteraction k_TriggerInteraction = QueryTriggerInteraction.Collide;
	
	private void OnEnable() {
		Destroy(gameObject, maxLifeTime);
	}
	
	public void OnShoot() {
		m_LastRootPosition = root.position;
		m_Velocity = transform.forward * speed;
		m_IgnoredColliders = new List<Collider>();
		
		// Ignore colliders of owner
		//Collider[] ownerColliders = owner.GetComponentsInChildren<Collider>();
		//m_IgnoredColliders.AddRange(ownerColliders);
	}
	
	void Update() {
		// Move
		transform.position += m_Velocity * Time.deltaTime;
		
		// Hit detection
		RaycastHit closestHit = new RaycastHit();
		closestHit.distance = Mathf.Infinity;
		bool foundHit = false;
		
		// Sphere cast
		Vector3 displacementSinceLastFrame = tip.position - m_LastRootPosition;
		RaycastHit[] hits = Physics.SphereCastAll(
								m_LastRootPosition, 
								radius, 
								displacementSinceLastFrame.normalized, 
								displacementSinceLastFrame.magnitude, 
								hittableLayers, 
								k_TriggerInteraction
							);
		foreach (var hit in hits) {
			if (IsHitValid(hit) && hit.distance < closestHit.distance) {
				foundHit = true;
				closestHit = hit;
			}
		}
		
		if (foundHit) {
			// Handle case of casting while already inside a collider
			if (closestHit.distance <= 0f) {
				closestHit.point = root.position;
				closestHit.normal = -transform.forward;
			}
			
			OnHit(closestHit.point, closestHit.normal, closestHit.collider);
		}
		
		m_LastRootPosition = root.position;
	}
	
	bool IsHitValid(RaycastHit hit) {
		// ignore hits with triggers that don't have a Damageable component
		if (hit.collider.isTrigger && hit.collider.GetComponent<Damageable>() == null) {
			return false;
		}
		
		// ignore hits with specific ignored colliders (self colliders, by default)
		if (m_IgnoredColliders.Contains(hit.collider)) {
			return false;
		}
		
		return true;
	}
	
	void OnHit(Vector3 point, Vector3 normal, Collider collider) {
		// damage
		// point damage
		Damageable damageable = collider.GetComponent<Damageable>();
		if (damageable) {
			damageable.InflictDamage(damage);
		}
		
		// impact vfx
		if (impactVFX) {
			GameObject impactVFXInstance = Instantiate(impactVFX, point + (normal * impactVFXSpawnOffset), Quaternion.LookRotation(normal));
			if (impactVFXLifetime > 0) {
				Destroy(impactVFXInstance.gameObject, impactVFXLifetime);
			}
		}
		
		// impact sfx
		if (impactSFXClip){
			//AudioUtility.CreateSFX(impactSFXClip, point, AudioUtility.AudioGroups.Impact, 1f, 3f);
		}
		
		// Self Destruct
		Destroy(this.gameObject);
	}
}
