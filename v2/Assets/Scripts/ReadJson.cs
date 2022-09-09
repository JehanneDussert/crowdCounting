using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // lire / écrire fichiers
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Data;

public class ReadJson : MonoBehaviour
{
    public GameObject	floor;
    public string       fileName;
	public City			city = new City();

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

	List<Building>	GetBuildings()
	{
		return city.buildings;
	}

	void	createBase(City city)
	{
		GameObject newObject = Instantiate(floor, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;  // instatiate the object
		newObject.transform.localScale = new Vector3(city.width, 1, city.height); // change its local scale in x y z format
	}

    void Start()
    {
		city = parseJson(Read(), city);
		createBase(city);
    }

    void Update()
    {

    }
}

public class Building {
		public int			id;
		public float		x;
		public float		y;
		public int			nbOfFloors;
		public string		type;
		public bool			includeRoof;
		public int			width;
		public int			height;
		
		[SerializeField]
    	public BuildingGen	buildingGen;
}

public class City {
	public List<Building>   buildings;
	public string	        name;
	public float	        width;
	public float	     	height;
	public int				inhabitants;
}