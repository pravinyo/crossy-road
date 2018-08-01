﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {
	public PlayerController playerController = null;
	private Animator animator = null; 
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerController.isDead){
			animator.SetBool("dead",true);
		}
		if(playerController.jumpStart){
			animator.SetBool("jumpStart",true);
		}else if(playerController.isJumping){
			animator.SetBool("jump",true);
		}else{
			animator.SetBool("jump",false);
			animator.SetBool("jumpStart",false);
		}
	}
}
