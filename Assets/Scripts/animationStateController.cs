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

    void FixedUpdate()
    {
      transform.Translate(Vector3.right * -20f * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));
      transform.Translate(Vector3.forward * 20f * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
    }

    void Update()
    {
        bool    isRunning = animator.GetBool(isRunningHash);
        bool    isWalking = animator.GetBool(isWalkingHash);
        bool    forwardPressed = Input.GetKey("up");
        bool    backwardPressed = Input.GetKey("down");
        bool    leftPressed = Input.GetKey("left");
        bool    rightPressed = Input.GetKey("right");
        bool    runPressed = Input.GetKey("left shift");
        // Debug.Log("animation controller");

        if (!isWalking && (forwardPressed || backwardPressed))
        {
            Debug.Log("Forward pressed");
            animator.SetBool(isWalkingHash, true);
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
    }
}
