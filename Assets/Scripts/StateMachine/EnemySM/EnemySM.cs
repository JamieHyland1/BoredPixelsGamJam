using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : StateMachine
{
    public EnemyIdleState enemyIdleState {get; private set;}
    public EnemyFollowState enemyFollowState {get; private set;}


    [Header ("Enemy idle route")]
    [Tooltip ("Creates a path for an enemy to follow when a player isnt around by using a bezier curve")]
    [SerializeField]
    Transform[] controlPoints;

    [SerializeField]
    [Tooltip ("determines the distance the player has to be in range of the enemy for them to follow them")]
    float enemyViewRadius;

    private GameObject player;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyFollowState = new EnemyFollowState(this,enemyViewRadius,player);
        enemyIdleState = new EnemyIdleState(this, controlPoints, enemyViewRadius,player);

        this.ChangeState(enemyIdleState);
    }

    private void OnEnable(){

    }

    private void OnDisable() {
        
    }




}
