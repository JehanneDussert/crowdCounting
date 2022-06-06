using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    // = method
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
		// translate = method pour faire bouger un objet
		// Time.fixedDeltaTime = pour calibrer l'accélération qu'on va faire en fonction de la machine sur lequel
		// jeu est effectué
		transform.Translate(Vector3.forward * 5f * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
		transform.Translate(Vector3.right * 5f * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));
    }
}
