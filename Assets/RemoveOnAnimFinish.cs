using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnAnimFinish : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
