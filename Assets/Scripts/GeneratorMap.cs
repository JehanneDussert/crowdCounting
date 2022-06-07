using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorMap : MonoBehaviour
{
    public  Transform       _THELEVEL; //  stocke les objets
    public  TextAsset       level;      //  fichier texte de la map
    public  GameObject[]    lvlElements; //  élément à placer

    private string      txt;        //  récupérer le texte de notre fichier .txt
    private float       x = 0, y = 0, z = 0;    //  positions en temps réel
    private float       originalX, originalY, originalZ;    // positions d'origines

    void    createWall(int wallX, int wallY) {
        for (int i = -1; i < (wallX + 2); i++)
        {
            GameObject newWall1 = (GameObject)Instantiate(lvlElements[4], new Vector3(x + i, y, z), Quaternion.identity);
            GameObject newWall2 = (GameObject)Instantiate(lvlElements[4], new Vector3(x + i, y, z - (wallY - 1)), Quaternion.identity);
            newWall1.transform.parent = _THELEVEL;
            newWall2.transform.parent = _THELEVEL;
        }
        Debug.Log(wallY);
        for (int i = 0; i < wallY; i++)
        {
            Debug.Log("enter");
            GameObject newWall1 = (GameObject)Instantiate(lvlElements[4], new Vector3((x - 2), y, z - i), Quaternion.identity);
            GameObject newWall2 = (GameObject)Instantiate(lvlElements[4], new Vector3(x + wallX + 1, y, z - i), Quaternion.identity);
            newWall1.transform.parent = _THELEVEL;
            newWall2.transform.parent = _THELEVEL;
        }
    }

    void Start()
    {
        originalX = x;
        originalY = y;
        originalZ = z;

        txt = level.text;
        int lenX = txt.IndexOf('/');

        createWall(lenX, (txt.Length / lenX));
        for (int i = 0; i < txt.Length; i++)
        {
            if (txt.Substring(i, 1).ToLower() == "f") {
                GameObject newObject = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
                newObject.transform.parent = _THELEVEL;
                x++;
            }
            else if (txt.Substring(i, 1) == "/") {
                x = originalX;
                z--;
            }
            else if (txt.Substring(i, 1) == "*") {
                x++;
            }
            else if (txt.Substring(i, 1).ToLower() == "b") {
                GameObject newObject = (GameObject)Instantiate(lvlElements[1], new Vector3(x, y, z), Quaternion.identity);
                newObject.transform.parent = _THELEVEL;
                x++;
            }
            else if (txt.Substring(i, 1).ToLower() == "c") {
                GameObject newObject = (GameObject)Instantiate(lvlElements[2], new Vector3(x, y, z), Quaternion.identity);
                GameObject newFloor = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
                newObject.transform.parent = _THELEVEL;
                newFloor.transform.parent = _THELEVEL;
                x++;
            }
            else if (txt.Substring(i, 1).ToLower() == "p") {
                GameObject newObject = (GameObject)Instantiate(lvlElements[3], new Vector3(x, y, z), Quaternion.identity);
                GameObject newFloor = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
                newObject.transform.parent = _THELEVEL;
                newFloor.transform.parent = _THELEVEL;
                x++;
            }
        }

    }
}
