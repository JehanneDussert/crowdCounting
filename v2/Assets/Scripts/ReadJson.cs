using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // lire / écrire fichiers
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Data;

public class ReadJson : MonoBehaviour
{
	public string fileName;
	void Start()
	{
		string	str = Read();
		var		buildings = new List<Building>{};

		JObject Data = JObject.Parse(str);
		foreach (JObject jo in Data["data"])
		{
			buildings.Add(JsonUtility.FromJson<Building>(jo.ToString()));
		}
		// for (int i = 0; i < buildings.Count; i++)
		// {
		// 	Debug.Log("id: " + buildings[i].id);
		// 	Debug.Log("x: " + buildings[i].x);
		// 	Debug.Log("y: " + buildings[i].y);
		// 	Debug.Log("floors: " + buildings[i].floors);
		// 	Debug.Log("type: " + buildings[i].type);
		// }
	}

	void Update()
	{
		
	}
	string Read() {
		StreamReader sr = new StreamReader(Application.dataPath + "/" + fileName);
		string content = sr.ReadToEnd();
		sr.Close();

		return content;
	}
}

// public class Building {
// 		public int		id;
// 		public float	x;
// 		public float	y;
// 		public int		floors;
// 		public string	type;
// }

// public class City {
// 	public Building []	buildings;
// 	public string	name;
// 	public float	width;
// 	public float	height;
// 	public int		inhabitants;
// }