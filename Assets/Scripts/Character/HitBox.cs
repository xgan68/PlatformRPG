/*
这段代码是实现塞尔达传说游戏里那种打击感的核心，
简单的子弹时间效果，粒子特效，音效和击退效果结合
能让游戏的战斗手感大幅度提升。
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

	Collider hitBoxCollider;
	private static float BulletTimeLength = 0.02f;

	Transform hitEffect;
	Vector3 hitEffectOffset = new Vector3(0, 1.0f, -0.5f);

	void Start () {
		hitBoxCollider = GetComponent<BoxCollider> ();
		hitBoxCollider.enabled = false;
		hitEffect = transform.GetChild (0);
	}

	void Update () {
		IsAttacking ();
	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Enemy") {
			StartCoroutine(BulletTime(BulletTimeLength));
			other.gameObject.GetComponent<EnemyController> ().Hit (transform.parent);
			hitEffect.position = other.transform.position + hitEffectOffset;
			hitEffect.GetComponent<ParticleSystem> ().Emit (1);

		}
		if (other.tag == "Chest") {
			StartCoroutine(BulletTime(BulletTimeLength));
			other.gameObject.GetComponent<OpenChest> ().Hit ();
			hitEffect.GetComponent<ParticleSystem> ().Emit (1);
		}
	}

	public void IsAttacking() {
		hitBoxCollider.enabled = CharacterController.isAttacking;
	}


	IEnumerator BulletTime(float remain) {
		Time.timeScale = 0.1f;
		while (remain > 0) {
			yield return new WaitForSeconds (0.01f);
			remain -= 0.01f;
		}
		Time.timeScale = 1f;
	}
}
