using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public Transform Point1;
    public Transform Point2;
    public float speed;
    public Transform startPoint;

    Vector3 nextPoint;

    // Start is called before the first frame update
    void Start()
    {
        nextPoint = startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == Point1.position) {

            nextPoint = Point2.position;

        }
        if (transform.position == Point2.position) {

            nextPoint = Point1.position;

        }

        transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed*Time.deltaTime);
    }

    private void OnDrawGizmos() {

        Gizmos.DrawLine(Point1.position, Point1.position);

    }

}
