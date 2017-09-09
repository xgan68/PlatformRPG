using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

	Collider hitBoxCollider;

	Transform hitEffect;
	Vector3 hitEffectOffset = new Vector3(0, 1.0f, -0.5f);
	// Use this for initialization
	void Start () {
		hitBoxCollider = GetComponent<BoxCollider> ();
		hitBoxCollider.enabled = false;

		hitEffect = transform.GetChild (0);
	}

	void Update () {
		IsAttacking ();
	}

	void OnTriggerEnter(Collider other){



		if (other.tag == "Enemy") {
			StartCoroutine(BulletTime(0.02f));
			other.gameObject.GetComponent<EnemyController> ().Hit (transform.parent);
			hitEffect.position = other.transform.position + hitEffectOffset;
			hitEffect.GetComponent<ParticleSystem> ().Emit (1);

		}
		if (other.tag == "Chest") {
			//StartCoroutine(BulletTime(0.02f));
			other.gameObject.GetComponent<OpenChest> ().Hit ();
			hitEffect.GetComponent<ParticleSystem> ().Emit (1);
		}

	}

	public void IsAttacking(){
		hitBoxCollider.enabled = CharacterController.isAttacking;
	}


	IEnumerator BulletTime(float remain){
		Time.timeScale = 0.1f;
		while (remain > 0) {
			yield return new WaitForSeconds (0.02f);
			remain -= 0.02f;

		}
		Time.timeScale = 1f;
	}
}
