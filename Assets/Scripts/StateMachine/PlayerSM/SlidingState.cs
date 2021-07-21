using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : IState
{
    PlayerSM _playerSM;
    float freezeTime;

    float counter;

    MoveController moveController;

    Rigidbody2D rigidbody2D;

    Animator animator;
    
    public SlidingState(){

    }

    public SlidingState(PlayerSM _playerSM, float freezeTime, MoveController moveController, Rigidbody2D rigidbody2D, Animator animator){
        this._playerSM = _playerSM;
        this.freezeTime = freezeTime;
        this.moveController = moveController;
        this.rigidbody2D = rigidbody2D;
        this.animator = animator;
    }
    public void Enter(){
        counter = freezeTime;
     //   rigidbody2D.freezeRotation = false;
        animator.SetBool("Sliding",true);
    
    }  

    public void Exit(){
    //    rigidbody2D.freezeRotation = true;
        animator.SetBool("Sliding",false);
       // _playerSM.transform.rotation.eulerAngles = new Vector3(0,0,0);
    }

    public void FixedTick(){

    }

    public void Tick(){
        counter -= Time.deltaTime;
        moveController.applyGravity();
        moveController.slide();
        moveController.applyForces();
        if(counter <= 0)_playerSM.ChangeState(_playerSM.neutralState);
    }
}
