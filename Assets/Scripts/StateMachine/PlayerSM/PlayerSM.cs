using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSM : StateMachine
{
   public NeutralState neutralState {get; private set;}
   public SlidingState slidingState {get; private set;}
   
   enum States {neutral, sliding};
   [Header ("Starting state")]
   [SerializeField]
   States startingState;

   [SerializeField]
    int playerHealth = 1;
    public Rigidbody2D controller;
    
    [SerializeField, Range(0f,1000f)]
    public float speed = 12f;
    
    [Tooltip("This determins how fast the player falls when the jump button is pressed quickly, when held longer the player jumps further")]
    [SerializeField,Range(0,5)]
    float forceMultipler;
    
    [Tooltip("The force of gravity applied to the player each frame")]
    [SerializeField, Range(-300,300)]
    public float gravityScale;
    
    [Tooltip("How much friction is applied to the player when they are in a block of ice")]
    [SerializeField,Range(0,1)]
    float friction;

    [Tooltip("How much force is applied to the player in the horizontal when doing a rocket jump")]
    [SerializeField,Range(1,7500)]
    float xBlast;

    [Tooltip("How much force is applied to the player in the vertical when doing a rocket jump")]
    [SerializeField,Range(1,-7500)]
    float yBlast;
    
    
    [Header("Colission checks")]
    [Tooltip("This checks to see if the player is touching the ground or not")]
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform wallCheckR;
    [SerializeField]
    Transform wallCheckL;

    [SerializeField]
    Transform wallCheckT;


    [SerializeField]
    LayerMask mask;
    
    [Tooltip("This is a pop up note hovering over the var in inspector")]
    [SerializeField]
    Transform shootingTransform;

    [SerializeField, Range(0,5000)]
    float jumpHeight;

    [SerializeField]
    GameObject cursor;

    [SerializeField]
    Animator animator;
    
    [SerializeField]
    float radius;
    
	MoveController moveController;
    ShootController shootController;
	
    private void Awake() {
        controller = this.GetComponent<Rigidbody2D>();
        moveController = new MoveController(this,controller,speed,gravityScale, jumpHeight, forceMultipler, friction, xBlast, yBlast, groundCheck, wallCheckR, wallCheckL, wallCheckT , mask, animator);
        shootController = new ShootController(cursor,radius, this.transform);

        neutralState = new NeutralState(this,moveController,shootController);
        slidingState = new SlidingState(this,5f,moveController, controller,animator);
    }
   private void Start() {
      if(startingState == States.neutral)this.ChangeState(neutralState);
   }
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(groundCheck.position,0.1f);    
    }

    public void hit(){
		
		this.playerHealth -= 1;
		
		if(this.playerHealth > 0)	// We have health left and shouldn't exit the level
		{
			animator.SetTrigger("Hit");
		}
		else if(this.playerHealth > -1) // Only one instance of the death explosion
		{
			var explosionFX =  Resources.Load<GameObject>("Prefabs/DeathExplosion");
			MonoBehaviour.Instantiate(explosionFX,gameObject.transform.position,gameObject.transform.rotation);
			animator.SetTrigger("Dead");
			
			Destroy(gameObject.GetComponentInChildren<Rigidbody2D>());
			Destroy(gameObject.GetComponentInChildren<SpriteRenderer>());
			//moveController.characterController.velocity = new Vector2();
			
			StartCoroutine(Delay());			
		}		
    }
	
	private IEnumerator Delay()
	{
		yield return new WaitForSeconds(4);
		SceneManager.LoadScene(0);
	}

     private void OnTriggerEnter2D(Collider2D other) {

        // Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Player"){
            // isPressed = true;
            // animator.SetBool("IsPressed",isPressed);
        }    
    }

    private void OnTriggerExit2D(Collider2D other) {
         // Debug.Log(other.gameObject.name);
            if(other.gameObject.tag == "Player"){
            // isPressed = false;
            // animator.SetBool("IsPressed",isPressed);
        }
    }
}
