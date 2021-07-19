using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : IState
{
    EnemySM _EnemySM;
    Transform playerTransform;

    Animator animator;

    float timer;
    float distanceToShootFrom;

    float counter;

    bool readyToShoot = false;
    

    public EnemyWalkState(EnemySM enemySM, Transform plyrTrans, Animator animator, float timer, float distanceToShootFrom){
        this._EnemySM = enemySM;
        this.playerTransform = plyrTrans;
        this.animator = animator;
        this.timer=timer;
        this.distanceToShootFrom = distanceToShootFrom;
       
    }

    public void Enter(){
        Debug.Log("ENTERED WALKING STATE");
        animator.SetTrigger("Walking");
        counter = timer;
    }

    public void Exit(){
        Debug.Log("LEAVING WALK STATE");
        readyToShoot = false;
    }

    public void FixedTick(){

    }

    public void Tick(){
        if(counter > 0) counter -= Time.deltaTime;

        if(counter <= 0)readyToShoot = true;

        if(readyToShoot && Vector3.Distance(_EnemySM.transform.position,playerTransform.position) >= distanceToShootFrom){
           // _EnemySM.ChangeState(_EnemySM.ShootState);
        }
        //move to player
        _EnemySM.transform.position = Vector3.MoveTowards(_EnemySM.transform.position,playerTransform.position,5f*Time.deltaTime);
       // if(Vector3.Distance(_EnemySM.transform.position,playerTransform.position) > 15)_EnemySM.ChangeState(_EnemySM.IdleState);
    }
}
