using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon {

	float damage;

	public Weapon(float damage){
		this.damage = damage;
	}
}
