using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombActivationState : IState
{   
    EnemySM _enemySM;
    float viewRadius;
	float bombDetectionRadius;

    float waitTime = 1;
    float counter;
    Queue<GameObject> bombsToCheck;
	
	float animDelay = 0.75f;

    GameObject currentBomb = null;

    float speed;
    public BombActivationState(){

    }

    public BombActivationState(EnemySM _enemySM, float viewRadius, float bombDetectionRadius, float speed){
        this._enemySM = _enemySM;
        this.viewRadius = viewRadius;
		this.bombDetectionRadius = bombDetectionRadius;
        bombsToCheck = new Queue<GameObject>();
        this.speed = speed;
    }
    public void Enter(){
		
		_enemySM.animator.SetTrigger("TargetAcquired");
		_enemySM.animator.ResetTrigger("ActionAnimFinished");
		animDelay = 0.75f;
		
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        for(int i = 0; i < bombs.Length; i++){
            Vector3 bombPos = bombs[i].transform.position;
            var bomb = bombs[i].GetComponent<Bomb>();
            float distanceFromEnemy = Vector3.Distance(_enemySM.transform.position,bombPos);
            if(distanceFromEnemy <= bombDetectionRadius){
                if(!bomb.active && !bombsToCheck.Contains(bombs[i]))bombsToCheck.Enqueue(bombs[i]);
            }
        }
        counter = waitTime;
        Debug.Log("Entering bomb state");
    }

    public void Exit(){
        Debug.Log("Exiting bomb state");
    }

    public void FixedTick(){
 
    }

    public void Tick(){
		if(animDelay > 0.0f)
			animDelay -= Time.deltaTime;
		
		if(animDelay <= 0.0f)
		{
			_enemySM.animator.SetTrigger("ActionAnimFinished");
			if(bombsToCheck.Count > 0 && currentBomb == null)currentBomb = bombsToCheck.Dequeue();
			if(currentBomb != null){
				_enemySM.transform.position = Vector3.MoveTowards(_enemySM.transform.position,currentBomb.transform.position, speed*Time.deltaTime);

				if(Vector3.Distance(_enemySM.transform.position,currentBomb.transform.position) <= 1){
					counter -= Time.deltaTime;
					if(counter <= 0){
						currentBomb.GetComponent<Bomb>().active = true;
						currentBomb = null;
						counter = waitTime;
					}
				}
			}

			if(bombsToCheck.Count == 0 && currentBomb == null)
			{
				_enemySM.ChangeState(_enemySM.enemyIdleState);
			}
		}
    }
}
