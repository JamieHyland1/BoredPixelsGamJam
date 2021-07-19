using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : StateMachine
{
   public NeutralState neutralState {get; private set;}
   public SlidingState slidingState {get; private set;}
   
   enum States {neutral, sliding};
   [Header ("Starting state")]
   [SerializeField]
   States startingState;

   [SerializeField]
    int playerHealth = 25;
    public CharacterController controller;
    
    [SerializeField, Range(0f,10f)]
    public float speed = 12f;
    
    [Tooltip("This determins how fast the player falls when the jump button is pressed quickly, when held longer the player jumps further")]
    [SerializeField,Range(0,5)]
    float forceMultipler;
    
    [Tooltip("The force of gravity applied to the player each frame")]
    [SerializeField, Range(-30,30)]
    public float gravityScale;
    
    [Tooltip("How much friction is applied to the player when they are in a block of ice")]
    [SerializeField,Range(0,1)]
    float friction;

    [Tooltip("How much force is applied to the player in the horizontal when doing a rocket jump")]
    [SerializeField,Range(1,75)]
    float xBlast;

    [Tooltip("How much force is applied to the player in the vertical when doing a rocket jump")]
    [SerializeField,Range(1,-75)]
    float yBlast;
    
    
    
    [Tooltip("This checks to see if the player is touching the ground or not")]
    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask mask;

    [Tooltip("This is a pop up note hovering over the var in inspector")]
    [SerializeField]
    Transform shootingTransform;

    [SerializeField, Range(0,50)]
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
        moveController = new MoveController(this,controller,speed,gravityScale, jumpHeight, forceMultipler, friction, xBlast, yBlast, groundCheck, mask, animator);
        shootController = new ShootController(cursor,radius, this.transform);
        neutralState = new NeutralState(this,moveController,shootController);
        slidingState = new SlidingState(this,5f,moveController);
    }

   private void Start() {
      if(startingState == States.neutral)this.ChangeState(neutralState);
   }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(groundCheck.position,0.3f);    
    }
}
