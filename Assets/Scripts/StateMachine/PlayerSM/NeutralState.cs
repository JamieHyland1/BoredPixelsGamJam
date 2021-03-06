using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralState : IState
{
    PlayerSM _playerSM;
    MoveController moveController;

    ShootController shootController;

    public NeutralState(){

    }
    public NeutralState(PlayerSM _playersm, MoveController moveController, ShootController shootController){
        this._playerSM = _playersm;
        this.moveController = moveController;
        this.shootController = shootController;
    }
    public void Enter(){
    }

    public void Exit(){

    }

    public void FixedTick(){
      
         moveController.applyForces();
    }

    public void Tick(){
        
      
        
        moveController.applyGravity();
        moveController.moveCharacter();

        if(Input.GetButtonDown("Fire1")){
            shootController.placeBomb();
        }
        if(Input.GetButtonDown("Fire3")){
            _playerSM.ChangeState(_playerSM.slidingState);
        }
        if(Input.GetButtonDown("Fire2")){
            Debug.Log("dash");
            moveController.Dash();
            shootController.rocketImpulse();
        }
    

       
        
//        shootController.Aim();
    }
}
