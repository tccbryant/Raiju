using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Short_Script : MonoBehaviour {  //this serves to be a tile you DONT want to power. Doing so causes the entire flow to stop
   public bool current = false;
   
   
  public void RemoveCurrent()
    {
        GameObject tm = GameObject.FindGameObjectWithTag("tile_manager");
        Tile_Manager_Script tm_script = tm.GetComponent<Tile_Manager_Script>();
        Node_Attributes att = GetComponent<Node_Attributes>();

        int x = (int) att.x_coord;
        int y = (int) att.y_coord;

        GameObject[] nearby = new GameObject[4];

        //put this into a separate method
        
        nearby[0] = tm_script.GetTile(x, y + 1); //top right
        nearby[1] = tm_script.GetTile(x, y + 1); //top left
        nearby[2] = tm_script.GetTile(x - 1, y); //bottom right
        nearby[3] = tm_script.GetTile(x, y - 1); //bottom left

        foreach (GameObject tile in nearby)
        {
            if(tile != null && tile.GetComponent<Node_Attributes>()._type == nodeType._wire)
            {
                tile.GetComponent<Wire_Script>().current = false;
                tile.GetComponent<Wire_Script>().RemoveCurrent();
            }
        }
    } 
   

  public void defeat(){
  GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_020");
  Debug.Log("You hit a short");
  
  //DO defeat stuff
  }
}