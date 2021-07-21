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
    
    public SlidingState(){

    }

    public SlidingState(PlayerSM _playerSM, float freezeTime, MoveController moveController, Rigidbody2D rigidbody2D){
        this._playerSM = _playerSM;
        this.freezeTime = freezeTime;
        this.moveController = moveController;
        this.rigidbody2D = rigidbody2D;
    }
    public void Enter(){
        counter = freezeTime;
        rigidbody2D.freezeRotation = false;
    
    }  

    public void Exit(){
        rigidbody2D.freezeRotation = true;
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
