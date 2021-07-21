using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombActivationState : IState
{   
    EnemySM _enemySM;
    float viewRadius;

    float waitTime = 1;
    float counter;
    Queue<GameObject> bombsToCheck;

    GameObject currentBomb = null;

    float speed;
    public BombActivationState(){

    }

    public BombActivationState(EnemySM _enemySM, float viewRadius, float speed){
        this._enemySM = _enemySM;
        this.viewRadius = viewRadius;
        bombsToCheck = new Queue<GameObject>();
        this.speed = speed;
    }
    public void Enter(){
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        for(int i = 0; i < bombs.Length; i++){
            Vector3 bombPos = bombs[i].transform.position;
            var bomb = bombs[i].GetComponent<Bomb>();
            float distanceFromEnemy = Vector3.Distance(_enemySM.transform.position,bombPos);
            if(distanceFromEnemy <= viewRadius){
                if(!bomb.active && !bombsToCheck.Contains(bombs[i]))bombsToCheck.Enqueue(bombs[i]);
            }
        }
        counter = waitTime;
        Debug.Log("Entering bomb state");
    }

    public void Exit(){
        Debug.Log("Entering bomb state");
    }

    public void FixedTick(){
 
    }

    public void Tick(){
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

        if(bombsToCheck.Count == 0 && currentBomb == null)_enemySM.ChangeState(_enemySM.enemyIdleState);
    }
}
