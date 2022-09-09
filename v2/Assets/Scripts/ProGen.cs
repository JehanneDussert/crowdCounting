using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ProGen : MonoBehaviour
{
    [Header("Prefab options")]
    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject roofPrefab;

    [SerializeField]
    private GameObject windowPrefab;

    [SerializeField]
    private GameObject doorPrefab;
    
    [SerializeField]
    private bool includeRoof = false;

    [SerializeField]
    private bool keepInsideWalls = false;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float windowPercentChance = 0.3f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float doorPercentChance = 0.2f;
    
    [Header("Grid options")]
    [SerializeField]
    [Range(1, 20)]
    private int rows = 3;

    [SerializeField]
    [Range(1, 20)]
    private int columns = 3;
    
    [SerializeField]
    [Range(0.0f, 20.0f)]
    private float cellUnitSize = 1;
    
    [SerializeField]
	[Range(1, 20)]
    private int numberOfFloors = 1;
    
    private Floor[] floors;

    public void Awake() => Generate();

    private List<GameObject> rooms = new List<GameObject>();
    
	private int prefabCounter = 0;
    
    public void Generate()
    {
        prefabCounter = 0;

		// clear
        Clear();

		// build data structure
        BuildDataStructure();

        // gen prefab
        Render();

		// removes inside walls
        if (!keepInsideWalls)
        {
            RemoveInsideWalls();
        }
    }

    void BuildDataStructure()
    {
        floors = new Floor[numberOfFloors];
        
		int floorCount = 0;

        foreach (Floor floor in floors)
        {
            Room[,] rooms = new Room[rows, columns];
            Debug.Log("creating rooms");

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    var roomPosition = new Vector3(row * cellUnitSize, floorCount, column * cellUnitSize);
                    rooms[row, column] = new Room(roomPosition, includeRoof ? (floorCount == floors.Length - 1) : false);
                    Debug.Log("pos: " + roomPosition);
					rooms[row, column].Walls[0] = new Wall(roomPosition, Quaternion.Euler(0, 0, 0));
					rooms[row, column].Walls[1] = new Wall(roomPosition, Quaternion.Euler(0, 90, 0));
					rooms[row, column].Walls[2] = new Wall(roomPosition, Quaternion.Euler(0, 180, 0));
					rooms[row, column].Walls[3] = new Wall(roomPosition, Quaternion.Euler(0, -90, 0));
				}
            }
            floors[floorCount] = new Floor(floorCount++, rooms);
        }
    }

    void Render()
    {
        foreach(Floor floor in floors)
        {
            for (int row = 0; floor.Rows < rows; row++)
            {
                for (int column = 0; column < floor.Columns; column++)
                {
                    Room room = floor.rooms[row, column];
                    GameObject roomGo = new GameObject($"Room_{row}_{column}");
                    rooms.Add(roomGo);
                    roomGo.transform.parent = transform;
                    if (floor.FloorNumber == 0)
                        RoomPlacement(UnityEngine.Random.Range(0.0f, 1.0f) <= doorPercentChance ? doorPrefab : wallPrefab, room, roomGo);
                    else
                        RoomPlacement(UnityEngine.Random.Range(0.0f, 1.0f) <= windowPercentChance ? windowPrefab : wallPrefab, room, roomGo);
                }
            }
        }
    }

    private void RoomPlacement(GameObject prefab, Room room, GameObject roomGo)
    {
        SpawnPrefab(prefab, roomGo.transform, room.Walls[0].Position, room.Walls[0].Rotation);
        SpawnPrefab(prefab, roomGo.transform, room.Walls[1].Position, room.Walls[1].Rotation);
        SpawnPrefab(prefab, roomGo.transform, room.Walls[2].Position, room.Walls[2].Rotation);
        SpawnPrefab(prefab, roomGo.transform, room.Walls[3].Position, room.Walls[3].Rotation);

        if (room.HasRoof)
        {
            SpawnPrefab(prefab, roomGo.transform, room.Walls[0].Position, room.Walls[0].Rotation);
        }
    }

	private void SpawnPrefab(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
	{
		var gameObject = Instantiate(prefab, position, rotation);
		gameObject.transform.parent = parent;
		gameObject.AddComponent<WallComponent>();
		gameObject.name = $"{gameObject.name}_{prefabCounter}";
		prefabCounter++;
	}

	void RemoveInsideWalls()
	{
		var WallComponents = GameObject.FindObjectOfType<WallComponent>();
		var childs = WallComponents.Select(c => c.transform.GetChild(0).position.ToString()).ToList();

		var dupPosition = childs.GroupBy(c => c)
			.Where(c => c.Count() > 1)
			.Select(grp => grp.Key)
			.ToList();

		foreach(WallComponent w in WallComponents)
		{
			var childTransform = w.transform.GetChild(0);
			if (dupPosition.Contains(childTransform.position.ToString()))
			{
				DestroyImmediate(childTransform.gameObject);
			}
		}
	}

	void Clear()
	{
        Debug.Log("clear()");
		for (int i = 0; i < rooms.Count; i++)
		{
			DestroyImmediate(rooms[i]);
		}
		rooms.Clear();
	}
}