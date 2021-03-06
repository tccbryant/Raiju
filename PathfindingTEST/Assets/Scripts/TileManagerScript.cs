﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//might want to make a custom editor for this class
//it is hard to deal with the 2d array because it is
//hard to see what is going on inside it

public class TileManagerScript : MonoBehaviour {

    public GameObject[,] tile_array;
    public Sprite onSprite;
    public Sprite offSprite;
    public int length;
    public int width;

    public void Awake()
    {
        //should find a way to initialize without needing length and width
        CreateArray(GameObject.FindGameObjectsWithTag("tile"), length, width);
    }

    //sets up the 2d array based on the array of tiles
    public void CreateArray(GameObject[] tiles, int x, int y)
    {
        GameObject[,] test = new GameObject[x,y];
        foreach(GameObject tile in tiles)
        {
            int i = (int) tile.GetComponent<NodeAttributes>().x_coord;
            int j = (int) tile.GetComponent<NodeAttributes>().y_coord;
            test[i, j] = tile;
            //Debug.Log(i);
        }
        tile_array = test;

    }

    //returns the tile specified, null if the index doesnt exist
    public GameObject GetTile(int x, int y)
    {
        
        if (x < length && x >= 0 && y < width && y >= 0)
        {
            return tile_array[x, y];
        } return null;
    }

    public void ArrayContents()
    {
        int x = tile_array.GetLength(0);
        int y = tile_array.GetLength(1);

        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                GameObject tile = tile_array[i, j];
                NodeAttributes att = tile.GetComponent<NodeAttributes>();
                Debug.Log(att.x_coord + ", " + att.y_coord);
            }
        }

        Debug.Log(x + ", "+y);
    }
}
