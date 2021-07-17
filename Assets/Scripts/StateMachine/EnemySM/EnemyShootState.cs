using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootState : IState
{
    EnemySM _EnemySM;
    Transform PlayerTransform;

    Transform bulletTransform;

    GameObject bulletObject;

    Animator animator;

    float shootTimer = 0.55f;

    float counter = 0;

    public EnemyShootState(EnemySM _esm, Transform plyrTrans, Animator animator, Transform bulletTransform, GameObject bulletObj){
        this._EnemySM = _esm;
        this.PlayerTransform = plyrTrans;
        this.animator = animator;
        this.bulletTransform = bulletTransform;
        this.bulletObject = bulletObj;
    }
    public void Enter(){
        Debug.Log("ENTERING SHOOT STATE");
        animator.SetTrigger("Shooting");
        counter = shootTimer;
    }
    public void Exit(){
        Debug.Log("LEAVING SHOOT STATE");

    }
    public void FixedTick(){

    }
    public void Tick(){
        counter -= Time.deltaTime;
        if(counter <= 0){
            Debug.Log("BANG!");
            MonoBehaviour.Instantiate(bulletObject,bulletTransform.position,Quaternion.identity);
            _EnemySM.ChangeState(_EnemySM.IdleState);
        }
    }
}
