using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	enum MovementStates:int{
		Idle=0,
		Follow=1,
		Attack=2,
		Dead=3,
	};

	MovementStates state;
	Animator anim;

	public Transform target;
	NavMeshAgent agent;

	float hp;
	float attackDamage;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		state = MovementStates.Idle;

		hp = 2f;
		attackDamage = 1f;
	}

	void OnEnable(){
		AreaTrigger.OnStepIn += StartChasing;
	}

	// Update is called once per frame
	void Update () {
		if(state != MovementStates.Idle && state != MovementStates.Dead)
			NextMovement ();
	}

	public void Hit(Transform attacker){
		hp--;
		if (hp <= 0) {
			Die ();
		} else {
			anim.SetTrigger ("getHit");
			PushBack (attacker, 0.3f);
		}

	}

	void PushBack(Transform attacker, float distance){
		Vector3 dir = transform.position - attacker.transform.position;
		dir.y = 0;
		dir = Vector3.Normalize(dir);
		transform.position += distance * dir;
	}

	void NextMovement(){
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
			agent.SetDestination (transform.position);
		else if(state ==  MovementStates.Follow && anim.GetCurrentAnimatorStateInfo(0).IsName("Run")){
			agent.SetDestination (target.position);
		}

		if (Vector3.Distance (target.position, transform.position) >= 2f) {
			state = MovementStates.Follow;
		} else {
			transform.LookAt (target.position);
			state = MovementStates.Attack;
		}



		anim.SetInteger ("state", (int)state);

		//Debug.Log (agent.nextPosition);
	}

	void Die(){
		state = MovementStates.Dead;
		anim.SetTrigger ("Die");
		GetComponent<Collider> ().enabled = false;
		Destroy (gameObject, 2);
	}

	void StartChasing(){
		state = MovementStates.Follow;
		Debug.Log ("Start Chasing");
	}

	public void Step(){
		
	}

	public void OnAttack(){
		if (Vector3.Distance (target.position, transform.position) <= 2f) {
			target.GetComponent<CharacterController> ().GetHit (attackDamage);
		}
	}

}
