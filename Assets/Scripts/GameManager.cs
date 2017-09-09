using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = new Vector3 (4f, 3f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static Vector3 GetStartPosition(){
		return startPosition;
	}
}
