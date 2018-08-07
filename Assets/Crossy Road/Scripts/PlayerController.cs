using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveDistance = 1;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1;
    public bool isIdle = true;
    public bool isDead = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool jumpStart = false;
    public ParticleSystem particle = null;
    public GameObject chick = null;
    private Renderer mRenderer = null;
    private bool isVisible = false;

    void Start(){
        mRenderer=chick.GetComponent<Renderer>();
    }
    void Update(){
        // TODO: Manager -> CanPlay()
        if(isDead) return;
        CanIdle();
        CanMove();
        IsVisible();
    }
    
    void CanIdle(){
        if(isIdle){
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
             Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)){
                 CheckIfCanMove();
            }
        }
    }

    void CheckIfCanMove(){
        // find obstacle in front of player
        RaycastHit hit;
        Physics.Raycast(this.transform.position,-chick.transform.up,out hit,colliderDistCheck);
        Debug.DrawRay(this.transform.position,-chick.transform.up * colliderDistCheck,Color.red,2f);

        if(hit.collider == null){
            SetMove();
        }else{
            if(hit.collider.tag == "collider"){
                Debug.Log("got hit");
                GotHit();
            }else{
                SetMove();
            }
        }
        
    }

    void SetMove(){
        Debug.Log("all clear");
        isIdle = false;
        jumpStart = true;
        isMoving = true;
    }

    void CanMove(){
        if(isMoving){
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));
                SetMoveForwardState();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance));
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z));
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Moving(new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z));
            }
        }
    }
    
    void Moving(Vector3 pos){
        isIdle = false;
        isMoving = false;
        isJumping = true;
        jumpStart = false;
        LeanTween.move(this.gameObject,pos,moveTime).setOnComplete(MoveComplete);
    }

    void MoveComplete(){
        Debug.Log("Move complete");
        isIdle = true;
        isJumping = false;
    }

    void SetMoveForwardState(){

    }

    void IsVisible(){
        if(mRenderer.isVisible){
            isVisible = true;
        }
        if(!mRenderer.isVisible && isVisible){
            Debug.Log("Got hit since player moved off the screen");
            GotHit();
        }
    }

    public void GotHit(){
        isDead = true;
        ParticleSystem.EmissionModule pm = particle.emission;
        pm.enabled = true;

        // TODO: Manager - >GameOver()
    }

}
