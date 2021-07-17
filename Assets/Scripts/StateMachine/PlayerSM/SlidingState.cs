using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : IState
{
    PlayerSM _playerSM;
    float freezeTime;

    float counter;

    MoveController moveController;
    
    public SlidingState(){

    }

    public SlidingState(PlayerSM _playerSM, float freezeTime, MoveController moveController){
        this._playerSM = _playerSM;
        this.freezeTime = freezeTime;
        this.moveController = moveController;
    }
    public void Enter(){
        counter = freezeTime;
       
    
    }  

    public void Exit(){

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
