using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
	public float speed;
	public float moveDirection = 0;
	public bool parentOnTrigger = true;
	public bool hitBoxOnTrigger = false;
	public GameObject moverObject = null;
	private Renderer mRenderer = null;
	private bool isVisible = false;
	// Use this for initialization
	void Start () {
		mRenderer = moverObject.GetComponent<Renderer>();
		speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(speed*Time.deltaTime,0,0);
		IsVisible();
	}
	void IsVisible(){
		if(mRenderer.isVisible){
			isVisible = true;
		}
		
		if(!mRenderer.isVisible && isVisible){
			Debug.Log("Remove object no longer seen");
			Destroy(this.gameObject);
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Debug.Log("Enter");
		}
		if(parentOnTrigger){
			Debug.Log("Enter: parent to me");
			other.transform.parent = this.transform;
		}
		if(hitBoxOnTrigger){
            Debug.Log("Enter: hitbox gameover");
			other.GetComponent<PlayerController>().GotHit();
		}
	}
	void OnTriggerExit(Collider other){
        if (other.tag == "Player")
        {
            Debug.Log("Mover hit player and gone");
			if(parentOnTrigger){
				other.transform.parent = null;
			}
        }
	}
}
