using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    EnemySM _EnemySM;
    Transform[] controlPoints;
    float enemyViewRadius;
	float enemyBombDetectionRadius;
	
	float animDelay = 1.25f; // Roughly timed anim for lost target anim

    GameObject player;
    List<Vector3> path;
    float speed;
    int currentPathIndex = 0;
    int nextPathIndex = 1;
    public EnemyIdleState(EnemySM enemySM, Transform[] controlPoints, float enemyViewRadius, float enemyBombDetectionRadius, GameObject player, float speed){
        this._EnemySM = enemySM;
        this.controlPoints = controlPoints;
        this.enemyViewRadius = enemyViewRadius;
		this.enemyBombDetectionRadius = enemyBombDetectionRadius;
        this.player = player;
        this.speed = speed;
    }
    public void Enter(){
        path = GeneratePath();
		
        _EnemySM.animator.SetTrigger("TargetLost");
		_EnemySM.animator.ResetTrigger("ActionAnimFinished");
		animDelay = 1.25f;
    }

    public void Exit(){

    }

    public void FixedTick(){

    }

    public void Tick() {
		if(animDelay > 0.0f)
			animDelay -= Time.deltaTime;
		
        _EnemySM.checkForBombs();
		
		if(animDelay <= 0.0f) // Only move if not playing the lost target anim
		{
			_EnemySM.animator.SetTrigger("ActionAnimFinished");
			_EnemySM.transform.position =  Vector3.MoveTowards(_EnemySM.transform.position,path[nextPathIndex],speed*Time.deltaTime);
		}
        
		if(Vector3.Distance(_EnemySM.transform.position,path[nextPathIndex]) < 0.1f){
            _EnemySM.transform.position = path[nextPathIndex];
            nextPathIndex++;
        }
        if(nextPathIndex >= path.Count){
            path.Reverse();
            nextPathIndex = 0;
        }

        if(Vector3.Distance(_EnemySM.transform.position, player.transform.position) <= enemyViewRadius)
		{
			_EnemySM.animator.SetBool("HasTarget", true);
            _EnemySM.ChangeState(_EnemySM.enemyFollowState);
        }
		else
		{
			_EnemySM.animator.SetBool("HasTarget", false);
		}
        
    }

    public List<Vector3> GeneratePath(){
        List<Vector3> path = new List<Vector3>();
           for (float i = 0; i < 1; i += 0.05f){
            Vector3 pos;
            pos = Mathf.Pow(1-i,3) * controlPoints[0].position +
                  3 * Mathf.Pow(1-i,2) * i * controlPoints[1].position +
                  3 * (1-i) * Mathf.Pow(i,2) * controlPoints[2].position +
                  Mathf.Pow(i,3) * controlPoints[3].position;
                path.Add(pos);
        }
        return path;
    }

    
}
