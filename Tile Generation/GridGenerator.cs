using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridGenerator : ScriptableWizard {

    public int length;
    public int width;

    public GameObject tile;

    private string terrain_tag = "terrain";
    
	[MenuItem("Raiju Tools/Generate Grid...")]
    static void WizardScript()
    {
        ScriptableWizard.DisplayWizard<GridGenerator>("Generate Grid...", "Generate Grid");
    }

    private void OnWizardCreate()
    {
        //should make a function to check if there is a tile already there
        GenerateGrid(length, width);
    }

    //function to generate grid, should split into multiple functions eventually
    void GenerateGrid(int x, int y)
    {
        float curx = 0;
        float cury = 0;

        //creates a terrain object if there is not one in the scene
        if(GameObject.FindGameObjectWithTag("terrain") == null)
        {
            GameObject iso_terrain = new GameObject();
            iso_terrain.name = "iso_terrain";
            iso_terrain.tag = terrain_tag;
        }

        //hold the terrain object in a variable
        GameObject ter = GameObject.FindGameObjectWithTag(terrain_tag);
        
        //iterate along the rows
        for (int i =0; i< y; i++)
        {
            //these three lines of code organize the tiles into rows
            GameObject cur_row = new GameObject();
            cur_row.name = "row " + i;
            cur_row.transform.SetParent(ter.transform);

            //iterate along the columns
            for(int j = 0; j < x; j++)
            {
                //Instantiates a tile prefab at the coordinates generated no rotation and as a child of the row object
                GameObject this_tile = (GameObject) Instantiate(tile, new Vector3(curx, cury, cury*2), Quaternion.identity, cur_row.transform);
                InitializeTile(this_tile, i, j);                

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

    //function to name and set the coordinates of each tile
    void InitializeTile(GameObject tile, int x, int y)
    {
        //name the tile based on its coordinate
        string tile_name = "(" + x + "," + y + ")";
        tile.name = tile_name;

        //fill in the tile's coordinates
        Node_Attributes att = tile.GetComponent<Node_Attributes>();
        att.x_coord = x;
        att.y_coord = y;
    }
}
