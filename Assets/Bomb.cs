using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    [Tooltip ("A timer in seconds how long till the bomb fizzles out onces its been activated")]
    private float timer;
    public bool active {get; set;}

    Animator animator;

    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = timer;
        active = false;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(active){
            animator.SetTrigger("Activate");
            Destroy (gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 0.5f); 
        }
    }
}
