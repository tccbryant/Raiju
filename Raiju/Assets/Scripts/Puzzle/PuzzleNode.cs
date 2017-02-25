using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNode : MonoBehaviour {
    //holds the io state of the puzzle tile
    public bool current;

    //hold properties that need to be referenced
    private NodeAttributes att;
    private SpriteRenderer sr;
    private TileManagerScript tm;

    //holds the nearby nodes
    private GameObject[] nearby = new GameObject[4];

    public void Start()
    {
        //initialize properties
        att = GetComponent<NodeAttributes>();
        sr = GetComponent<SpriteRenderer>();
        tm = GameObject.FindGameObjectWithTag("tile_manager").GetComponent<TileManagerScript>();
    }

    public void Current()
    {
        current = true;
        sr.sprite = tm.onSprite;
        GetNearbyTiles();
        foreach (GameObject tile in nearby)
        {
            //check to see if the tile is null
            bool valid = (tile != null); 
            //check to see if the node is a wire (should change this to general puzzle piece)
            valid = valid && tile.GetComponent<NodeAttributes>()._type == nodeType._wire;
            //check to see if the current is on
            valid = valid && !tile.GetComponent<PuzzleNode>().current;
            if (valid)
            {
                //sends the current to the next node
                StartCoroutine(SendCurrent(tile));
            }
        }
    }

    public void GetNearbyTiles()
    {
        //retrieve the x and y coordinates
        int x = (int)att.x_coord;
        int y = (int)att.y_coord;

        //fill the nearby array
        nearby[0] = tm.GetTile(x, y + 1); //top right
        nearby[1] = tm.GetTile(x + 1, y); //top left
        nearby[2] = tm.GetTile(x, y - 1); //bottom left
        nearby[3] = tm.GetTile(x - 1, y); //bottom right
    }

    public IEnumerator SendCurrent(GameObject node)
    {
        yield return new WaitForSeconds(.05f);
        node.GetComponent<PuzzleNode>().Current();
    }	
}
