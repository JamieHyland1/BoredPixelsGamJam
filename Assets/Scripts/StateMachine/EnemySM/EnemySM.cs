using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : StateMachine
{
    public EnemyIdleState enemyIdleState {get; private set;}
    public EnemyFollowState enemyFollowState {get; private set;}
    public BombActivationState bombActivationState {get; private set;}

    [Header ("Enemy idle route")]
    [Tooltip ("Creates a path for an enemy to follow when a player isnt around by using a bezier curve")]
    [SerializeField]
    Transform[] controlPoints;

    [SerializeField]
    [Tooltip ("determines the distance the player has to be in range of the enemy for them to follow them")]
    float enemyViewRadius;
	
	[SerializeField]
    [Tooltip ("determines the distance a bomb has to be in range of the enemy for it to detect the bomb")]
    float enemyBombDetectionRadius = 14.0f;

    [SerializeField]
    private bool followsPath = false;

    [SerializeField]
    float moveSpeed;
	
	[SerializeField]
	public Animator animator;

    private GameObject player;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyFollowState = new EnemyFollowState(this,enemyViewRadius, enemyBombDetectionRadius, player, moveSpeed);
        bombActivationState = new BombActivationState(this,enemyViewRadius, enemyBombDetectionRadius, moveSpeed);
        enemyIdleState = new EnemyIdleState(this, controlPoints, enemyViewRadius, enemyBombDetectionRadius, player, moveSpeed);

        this.ChangeState(enemyIdleState);
    }

    private void OnEnable(){

    }

    private void OnDisable() {
        
    }

    public void checkForBombs(){
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        for(int i = 0; i < bombs.Length; i++){
            Vector3 bombPos = bombs[i].transform.position;
            var bomb = bombs[i].GetComponent<Bomb>();
            if(!bomb.active){
                float distanceFromEnemy = Vector3.Distance(this.transform.position,bombPos);
                if(distanceFromEnemy <= enemyBombDetectionRadius)
				{
					animator.SetBool("HasTarget", true);
					this.ChangeState(bombActivationState);
                }
            }
        }
    }

    

}
