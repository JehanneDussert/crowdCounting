using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class Duplicate : MonoBehaviour
{
    public GameObject origin;
//     static void Main()
//     {
//       Random rd = new Random();

//       int rand = rd.Next(100,200);

//       Console.WriteLine(rand);
//    }
    // Start is called before the first frame update
    void Start()
    {
        Random  rd = new Random();
        int     rand = rd.Next(50, 150);
        Console.WriteLine(rand);

        createClone(rand);
    }

    void    createClone(int n)
    {
        Random rd = new Random();
        double rand = rd.NextDouble() * (0.2f - 0.1f) + 0.1f; //for doubles

        for (int i = 0; i < n; i++)
        {
            GameObject clone = Instantiate(origin, new Vector3(i * (float)rand, origin.transform.position.y, i * (float)rand), origin.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
