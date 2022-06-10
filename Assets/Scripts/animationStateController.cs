using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator    animator;
    int         isWalkingHash;
    int         isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool    isRunning = animator.GetBool(isRunningHash);
        bool    isWalking = animator.GetBool(isWalkingHash);
        bool    forwardPressed = Input.GetKey("up");
        bool    backwardPressed = Input.GetKey("down");
        bool    leftPressed = Input.GetKey("left");
        bool    rightPressed = Input.GetKey("right");
        bool    runPressed = Input.GetKey("left shift");

    //   transform.Translate(Vector3.right * -20f * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
    //   transform.Translate(Vector3.forward * 20f * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        } 
        else if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
        else if (!isRunning && forwardPressed && runPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
