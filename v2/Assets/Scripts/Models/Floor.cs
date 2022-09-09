using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor
{
    public int FloorNumber { get; private set; }
    public int Rows { get; private set; }
    public int Columns { get; private set; }

    [SerializeField]
    public Room[,] rooms;
    public Floor(int floorNumber, Room[,] rooms)
    {
        FloorNumber = floorNumber;
        this.rooms = rooms;
    }
}
