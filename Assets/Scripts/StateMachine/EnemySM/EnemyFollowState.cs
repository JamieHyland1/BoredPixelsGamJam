using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : IState{
    EnemySM _EnemySM;
    float enemyViewRadius;
    GameObject player;

    float speed;

    public EnemyFollowState(){

    }

    public EnemyFollowState(EnemySM enemySM, float enemyViewRadius, GameObject player, float speed){
        this._EnemySM = enemySM;
        this.enemyViewRadius = enemyViewRadius;
        this.player = player;
        this.speed = speed;
    }


    public void Enter(){
        
    }

    public void Exit(){
        
    }

    public void FixedTick(){
       
    }

    public void Tick(){
        _EnemySM.checkForBombs();
        _EnemySM.transform.position = Vector3.MoveTowards(_EnemySM.transform.position,player.transform.position, speed*Time.deltaTime);
        if(Vector3.Distance(_EnemySM.transform.position,player.transform.position) > enemyViewRadius){
            _EnemySM.ChangeState(_EnemySM.enemyIdleState);
        }
        if(Vector3.Distance(_EnemySM.transform.position,player.transform.position) < 1){
            player.GetComponent<PlayerSM>().hit();
        }
    }

    
}
