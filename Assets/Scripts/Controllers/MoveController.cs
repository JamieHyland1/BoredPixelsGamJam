using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController {
    private PlayerSM _playerSm;
    private CharacterController characterController;
    private float playerSpeed;
	private float playerAccel = 0.25f;
	private float playerAirAccel = 0.15f;
    private float gravityScale;
    private float jumpHeight;
    private float forceMultipler;
    private float friction;
    private float directionY;
    private float directionX;
	
	private float jumpDelay = 0.1f;
	private float currentJumpDelay;

    private float xBlast;
    private float yBlast;
    private Vector3 velocity;
	private bool prevGrounded;
    private bool isGrounded;
    private LayerMask mask;
    private Transform groundCheck;
    int xDirection;
    int yDirection;
    bool facingRight = true;

    Animator animator;

    public MoveController(){

    }

    public MoveController(PlayerSM playerSM, CharacterController controller, float speed, float gravityScale,float jumpHeight, float forceMultiplier, float friction, float xBlast, float yBlast, Transform groundCheck, LayerMask mask, Animator animator){
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
        this.mask = mask;
        this.animator = animator;
		currentJumpDelay = jumpDelay;
    }

    public void moveCharacter(){
		
        xDirection = (int)Input.GetAxisRaw("Horizontal");  
        yDirection = (int)Input.GetAxisRaw("Vertical");
        if(xDirection < 0 && _playerSm.transform.localScale.x == 1)_playerSm.transform.localScale = new Vector3(-1,1,1);
        if(xDirection > 0 && _playerSm.transform.localScale.x == -1)_playerSm.transform.localScale = new Vector3(1,1,1);
        if(isGrounded){
            velocity = new Vector3();
			
			if(xDirection != 0)	// Heavily decelerate when no input in horizontal direction.
			{
				directionX += xDirection * playerAccel;
				if(Mathf.Abs(directionX) > playerSpeed)
				{
					directionX = playerSpeed * Mathf.Sign(directionX);
				}
			}
			else
			{
				float tempDirectionX = directionX;
				directionX -= directionX * (3 * playerAccel);
				if(Mathf.Sign(tempDirectionX) != Mathf.Sign(directionX)) // We decelerated too far and passed stopping point
				{
					directionX = 0.0f;
				}
				
				xDirection = (int)(-3 * Mathf.Sign(directionX));
			}
			
            if(Input.GetButton("Jump") && isGrounded){
				currentJumpDelay -= Time.deltaTime;
				animator.SetTrigger("Jump");
				
				// Debug.Log(currentJumpDelay);
				
				if(currentJumpDelay <= 0)
				{
					currentJumpDelay = 0;
					directionY = jumpHeight;
					animator.SetFloat("ySpeed", Mathf.Abs(velocity.y));
				}
            }
			else
			{
				currentJumpDelay = jumpDelay;
			}
        }
		else // Mitigated player movement while not grounded
		{
			if(xDirection != 0)
			{
				directionX += xDirection * playerAirAccel;
				if(Mathf.Abs(directionX) > playerSpeed)
				{
					directionX = playerSpeed * Mathf.Sign(directionX);
				}
			}
			else
			{
				float tempDirectionX = directionX;
				directionX -= directionX * (3 * playerAccel);
				if(Mathf.Sign(tempDirectionX) != Mathf.Sign(directionX)) // We decelerated too far and passed stopping point
				{
					directionX = 0.0f;
				}
				
				xDirection = (int)(-3 * Mathf.Sign(directionX));
			}
		}
    }

    public void applyGravity(){
        
		prevGrounded = isGrounded;
        isGrounded = UnityEngine.Physics.CheckSphere(groundCheck.position,0.1f,mask);
		if(prevGrounded != isGrounded && isGrounded) // Weren't grounded last frame, but we are now.
		{
			animator.SetTrigger("Land");
		}
		else if(!prevGrounded)
		{
			animator.ResetTrigger("Land");
		}
        animator.SetBool("Grounded",isGrounded);
        if(isGrounded)animator.ResetTrigger("Jump");
        if(!isGrounded && !Input.GetButton("Jump"))directionY += gravityScale * forceMultipler * Time.deltaTime; else if(!isGrounded) directionY += gravityScale * Time.deltaTime; else directionY = 0;
    }
    public void applyForces(){
       
        velocity.x = directionX;
        velocity.y = directionY;

        animator.SetFloat("ySpeed", velocity.y);
        animator.SetFloat("xSpeed", Mathf.Abs(velocity.x));

        // Debug.Log((velocity.y));  // Commented debug spam
        characterController.Move(velocity*Time.deltaTime);

    }
    public void slide(){
       
        Vector3 frictionVec = velocity;
        frictionVec.y = 0;
        frictionVec.Normalize();
        frictionVec *= -1;
        frictionVec *= friction;
        velocity += frictionVec;

    }

    public void Dash(){
    
        //reset the players momentum upon dash, if this isnt there and you rocket dash left, then rocket dash right,
        //because the forces are equal they cancel each other out and the player enters a stand still

        directionX = 0;
        directionY = 0;

        
        directionX = xBlast * -xDirection;
        //add a tiny bit of force in the y direction if the player is running on the ground and does a rocket jump, prevents walking code from overriding the blast code
        if(yDirection == 0) directionY = yBlast * -.25f; else directionY = yBlast * yDirection;
    }
}
