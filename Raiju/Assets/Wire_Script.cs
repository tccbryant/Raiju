using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//wire script and source script should be children of the same class
//need to implement this class so they can inherit the stuff.

public class Wire_Script : MonoBehaviour {
    public bool current = false;

    //horribly written code, needs to be cleaned and separated
    public void SendCurrent()
    {
        GameObject tm = GameObject.FindGameObjectWithTag("tile_manager");
        Tile_Manager_Script tm_script = tm.GetComponent<Tile_Manager_Script>();
        Node_Attributes att = GetComponent<Node_Attributes>();
        GetComponent<SpriteRenderer>().sprite = tm_script.onSprite;

        int x = (int)att.x_coord;
        int y = (int)att.y_coord;

        GameObject[] nearby = new GameObject[4];

        //put this into a separate method

        nearby[0] = tm_script.GetTile(x, y + 1); //top right
        nearby[1] = tm_script.GetTile(x, y + 1); //top left
        nearby[2] = tm_script.GetTile(x - 1, y); //bottom right
        nearby[3] = tm_script.GetTile(x, y - 1); //bottom left

        foreach (GameObject tile in nearby)
        {
            if (tile != null && tile.GetComponent<Node_Attributes>()._type == nodeType._wire)
            {
                if (!tile.GetComponent<Wire_Script>().current)
                {
                    tile.GetComponent<Wire_Script>().current = true;
                    tile.GetComponent<Wire_Script>().SendCurrent();
                }
            }
        }
    }
}
