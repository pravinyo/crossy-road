using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	public int coinValue = 1;
	// Use this for initialization

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Debug.Log("Player picked up the coin");
			//update the coin count
			Destroy(this.gameObject);
		}
	}
}
