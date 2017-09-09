using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
	public enum Moves{
		Idle,
		Run,
		Attack,
		jump
	};
		
	//Player Stats
	public static float hp;
	float maxHP = 3f;
	//***************

	public static Moves moveState;
	Moves prevState;

	Animator anim;

	Vector3 runDir;
	float speed = 2.5f;

	public static bool isAttacking;

	private static Vector3 playerPosition;

	// Use this for initialization
	void Start () {
		hp = 3f;
		moveState = Moves.Idle;
		runDir = Vector3.forward;
		anim = GetComponent<Animator> ();
		prevState = moveState;
		isAttacking = false;

		transform.position = GameManager.startPosition;
	}
	
	// Update is called once per frame
	void Update () {
		CheckBtnDown ();
		Movement ();
		AnimationStates ();
		playerPosition = transform.position;
	}


	void CheckBtnDown(){

		runDir.x = Input.GetAxis ("Horizontal");
		runDir.z = Input.GetAxis ("Vertical");

		runDir = Vector3.Normalize (runDir);

		if (Input.GetKeyUp (KeyCode.J)) {
			moveState = Moves.Attack;
			anim.SetTrigger ("Attack");

		} else if (Input.GetKeyDown (KeyCode.Space)) {
			moveState = Moves.jump;
		} else if (runDir != Vector3.zero) {
			moveState = Moves.Run;
		} else {
			if(runDir == Vector3.zero && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
				moveState = Moves.Idle;
		} 


	}

	void Movement(){
		if (runDir != Vector3.zero && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") ) {
			transform.forward = runDir;
			transform.position += runDir * speed * Time.deltaTime;
		}
	}
		
	void AnimationStates(){
		
		anim.SetFloat ("Speed", Mathf.Max(Mathf.Abs(runDir.x), Mathf.Abs(runDir.z)));

	}

	void Hit(int state){
		if (state == 1)
			isAttacking = true;
		else {
			isAttacking = false;
		}
	}

	void Step(){
		//transform.GetChild (0).GetComponent<ParticleSystem> ().Emit (1);
	}

	public static Vector3 GetPlayerPosition(){
		return playerPosition;
	}



	public void GetHit(float damage){
		hp -= damage;

		if (hp <= 0)
			OnDeath ();
	}

	private void OnDeath(){
		transform.position = GameManager.GetStartPosition();
		hp = maxHP;
	}


}
