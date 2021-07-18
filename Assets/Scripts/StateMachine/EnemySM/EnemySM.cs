using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : StateMachine
{
    public EnemyIdleState IdleState {get; private set;}
    public EnemyShootState ShootState {get; private set;}
    public EnemyWalkState WalkState {get; private set;}

    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Animator animator;

    [SerializeField]
    public float distanceToShootFrom = 2f;

    [SerializeField]
    GameObject bulletObj;

    [SerializeField]
    Transform bulletTransform;

    private void Awake() {
        IdleState = new EnemyIdleState(this, playerTransform, animator);
        WalkState = new EnemyWalkState(this, playerTransform,animator, 1.3f, distanceToShootFrom);
        ShootState = new EnemyShootState(this, playerTransform, animator, bulletTransform, bulletObj);


        this.ChangeState(IdleState);
    }

    private void OnEnable(){

    }

    private void OnDisable() {
        
    }




}
