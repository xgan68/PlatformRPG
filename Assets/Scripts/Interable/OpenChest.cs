using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hit(){
		anim.SetTrigger ("Open");

		StartCoroutine (TriggerAfterSec (1f));
	}

	IEnumerator TriggerAfterSec(float remain){
		while (remain > 0) {
			remain -= 0.5f;
			yield return new WaitForSeconds(0.5f);
		}
		UIManager.OpenPauseMenu ();
	}
}
