using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : IState{
    EnemySM _EnemySM;
    float enemyViewRadius;
	float enemyBombDetectionRadius;
    GameObject player;

    float speed;
	float bounceSpeed;
	
	float animDelay = 0.75f;
	SpriteRenderer renderer;

    public EnemyFollowState(){

    }

    public EnemyFollowState(EnemySM enemySM, float enemyViewRadius, float enemyBombDetectionRadius, GameObject player, float speed){
        this._EnemySM = enemySM;
        this.enemyViewRadius = enemyViewRadius;
		this.enemyBombDetectionRadius = enemyBombDetectionRadius;
        this.player = player;
        this.speed = speed;
		
		renderer = _EnemySM.gameObject.GetComponentInChildren<SpriteRenderer>();
		
		Debug.Log(renderer.sprite.name);
    }


    public void Enter(){
		
		_EnemySM.animator.SetTrigger("TargetAcquired");
		_EnemySM.animator.ResetTrigger("ActionAnimFinished");
		animDelay = 0.75f;
    }

    public void Exit(){
        
    }

    public void FixedTick(){
       
    }

    public void Tick(){
		if(animDelay > 0.0f)
			animDelay -= Time.deltaTime;
		
        _EnemySM.checkForBombs();
		
		if(animDelay <= 0.0f) // Only move if not playing alert anim
		{
			_EnemySM.animator.SetTrigger("ActionAnimFinished");
			Vector3 prevPosition = _EnemySM.transform.position;
			_EnemySM.transform.position = Vector3.MoveTowards(_EnemySM.transform.position,player.transform.position, speed*Time.deltaTime);
			if(prevPosition.x > _EnemySM.transform.position.x) // We're moving left
			{
				renderer.flipX = true;
			}
			else
			{
				renderer.flipX = false;
			}
				
		}
		
        if(Vector3.Distance(_EnemySM.transform.position,player.transform.position) > enemyViewRadius)
		{
			_EnemySM.animator.SetBool("HasTarget", false);
			_EnemySM.animator.SetTrigger("TargetLost");
            _EnemySM.ChangeState(_EnemySM.enemyIdleState);
        }
        if(Vector3.Distance(_EnemySM.transform.position,player.transform.position) < 1)
		{
            player.GetComponent<PlayerSM>().hit();
			_EnemySM.animator.SetTrigger("TargetAcquired");
			_EnemySM.animator.ResetTrigger("ActionAnimFinished");
			animDelay = 0.75f;
        }
    }

    
}
