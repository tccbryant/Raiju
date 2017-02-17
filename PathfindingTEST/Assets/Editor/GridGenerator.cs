using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridGenerator : ScriptableWizard {

    public int length;
    public int width;

    public GameObject tile;

    private string terrain_tag = "terrain";
    private string tile_manager_tag = "tile_manager";
    
	[MenuItem("Raiju Tools/Generate Grid...")]
    static void WizardScript()
    {
        ScriptableWizard.DisplayWizard<GridGenerator>("Generate Grid...", "Generate Grid");
    }

    private void OnWizardCreate()
    {
        //should make a function to check if there is a tile already there
        GenerateGrid(length, width);
        CreateTileManager();
    }

    //function to generate grid, should split into multiple functions eventually
    void GenerateGrid(int x, int y)
    {
        float curx = 0;
        float cury = 0;

        //create a terrain object if there isnt one already
        GameObject ter = CreateTerrain();
        
        
        //iterate up right axis
        for (int i =0; i< y; i++)
        {
            //these three lines of code organize the tiles into rows
            GameObject cur_row = new GameObject();
            cur_row.name = "row " + i;
            cur_row.transform.SetParent(ter.transform);

            //iterate along up left axis
            for(int j = 0; j < x; j++)
            {
                //Instantiates a tile prefab at the coordinates generated no rotation and as a child of the row object
                GameObject this_tile = (GameObject) Instantiate(tile, new Vector3(curx, cury, cury*2), Quaternion.identity, cur_row.transform);
                InitializeTile(this_tile, j, i);                

                //iterate along the row
                curx -= 1f;
                cury += 0.5f;
            }
            //move to the next column
            curx += x+1;
            cury -= x / 2 -0.5f;

            //fix the /2 error every other column
            if(x % 2 == 1)
            {
                cury -= 0.5f;
            }
        }
    }

    void CreateTileManager()
    {
        //if there is a tile manager already in the scene, destroy it
        GameObject tm = GameObject.FindGameObjectWithTag(tile_manager_tag);
        if(tm != null)
        {
            GameObject.DestroyImmediate(tm);
        }

        //initialize the tile manager
        tm = new GameObject();
        tm.tag = tile_manager_tag;
        tm.name = "tile_manager";
        TileManagerScript tm_script = tm.AddComponent<TileManagerScript>();

        tm_script.CreateArray(GameObject.FindGameObjectsWithTag("tile"),length, width);
        tm_script.length = length;
        tm_script.width = width;
    }

    //function to name and set the coordinates of each tile
    void InitializeTile(GameObject tile, int x, int y)
    {
        //name the tile based on its coordinate
        string tile_name = "(" + x + "," + y + ")";
        tile.name = tile_name;

        //fill in the tile's coordinates
        NodeAttributes att = tile.GetComponent<NodeAttributes>();
        att.x_coord = x;
        att.y_coord = y;
    }

    //function to create a terrain object if there isnt one in the scene already
    GameObject CreateTerrain()
    {
        GameObject iso_terrain = GameObject.FindGameObjectWithTag(terrain_tag);
        //creates a terrain object if there is not one in the scene
        if (iso_terrain == null)
        {
            iso_terrain = new GameObject();
            iso_terrain.name = "iso_terrain";
            iso_terrain.tag = terrain_tag;
        }
        return iso_terrain;
    }
}
