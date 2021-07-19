using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    Transform[] controlPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() {
        for (float i = 0; i < 1; i += 0.05f){
            Vector3 pos;
            pos = Mathf.Pow(1-i,3) * controlPoints[0].position +
                  3 * Mathf.Pow(1-i,2) * i * controlPoints[1].position +
                  3 * (1-i) * Mathf.Pow(i,2) * controlPoints[2].position +
                  Mathf.Pow(i,3) * controlPoints[3].position;
            Gizmos.DrawSphere(pos,0.1f);
        }
    }
}
