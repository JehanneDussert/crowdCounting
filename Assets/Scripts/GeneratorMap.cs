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
        x += 6;
    }

    void    createBuildings(ref float x, float y, float z) {
        GameObject newObject = (GameObject)Instantiate(lvlElements[1], new Vector3(x, y, z), Quaternion.identity);
        GameObject newFloor = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
        newObject.transform.parent = _THELEVEL;
        newFloor.transform.parent = _THELEVEL;
        x += 6;
    }

    void    createPlayer(ref float x, float y, float z) {
        GameObject newObject = (GameObject)Instantiate(lvlElements[3], new Vector3(x, y, z), Quaternion.identity);
        GameObject newFloor = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
        newObject.transform.parent = _THELEVEL;
        newFloor.transform.parent = _THELEVEL;
        x += 6;
    }

    void    createFloor(ref float x, float y, float z) {
        GameObject newObject = (GameObject)Instantiate(lvlElements[0], new Vector3(x, y, z), Quaternion.identity);
        newObject.transform.parent = _THELEVEL;
        x += 6;
    }

    void    createWall(int wallX, int wallY) {
        Debug.Log("z: ");
        Debug.Log(y);
        float   minHeight = z + 3;
        float   maxHeight = z - (wallY * 6) + 3;
        float   maxWidth = wallX * 6;
        
        for (float tmpX = x; tmpX < maxWidth; tmpX += 6)
        {
            GameObject newWall1 = (GameObject)Instantiate(lvlElements[4], new Vector3(tmpX, y, minHeight), Quaternion.identity);
            GameObject newWall2 = (GameObject)Instantiate(lvlElements[4], new Vector3(tmpX, y, maxHeight), Quaternion.identity);
            newWall1.transform.parent = _THELEVEL;
            newWall2.transform.parent = _THELEVEL;
        }
        for (float tmpY = y; tmpY > maxHeight; tmpY -= 6)
        {
            GameObject newWall1 = (GameObject)Instantiate(lvlElements[5], new Vector3(x - 3, y, tmpY), Quaternion.identity);
            GameObject newWall2 = (GameObject)Instantiate(lvlElements[5], new Vector3(maxWidth - 3, y, tmpY), Quaternion.identity);
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
        int lenY = txt.Count(f => f == '/');
        int count = txt.Count(f => f == 'c');
        Debug.Log("Len X: ");
        Debug.Log(lenX);
        Debug.Log("Len Y: ");
        Debug.Log(lenY);

        createWall(lenX, lenY);

        for (int i = 0; i < txt.Length; i++)
        {
            if (txt.Substring(i, 1).ToLower() == "f") {
                createFloor(ref x, y, z);
            }
            else if (txt.Substring(i, 1) == "/") {
                x = originalX;
                z -= 6;
            }
            else if (txt.Substring(i, 1) == "*") {
                x += 6;
            }
            else if (txt.Substring(i, 1).ToLower() == "b") {
                createBuildings(ref x, y, z);
            }
            else if (txt.Substring(i, 1).ToLower() == "c") {
                Debug.Log("creation of a new character");
                createCrowd(ref x, y, z);
            }
            else if (txt.Substring(i, 1).ToLower() == "p") {
                Debug.Log("Create new player");
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
