using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GeneratorMap : MonoBehaviour
{
    public  Transform       _THELEVEL; //  stocke les objets
    public  TextAsset       level;      //  fichier texte de la map
    public  GameObject[]    lvlElements; //  élément à placer

    private string      txt;        //  récupérer le texte de notre fichier .txt
    private float       x = 0, y = 0, z = 0;    //  positions en temps réel
    private float       originalX, originalY, originalZ;    // positions d'origines

    void    createCrowd(ref float x, float y, float z) {
        GameObject newObject = (GameObject)Instantiate(lvlElements[2], new Vector3(x, y, z), Quaternion.identity);
        GameObject newFloor = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
        newObject.transform.parent = _THELEVEL;
        newFloor.transform.parent = _THELEVEL;
        x++;
    }

    void    createBuildings(ref float x, float y, float z) {
        GameObject newObject = (GameObject)Instantiate(lvlElements[1], new Vector3(x, y, z), Quaternion.identity);
        newObject.transform.parent = _THELEVEL;
        x++;
    }

    void    createPlayer(ref float x, float y, float z) {
        GameObject newObject = (GameObject)Instantiate(lvlElements[3], new Vector3(x, y, z), Quaternion.identity);
        GameObject newFloor = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
        newObject.transform.parent = _THELEVEL;
        newFloor.transform.parent = _THELEVEL;
        x++;
    }

    void    createFloor(ref float x, float y, float z) {
        GameObject newObject = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
        newObject.transform.parent = _THELEVEL;
        x++;
    }

    void    createWall(int wallX, int wallY) {
        for (int i = -1; i < (wallX + 2); i++)
        {
            GameObject newWall1 = (GameObject)Instantiate(lvlElements[4], new Vector3(x + i, y, z), Quaternion.identity);
            GameObject newWall2 = (GameObject)Instantiate(lvlElements[4], new Vector3(x + i, y, z - (wallY - 1)), Quaternion.identity);
            newWall1.transform.parent = _THELEVEL;
            newWall2.transform.parent = _THELEVEL;
        }
        for (int i = 0; i < wallY; i++)
        {
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
        int count = txt.Count(f => f == 'c');
        Debug.Log("COUNT: ");
        Debug.Log(count);

        createWall(lenX, (txt.Length / lenX));

        for (int i = 0; i < txt.Length; i++)
        {
            if (txt.Substring(i, 1).ToLower() == "f") {
                createFloor(ref x, y, z);
            }
            else if (txt.Substring(i, 1) == "/") {
                x = originalX;
                z--;
            }
            else if (txt.Substring(i, 1) == "*") {
                x++;
            }
            else if (txt.Substring(i, 1).ToLower() == "b") {
                createBuildings(ref x, y, z);
            }
            else if (txt.Substring(i, 1).ToLower() == "c") {
                createCrowd(ref x, y, z);
            }
            else if (txt.Substring(i, 1).ToLower() == "p") {
                createPlayer(ref x, y, z);
            }
            else if (txt.Substring(i, 1).ToLower() == "a") {
                GameObject newObject = (GameObject)Instantiate(lvlElements[6], new Vector3(x, y, z), Quaternion.identity);
                GameObject newFloor = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
                newObject.transform.parent = _THELEVEL;
                newFloor.transform.parent = _THELEVEL;
                x++;
                Debug.Log("animation");
            }
        }

    }
}
