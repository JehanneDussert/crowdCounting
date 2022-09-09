using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGen : MonoBehaviour
{
    private List<Building>  buildings = new List<Building>{};

    [SerializeField]
    private ReadJson	readJson;

    [SerializeField]
    private ProcGen		procGen;

    void Start()
    {
        buildings = readJson.city.buildings;
        foreach (Building b in buildings)
        {
            Debug.Log("id: " + b.id);
        }
    }

    void Update()
    {
        
    }
}
