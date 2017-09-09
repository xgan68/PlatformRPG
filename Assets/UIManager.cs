using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	static GameObject pauseCanvas;
	public GameObject pauseCan;

	public Texture Heard;

	// Use this for initialization
	void Start () {
		pauseCanvas = pauseCan;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public static void OpenPauseMenu(){
		if (pauseCanvas != null) {
			pauseCanvas.SetActive(true);
		}
		else
			throw new System.NullReferenceException ("No PauseCavas Found!");
	}

	public void OnRetry(){
		Debug.Log ("reload scene");
		Application.LoadLevel (Application.loadedLevel);
	}

	void OnGUI(){
		for (int i = 0; i < CharacterController.hp; i++) {
			GUI.DrawTexture (new Rect (10 + i * 50, 10, 60, 60), Heard);
		}
	}
}
