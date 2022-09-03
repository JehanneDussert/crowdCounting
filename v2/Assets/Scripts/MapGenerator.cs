using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // lire / écrire fichiers
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Data;

public class MapGenerator : MonoBehaviour
{
    // public Transform    parent;
    public GameObject[]	mapElement;
    public string       fileName;
	City	city = new City();

	string Read()
	{
		StreamReader sr = new StreamReader(Application.dataPath + "/" + fileName);
		string content = sr.ReadToEnd();
		sr.Close();

		return content;
	}

	City	parseJson(string str, City city)
	{
		var		buildings = new List<Building>{};
		JObject Data = JObject.Parse(str);

		// city = JsonUtility.FromJson<City>(Data.ToString());
		foreach (JObject jo in Data["city"])
		{
			city = JsonUtility.FromJson<City>(jo.ToString());
			JObject DataB = JObject.Parse(jo.ToString());
			foreach (JObject joB in DataB["buildings"])
			{
				buildings.Add(JsonUtility.FromJson<Building>(joB.ToString()));
			}
		}
		city.buildings = buildings;
		return city;
	}

	void	createBase(City city)
	{
		GameObject newObject = Instantiate(mapElement[0], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;  // instatiate the object
		newObject.transform.localScale = new Vector3(city.width, 1, city.height); // change its local scale in x y z format
	}

    void    createBuildings(List<Building> buildings) {
		for (int i = 0; i < buildings.Count; i++)
        {
			GameObject newObject = (GameObject)Instantiate(mapElement[1], new Vector3(buildings[i].x, 10, buildings[i].y), Quaternion.identity);
            // newObject.transform.parent = parent;
        }
    }

	void	createMap(City city)
	{
		createBase(city);
		createBuildings(city.buildings);
		// createTraffic();	-> roads, cars, traffic lights
		// createPedestrians();
		// createSky();
		// createTrees();
	}

    void Start()
    {
		city = parseJson(Read(), city);
		createMap(city);  
    }

    void Update()
    {

    }
}

public class Building {
		public int		id;
		public float	x;
		public float	y;
		public int		floors;
		public string	type;
}

public class City {
	public List<Building>   buildings;
	public string	        name;
	public float	        width;
	public float	     	height;
	public int				inhabitants;
}