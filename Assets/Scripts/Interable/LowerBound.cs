using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			// kill player --> health = 0;
			other.transform.position = GameManager.startPosition;
			other.transform.forward = new Vector3 (0, 0, 1);
		}


		Debug.Log (other.tag);
	}
}
