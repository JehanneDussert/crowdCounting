using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildingGen : MonoBehaviour
{
    private List<Building>  buildings = new List<Building>{};
	public GameObject[]		prefabObjects;

    [SerializeField]
    private ReadJson	readJson;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject doorPrefab;

    [SerializeField]
    private GameObject wdwPrefab;

    [SerializeField]
    private GameObject roofPrefab;
    
    [SerializeField]
    private Floor[] floors;

    [SerializeField]
    private Building building;
    
    public void Start() 
    {
        buildings = readJson.city.buildings;

        foreach (Building b in buildings)
        {
			Generate(b);
			Render(b);
        }
    }
    
    void Generate(Building b)
    {
        floors = new Floor[b.nbOfFloors];
        int floorCount = 0;

        foreach (Floor floor in floors)
        {
            Room[,] rooms = new Room[b.width, b.height];
            for (int i = 0; i < b.width; i++)
            {
                for (int j = 0; j < b.height; j++)
                {
                    rooms[i, j] = new Room(new Vector2(i, j),
                    b.includeRoof ? (floorCount == floors.Length - 1) : false);
                }
            }
            floors[floorCount] = new Floor(floorCount++, rooms);
        }
    }

    void Render(Building b)
    {
		float newX = b.x;
		float newY = b.y;
		GameObject prefab;

        foreach(Floor floor in floors)
        {
			newX = b.x;
            for(int i = 0; i < b.width; i++)
            {
				newY = b.y;
                for(int j = 0; j < b.height; j++)
                {
					if (floors.First() == floor && i == b.width/2)
						prefab = doorPrefab;
					else
						prefab = prefabObjects[Random.Range(0, 2)];
                    Room room = floor.rooms[i, j];
					var wall1 = Instantiate(prefab, new Vector3(newX, floor.FloorNumber, newY), Quaternion.Euler(0, 0, 0));
                    wall1.transform.parent = transform;
                    var wall2 = Instantiate(prefab, new Vector3(newX, floor.FloorNumber, newY), Quaternion.Euler(0, 90, 0));
                    wall2.transform.parent = transform;
                    var wall3 = Instantiate(prefab, new Vector3(newX, floor.FloorNumber, newY), Quaternion.Euler(0, 180, 0));
                    wall3.transform.parent = transform;
                    var wall4 = Instantiate(prefab, new Vector3(newX, floor.FloorNumber, newY), Quaternion.Euler(0, -90, 0));
                    wall4.transform.parent = transform;

                    if (room.HasRoof)
                    {
                        var roof = Instantiate(roofPrefab, new Vector3(newX, floor.FloorNumber, newY), Quaternion.identity);
                        roof.transform.parent = transform;
                    }
					newY++;
                }
				newX++;
            }
        }
    }
}
