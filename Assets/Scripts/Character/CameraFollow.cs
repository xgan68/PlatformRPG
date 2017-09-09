using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	public Vector3 cameraPositionOffset;

	// Use this for initialization
	void Start () {
		cameraPositionOffset = new Vector3(0,5,-10);

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = target.TransformPoint (cameraPositionOffset);
		transform.position = Vector3.SmoothDamp (transform.position, target.position, ref velocity, smoothTime);
	}
}
