using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    EnemySM _EnemySM;
    Transform playerTransform;
    Animator animator;
    public EnemyIdleState(EnemySM enemySM, Transform plyrTrans, Animator animator){
        this._EnemySM = enemySM;
        this.playerTransform = plyrTrans;
        this.animator = animator;
    }
    public void Enter(){
        Debug.Log("CHANGED TO IDLE STATE");
        animator.SetTrigger("Idle");
    }

    public void Exit()
    {
    }

    public void FixedTick()
    {
    }

    public void Tick(){
        if(Vector3.Distance(_EnemySM.transform.position,playerTransform.position) < 15f){
            Debug.Log("Found player");
            _EnemySM.ChangeState(_EnemySM.WalkState);
        }


    }
}
