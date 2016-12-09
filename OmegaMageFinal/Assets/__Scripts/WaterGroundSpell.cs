using UnityEngine;
using System.Collections;

public class WaterGroundSpell : PT_MonoBehaviour {
	public float duration = 4;
	public float durationVariance = 0.5f;
	
	public float fadeTime = 1f;
	public float timeStart;

	public float damagePerSecond = 0;

	void Start(){
		timeStart = Time.time;
		duration = Random.Range(duration - durationVariance,
		                        duration + durationVariance);
	}
	
	void Update(){
		
		float u = (Time.time - timeStart) / duration;
		
		float fadePercent = 1 - (fadeTime / duration);
		if (u > fadePercent){
			
			float u2 = (u - fadePercent) / (1 - fadePercent);
			
			Vector3 loc = pos;
			loc.z = u2 * 2;
			pos = loc;
		}
		
		if (u > 1){
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other){

		other.GetComponent<EnemyBug> ().speed = 0.1f;

		GameObject go = Utils.FindTaggedParent(other.gameObject);
		if (go == null){
			go = other.gameObject;
		}
		Utils.tr("Water hit", go.name);
	}
	
	void OnTriggerStay(Collider other){
		
		EnemyBug recipient = other.GetComponent<EnemyBug>();
		
		if (recipient != null){
			recipient.Damage(damagePerSecond, ElementType.earth, true);
			//recipient.speed = 0;

		}
	}
}
