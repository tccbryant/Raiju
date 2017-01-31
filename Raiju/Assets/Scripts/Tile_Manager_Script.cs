using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Manager_Script : MonoBehaviour {

    public GameObject[,] tile_array;

    //sets up the 2d array based on the array of tiles
    public void CreateArray(GameObject[] tiles, int x, int y)
    {
        GameObject[,] test = new GameObject[x,y];
        foreach(GameObject tile in tiles)
        {

        }

    }
}
