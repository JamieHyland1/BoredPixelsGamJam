using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController {
    static GameObject cursor;
    float radius;
    float x,y;
    Transform playerTransform;
   public ShootController(){

   }
   public ShootController(GameObject cursor, float radius, Transform playerTransform){
       ShootController.cursor = cursor;
       this.radius = radius;
       this.playerTransform = playerTransform;
   }

   public void Aim(){
        float xDir = (Input.GetAxisRaw("Horizontal"));
        float yDir = Input.GetAxisRaw("Vertical");
        Vector3 position = new Vector3();

        position = new Vector3(playerTransform.position.x + (xDir*radius), playerTransform.position.y + (yDir*radius), playerTransform.position.z);
        cursor.transform.position = position;
        x = 0;
        y = 0;
        xDir = 0;
        yDir = 0;
   }

   public void placeBomb(){
       var bomb =  Resources.Load<GameObject>("Prefabs/Bomb");
       Debug.Log(bomb);
       MonoBehaviour.Instantiate(bomb,cursor.transform.position,cursor.transform.rotation);
   }
}
