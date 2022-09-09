using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGen : MonoBehaviour
{
    private List<Building>  buildings = new List<Building>{};

    [SerializeField]
    private ReadJson	readJson;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject roofPrefab;
    
    // [SerializeField]
    // private bool includeRoof = false;
    
    // [SerializeField]
    // private int width = 3;
    
    // [SerializeField]
    // private int height = 3;
    
    [SerializeField]
    private float cellUnitSize = 1;
    
    // [SerializeField]
    // private int nbOfFloors = 1;
    
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

		Debug.Log("ID " + b.id);
		Debug.Log("w: " + b.width);
		Debug.Log("h: " + b.height);
		Debug.Log("nb fl: " + b.nbOfFloors);
        foreach (Floor floor in floors)
        {
			Debug.Log("Generate each floor");
            Room[,] rooms = new Room[b.width, b.height];
            for (int i = 0; i < b.width; i++)
            {
                for (int j = 0; j < b.height; j++)
                {
                    rooms[i, j] = new Room(new Vector2(i * cellUnitSize, j * cellUnitSize),
                    b.includeRoof ? (floorCount == floors.Length - 1) : false);
                }
            }
            floors[floorCount] = new Floor(floorCount++, rooms);
        }
    }

    void Render(Building b)
    {
        foreach(Floor floor in floors)
        {
            for(int i = 0; i < b.width; i++)
            {
                for(int j = 0; j < b.height; j++)
                {
                    Room room = floor.rooms[i, j];
                    var wall1 = Instantiate(wallPrefab, new Vector3(room.RoomPosition.x, floor.FloorNumber, room.RoomPosition.y), Quaternion.Euler(0, 0, 0));
                    wall1.transform.parent = transform;
                    var wall2 = Instantiate(wallPrefab, new Vector3(room.RoomPosition.x, floor.FloorNumber, room.RoomPosition.y), Quaternion.Euler(0, 90, 0));
                    wall2.transform.parent = transform;
                    var wall3 = Instantiate(wallPrefab, new Vector3(room.RoomPosition.x, floor.FloorNumber, room.RoomPosition.y), Quaternion.Euler(0, 180, 0));
                    wall3.transform.parent = transform;
                    var wall4 = Instantiate(wallPrefab, new Vector3(room.RoomPosition.x, floor.FloorNumber, room.RoomPosition.y), Quaternion.Euler(0, -90, 0));
                    wall4.transform.parent = transform;

                    if (room.HasRoof)
                    {
                        var roof = Instantiate(roofPrefab, new Vector3(room.RoomPosition.x, floor.FloorNumber, room.RoomPosition.y), Quaternion.identity);
                        roof.transform.parent = transform;
                    }
                }
            }
        }
    }
}
