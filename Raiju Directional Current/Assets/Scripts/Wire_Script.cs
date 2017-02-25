using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//wire script and source script should be children of the same class
//need to implement this class so they can inherit the stuff.

public class Wire_Script : MonoBehaviour {
    public bool current = false;
    public bool connectTopL = false;
    public bool connectTopR = false;
    public bool connectBotL = false;
    public bool connectBotR = false;
    public bool straight = false;
    public string comment = "Rshift: When mouse over rotatable tile; straight <-> corner";
    public string comment2;
    //public bool canRotate = false
   //Sprite[] sprites = Resources.LoadAll <Sprite> ("tiles");  

   
	public void OnMouseDown(){
   if(!this.current){ //cannot rotate if current is true
		bool temp = connectTopL;
      connectTopL = connectTopR;
      connectTopR = connectBotR;
      connectBotR = connectBotL;
      connectBotL = temp;
      changeSprite();
      }
	}

    //horribly written code, needs to be cleaned and separated
    public void changeSprite(){
        GameObject tm = GameObject.FindGameObjectWithTag("tile_manager");
        Tile_Manager_Script tm_script = tm.GetComponent<Tile_Manager_Script>();
   if(this.current){
        if (connectTopL && connectBotL){  
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_071");
      }else if(connectTopL && connectBotR){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_035");
      } else if(connectTopR && connectBotL){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_023");
      }else if(connectTopR && connectBotR){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_083");  
      }else if(connectTopR && connectBotR && connectTopL && connectBotL){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_011");
      }else if(connectTopL && connectTopR){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_047");
      }else if(connectBotL && connectBotR){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_059");   
      }else{   
         GetComponent<SpriteRenderer>().sprite = tm_script.onSprite;  //gets the default specified onSprite();
      }
   }else{  //no current   
        if (connectTopL && connectBotL){  
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_062");
      }else if(connectTopL && connectBotR){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_026");
      }else if(connectTopR && connectBotL){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_014");
      }else if(connectTopR && connectBotR){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_074");  
      }else if(connectTopR && connectBotR && connectTopL && connectBotL){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_096");
      }else if(connectTopL && connectTopR){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_038");
      }else if(connectBotL && connectBotR){
         GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_050");   
      }else{   
         GetComponent<SpriteRenderer>().sprite = tm_script.offSprite;  //gets the default specified onSprite();
      }
    
    }
   }//end changeSprite
    
    
    
    public void SendCurrent(){
        GameObject tm = GameObject.FindGameObjectWithTag("tile_manager");
        Tile_Manager_Script tm_script = tm.GetComponent<Tile_Manager_Script>();
        Node_Attributes att = GetComponent<Node_Attributes>();
		  
        changeSprite();
        //TE: These check the parameters of the tile and change it's sprite accordingly. NOTE: this uses the individual sprites in the Resources folder NOT the ones in Sprites, hence why they are named without the "work" as to make an easy visible distinction. 
      
        int x = (int)att.x_coord;
        int y = (int)att.y_coord;

        GameObject[] nearby = new GameObject[4];

        //put this into a separate method

        nearby[0] = tm_script.GetTile(x, y + 1); //top right
        nearby[1] = tm_script.GetTile(x + 1, y); //top left
        nearby[2] = tm_script.GetTile(x - 1, y); //bottom right
        nearby[3] = tm_script.GetTile(x, y - 1); //bottom left
      
      
      
      
      if(nearby[0] != null && nearby[0].GetComponent<Node_Attributes>()._type == nodeType._wire){
      if(this.connectTopR == true && nearby[0].GetComponent<Wire_Script>().connectBotL == true && nearby[0].GetComponent<Wire_Script>().current == false){
         nearby[0].GetComponent<Wire_Script>().current = true;
         nearby[0].GetComponent<Wire_Script>().SendCurrent();
      }
      }
      
      if(nearby[1] != null && nearby[1].GetComponent<Node_Attributes>()._type == nodeType._wire){
      if(this.connectTopL == true && nearby[1].GetComponent<Wire_Script>().connectBotR == true && nearby[1].GetComponent<Wire_Script>().current == false){
         nearby[1].GetComponent<Wire_Script>().current = true;
         nearby[1].GetComponent<Wire_Script>().SendCurrent();
      }
      }
      
      if(nearby[2] != null && nearby[2].GetComponent<Node_Attributes>()._type == nodeType._wire){
      if(this.connectBotR == true && nearby[2].GetComponent<Wire_Script>().connectTopL == true && nearby[2].GetComponent<Wire_Script>().current == false){
         nearby[2].GetComponent<Wire_Script>().current = true;
         nearby[2].GetComponent<Wire_Script>().SendCurrent();
      }
      }
      
      if(nearby[3] != null && nearby[3].GetComponent<Node_Attributes>()._type == nodeType._wire){
         if(this.connectBotL == true && nearby[3].GetComponent<Wire_Script>().connectTopR == true && nearby[3].GetComponent<Wire_Script>().current == false){
            nearby[3].GetComponent<Wire_Script>().current = true;
            nearby[3].GetComponent<Wire_Script>().SendCurrent();
         }
      }
      
      //goal checker
      if(nearby[0] != null && nearby[0].GetComponent<Node_Attributes>()._type == nodeType._goal ){
      if(this.connectTopR == true){
         nearby[0].GetComponent<Goal_Script>().current = true;
         nearby[0].GetComponent<Goal_Script>().victory();
      }
      } 

      /*if(nearby[1] != null && nearby[1].GetComponent<Node_Attributes>()._type == nodeType._goal){
      if(this.connectTopL == true){
         nearby[1].GetComponent<Goal_Script>().current = true;
         nearby[1].GetComponent<Goal_Script>().victory();
      }
      } */
      if(nearby[2] != null && nearby[2].GetComponent<Node_Attributes>()._type == nodeType._goal){
      if(this.connectBotR == true){
         nearby[2].GetComponent<Goal_Script>().current = true;
         nearby[2].GetComponent<Goal_Script>().victory();
      }
      } 
      if(nearby[3] != null && nearby[3].GetComponent<Node_Attributes>()._type == nodeType._goal){
      if(this.connectBotL == true){
         nearby[3].GetComponent<Goal_Script>().current = true;
         nearby[3].GetComponent<Goal_Script>().victory();
      }
      } 
   }//end SendCurrent  
    
    
    
   public void RemoveCurrent(){
   
      GameObject tm = GameObject.FindGameObjectWithTag("tile_manager");
      Tile_Manager_Script tm_script = tm.GetComponent<Tile_Manager_Script>();
      Node_Attributes att = GetComponent<Node_Attributes>();
		  
      changeSprite();

      //TE: These check the parameters of the tile and change it's sprite accordingly. NOTE: this uses the individual sprites in the Resources folder NOT the ones in Sprites, hence why they are named without the "work" as to make an easy visible distinction. 
      
      int x = (int)att.x_coord;
      int y = (int)att.y_coord;

      GameObject[] nearby = new GameObject[5];
      //put this into a separate method

      nearby[0] = tm_script.GetTile(x, y + 1); //top right
      nearby[1] = tm_script.GetTile(x + 1, y); //top left
      nearby[2] = tm_script.GetTile(x - 1, y); //bottom right
      nearby[3] = tm_script.GetTile(x, y - 1); //bottom left
      
      if(nearby[0] != null && nearby[0].GetComponent<Node_Attributes>()._type == nodeType._wire){
         if(this.connectTopR == true && nearby[0].GetComponent<Wire_Script>().connectBotL == true && nearby[0].GetComponent<Wire_Script>().current == true){  
            nearby[0].GetComponent<Wire_Script>().current = false;
            nearby[0].GetComponent<Wire_Script>().RemoveCurrent();
         }
      }
      
      if(nearby[1] != null && nearby[1].GetComponent<Node_Attributes>()._type == nodeType._wire){
         if(this.connectTopL == true && nearby[1].GetComponent<Wire_Script>().connectBotR == true && nearby[1].GetComponent<Wire_Script>().current == true){
            nearby[1].GetComponent<Wire_Script>().current = false;
            nearby[1].GetComponent<Wire_Script>().RemoveCurrent();
         }
      }
      
      if(nearby[2] != null && nearby[2].GetComponent<Node_Attributes>()._type == nodeType._wire){
         if(this.connectBotR == true && nearby[2].GetComponent<Wire_Script>().connectTopL == true && nearby[2].GetComponent<Wire_Script>().current == true){
            nearby[2].GetComponent<Wire_Script>().current = false;
            nearby[2].GetComponent<Wire_Script>().RemoveCurrent();
         }
      }
      
      if(nearby[3] != null && nearby[3].GetComponent<Node_Attributes>()._type == nodeType._wire){
         if(this.connectBotL == true && nearby[3].GetComponent<Wire_Script>().connectTopR == true && nearby[3].GetComponent<Wire_Script>().current == true){
            nearby[3].GetComponent<Wire_Script>().current = false;
            nearby[3].GetComponent<Wire_Script>().RemoveCurrent();
         }
      }
      
      foreach(GameObject tile in nearby){
         if (tile != null && tile.GetComponent<Node_Attributes>()._type == nodeType._goal){
            if(tile.GetComponent<Goal_Script>().current){
            tile.GetComponent<Goal_Script>().current = false;
            tile.GetComponent<Goal_Script>().defeat();
            }
         }
         
      }
   
   }//end RemoveCurrent
   
}//end CLASS
