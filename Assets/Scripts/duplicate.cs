using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duplicate : MonoBehaviour
{
    public GameObject originalChar;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject charClone = Instantiate(originalChar);
        createClone(5);
    }

    void    createClone(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject charClone = Instantiate(originalChar, new Vector3(i * 0.6f, originalChar.transform.position.y, i * 0.75f), originalChar.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
