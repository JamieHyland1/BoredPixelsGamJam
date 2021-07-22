using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController {
    static GameObject cursor;
    float radius;
    float x,y;
    Transform playerTransform;
	
	Quaternion aimRotation;

   public ShootController(){

   }
   public ShootController(GameObject cursor, float radius, Transform playerTransform){
       ShootController.cursor = cursor;
       this.radius = radius/2;
       this.playerTransform = playerTransform;
   }

   public void Aim(){
        float xDir = (Input.GetAxisRaw("Horizontal"));
        float yDir = Input.GetAxisRaw("Vertical");
        Vector3 position = new Vector3();
		aimRotation = new Quaternion();

        position = new Vector3(playerTransform.position.x + (xDir*radius), playerTransform.position.y + (yDir*radius), playerTransform.position.z);
		aimRotation = Quaternion.FromToRotation(Vector3.up, position);
        cursor.transform.position = position;
        x = 0;
        y = 0;
        xDir = 0;
        yDir = 0;
   }

   public void rocketImpulse()
   {
		float xDir = Input.GetAxisRaw("Horizontal");
        float yDir = Input.GetAxisRaw("Vertical");
		Debug.Log(xDir);
		
        float eulerRot = 0.0f;
		Vector3 position = new Vector3();
		
		if(xDir == -1)
		{
			if(yDir == 1) // Aiming up/right
				eulerRot = 45.0f;
			else if(yDir == -1) // Aiming down/right
				eulerRot = 135.0f;
			else	// Aiming right
				eulerRot = 90.0f;
		}
		else if(xDir == 1)
		{
			if(yDir == 1) // Aiming up/left
				eulerRot = -45.0f;
			else if(yDir == -1) // Aiming down/left
				eulerRot = -135.0f;
			else	// Aiming left
				eulerRot = -90.0f;
		}
		else 
		{
			if(yDir == 1) // Aiming up
				eulerRot = 0.0f;
			else	// Aiming down
				eulerRot = 180.0f;
		}
		
		aimRotation = Quaternion.Euler(0.0f, 0.0f, eulerRot);

        position = new Vector3(playerTransform.position.x + xDir, playerTransform.position.y + yDir, playerTransform.position.z);
		// aimRotation = Quaternion.FromToRotation(Vector3.up, position);
        cursor.transform.position = position;
        x = 0;
        y = 0;
        xDir = 0;
        yDir = 0;
	   
        var explosionFX =  Resources.Load<GameObject>("Prefabs/Explosion");
        MonoBehaviour.Instantiate(explosionFX,cursor.transform.position,aimRotation);
   }

   public void placeBomb(){
       var bomb =  Resources.Load<GameObject>("Prefabs/Bomb");
       Debug.Log(bomb);
       MonoBehaviour.Instantiate(bomb,cursor.transform.position,cursor.transform.rotation);
   }
}
