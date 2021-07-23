using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
    private PlayerSM _playerSm;
    public Rigidbody2D characterController;
    private float playerSpeed;
    private float gravityScale;
    private float jumpHeight;
    private float forceMultipler;
    private float friction;
    private float directionY;
    private float directionX;

    private float xBlast;
    private float yBlast;
    private Vector3 velocity;
    private bool isGrounded;
    private LayerMask mask;
    private Transform groundCheck;
    private Transform wallCheckR;
    private Transform wallCheckL;

    private Transform wallCheckT;
    int xDirection;
    int yDirection;
    bool facingRight = true;

    Animator animator;
	

    public MoveController(){

    }

    public MoveController(PlayerSM playerSM, Rigidbody2D controller, float speed, float gravityScale,float jumpHeight, float forceMultiplier, float friction, float xBlast, float yBlast, Transform groundCheck,Transform wallCheckR, Transform wallCheckL, Transform wallCheckT, LayerMask mask, Animator animator){
        this._playerSm = playerSM;
        this.characterController = controller;
        this.playerSpeed = speed;
        this.gravityScale = gravityScale;
        this.jumpHeight = jumpHeight;
        this.forceMultipler = forceMultiplier;
        this.friction = friction;
        this.xBlast = xBlast;
        this.yBlast = yBlast;
        this.groundCheck = groundCheck;
        this.wallCheckR = wallCheckR;
        this.wallCheckL = wallCheckL;
        this.wallCheckT = wallCheckT;
        this.mask = mask;
        this.animator = animator;
    }

    public void moveCharacter(){
        xDirection = (int)Input.GetAxisRaw("Horizontal");  
        yDirection = (int)Input.GetAxisRaw("Vertical");
        if(xDirection < 0 && _playerSm.transform.localScale.x == 1)_playerSm.transform.localScale = new Vector3(-1,1,1);
        if(xDirection > 0 && _playerSm.transform.localScale.x == -1)_playerSm.transform.localScale = new Vector3(1,1,1);
        if(isGrounded){
            velocity = new Vector3();
            directionX = xDirection * playerSpeed;
            if(Input.GetButtonDown("Jump") && isGrounded){
                animator.SetTrigger("Jump");
                directionY = jumpHeight;
                animator.SetFloat("ySpeed", Mathf.Abs(velocity.y));
                animator.SetTrigger("Jump");
            }
        }
    }

    public void applyGravity(){
        
        isGrounded = UnityEngine.Physics2D.OverlapCircle(groundCheck.position,0.1f,mask);
        animator.SetBool("Grounded",isGrounded);
        if(isGrounded)animator.ResetTrigger("Jump");
        if(!isGrounded && !Input.GetButton("Jump"))directionY += gravityScale * forceMultipler * Time.deltaTime; else if(!isGrounded) directionY += gravityScale * Time.deltaTime; else directionY = 0;
    }
    public void applyForces(){
       
        velocity.x = directionX;
        velocity.y = directionY;

        characterController.velocity = new Vector2();
        velocity = collisionCheck(velocity);
        animator.SetFloat("ySpeed", Mathf.Abs(velocity.y));
        animator.SetFloat("xSpeed", Mathf.Abs(velocity.x));
       
        characterController.velocity += ((Vector2)velocity * Time.fixedDeltaTime);
        

    }
    public void slide(){
       
        Vector3 frictionVec = velocity;
        frictionVec.y = 0;
        frictionVec.Normalize();
        frictionVec *= -1;
        frictionVec *= friction;
        velocity += frictionVec;

    }

    Vector2 collisionCheck(Vector2 posNextFrame){
       // Debug.Log(velocity + " " + posNextFrame);

       // Gets information about collisons
        RaycastHit2D hitRight = Physics2D.Raycast(wallCheckR.transform.position, Vector2.right,0.3f,mask);
        RaycastHit2D hitLeft = Physics2D.Raycast(wallCheckL.transform.position, Vector2.left,0.3f,mask);
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheck.transform.position, Vector2.left,0.3f,mask);
        RaycastHit2D topHit = Physics2D.Raycast(wallCheckT.transform.position, Vector2.up,0.3f,mask);

       // Stops them form moving left or right
        if(hitLeft.collider != null && xDirection == -1 && hitLeft.collider.tag != "OneWayPlatform")posNextFrame.x  = 0;
        if(hitRight.collider != null && xDirection == 1 && hitLeft.collider.tag != "OneWayPlatform")posNextFrame.x = 0;
        
       // Stops them form moving up unless it a One way plat form
        if(topHit.collider != null && Mathf.Sign(posNextFrame.y) == 1 && topHit.collider.tag != "OneWayPlatform"){
            posNextFrame.y = 0;
            applyGravity(); 
        } 

        
        //Debug.Log(posNextFrame);
        return posNextFrame;
        //RaycastHit2D hitLow = Physics2D.Raycast(groundCheck.transform.position, -direction,0.3f,1<<8);
    }

    public void Dash(){
    
        //reset the players momentum upon dash, if this isnt there and you rocket dash left, then rocket dash right,
        //because the forces are equal they cancel each other out and the player enters a stand still

        directionX = 0;
        directionY = 0;

        
        directionX = xBlast * -xDirection;
    
        //add a tiny bit of force in the y direction if the player is running on the ground and does a rocket jump, prevents walking code from overriding the blast code
        if(yDirection == 0) directionY = yBlast * -.25f; else directionY = yBlast * yDirection;
        // Debug.Log(new Vector2(directionX,directionY));
        // characterController.MovePosition(characterController.position +  new Vector2(directionX,directionY)*Time.deltaTime);
    }


    void checkMag(Vector2 posNextFrame){
        float mag = posNextFrame.magnitude;

    }
}
