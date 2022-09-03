using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator    animator;
    int         isWalkingHash;
    int         isWalkingBackwardsHash;
    int         isRunningHash;
    int         isTurningRightHash;
    int         isTurningLeftHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isWalkingBackwardsHash = Animator.StringToHash("isWalkingBackwards");
        isRunningHash = Animator.StringToHash("isRunning");
        isTurningRightHash = Animator.StringToHash("isTurningRight");
        isTurningLeftHash = Animator.StringToHash("isTurningLeft");
    }

    void Update()
    {
        bool    isRunning = animator.GetBool(isRunningHash);
        bool    isWalking = animator.GetBool(isWalkingHash);
        bool    isWalkingBackwards = animator.GetBool(isWalkingBackwardsHash);
        bool    isTurningLeft = animator.GetBool(isTurningLeftHash);
        bool    isTurningRight = animator.GetBool(isTurningRightHash);
        bool    forwardPressed = Input.GetKey("up");
        bool    backwardsPressed = Input.GetKey("down");
        bool    leftPressed = Input.GetKey("left");
        bool    rightPressed = Input.GetKey("right");
        bool    runPressed = Input.GetKey("left shift");
        
        if (!isWalking && forwardPressed)
        {
            Debug.Log("Forward pressed");
            animator.SetBool(isWalkingHash, true);
            Debug.Log(isWalkingHash);
        } 
        else if (isWalking && !forwardPressed)
        {
            Debug.Log("Stop walking");
            animator.SetBool(isWalkingHash, false);
        }
        else if (!isRunning && forwardPressed && runPressed)
        {
            Debug.Log("Start running");
            animator.SetBool(isRunningHash, true);
        }
        else if (isRunning && (!forwardPressed || !runPressed))
        {
            Debug.Log("Stop running");
            animator.SetBool(isRunningHash, false);
        }
        else if (!isWalkingBackwards && backwardsPressed)
        {
            Debug.Log("Backwards pressed");
            animator.SetBool(isWalkingBackwardsHash, true);
        } 
        else if (isWalkingBackwards && !backwardsPressed)
        {
            Debug.Log("Stop walking backwards");
            animator.SetBool(isWalkingBackwardsHash, false);
        }
        if (!isTurningLeft && leftPressed)
        {
            Debug.Log("Start turning left");
            animator.SetBool(isTurningLeftHash, true);
        }
        else if (isTurningLeft && !leftPressed)
        {
            Debug.Log("Stop turning left");
            animator.SetBool(isTurningLeftHash, false);
        }
        if (!isTurningRight && rightPressed)
        {
            Debug.Log("Start turning right");
            animator.SetBool(isTurningRightHash, true);
        }
        else if (isTurningRight && !rightPressed)
        {
            Debug.Log("Stop turning right");
            animator.SetBool(isTurningRightHash, false);
        }
    }
}
