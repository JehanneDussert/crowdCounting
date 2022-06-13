using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float    speed = 5f;
    public float    sensorLength = 1f;
    float           directionVal = 1.0f;
    float           turnVal = 0f;
    public float    turnSpeed = 50f;
    int             isTurningHash;

    Collider        myCollider;
    Animator        animator;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = transform.GetComponent<Collider>();
        animator = GetComponent<Animator>();
        isTurningHash = Animator.StringToHash("isTurning");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit  hit;
        int         flag = 0;
        bool        isTurning = animator.GetBool(isTurningHash);

        // Right Sensor
        if (Physics.Raycast(transform.position, transform.right, out hit, (sensorLength + transform.localScale.x)))
        {
            if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
            {
                animator.SetBool(isTurningHash, false);
                return ;
            }
            animator.SetBool(isTurningHash, true);
            turnVal -= 1;
            flag++;
        }
        // Left Sensor
        if (Physics.Raycast(transform.position, -transform.right, out hit, (sensorLength + transform.localScale.x)))
        {
            if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
            {
                animator.SetBool(isTurningHash, false);
                return ;
            }
            animator.SetBool(isTurningHash, true);
            turnVal += 1;
            flag++;
        }
        // Front Sensor
        if (Physics.Raycast(transform.position, transform.forward, out hit, (sensorLength + transform.localScale.z)))
        {
            if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
            {
                animator.SetBool(isTurningHash, false);
                return ;
            }
            animator.SetBool(isTurningHash, true);
            if (directionVal == 1f)
            {
                directionVal = -1;
            }
            flag++;
        }
        // Back Sensor
        if (Physics.Raycast(transform.position, -transform.forward, out hit, (sensorLength + transform.localScale.z)))
        {
            if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
            {
                animator.SetBool(isTurningHash, false);
                return ;
            }
            animator.SetBool(isTurningHash, true);
            if (directionVal == -1f)
            {
                directionVal = 1;
            }
            flag++;
        }

        if (flag == 0)
        {
            turnVal = 0;
        }

        transform.Rotate(Vector3.up * (turnSpeed * turnVal) * Time.deltaTime);

        transform.position += transform.forward * speed * directionVal * Time.deltaTime;
    }

    void    OnDrawGizmos()
    {
       Gizmos.DrawRay(transform.position, transform.forward * (sensorLength + transform.localScale.z));
       Gizmos.DrawRay(transform.position, -transform.forward * (sensorLength + transform.localScale.z));
       Gizmos.DrawRay(transform.position, transform.right * (sensorLength + transform.localScale.x));
       Gizmos.DrawRay(transform.position, -transform.right * (sensorLength + transform.localScale.x));
    }
}
