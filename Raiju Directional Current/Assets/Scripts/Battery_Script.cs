﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery_Script : MonoBehaviour { //continues to provide power after it loses current 
   public bool current = false;
   public int charge = 0; //How powered on is the battery?
   public bool connectTopL = false;
   public bool connectTopR = false;
   public bool connectBotL = false;
   public bool connectBotR = false;
  
   public void Update(){
      changeSprite();
      if(charge>0 && current == false){ // recall that a tile's current will only be false once the removeCurrent of the nearby tile is called. That meaning that this will not occur until the circuit isnt being powered by anything else. This is the basis of how the battery "turns on" after the original power has been turned off 
         charge -= 1;
         if(charge%15 == 0)SendCurrent(); //check every 1/2 second frame (30fps) or every 1/4 second (60fps)  
      }else if(charge == 0){  // this will only occur once and serves to send the removeCurrent command to power off the cicuit. As opposed to the above if, this is supposed to run only once.
         charge = -1;
         RemoveCurrent();
      }
   }
   
   public void powerUp(){// just serves to power on this tile
   charge = 300;
   
   }
   
   public void changeSprite(){
      if(charge == 50){GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_107");
      }else if(charge == 25){GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_106");
      }else if(charge < 1){GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_105");
      }
   }
   
   public void SendCurrent(){
    
        GameObject tm = GameObject.FindGameObjectWithTag("tile_manager");
        Tile_Manager_Script tm_script = tm.GetComponent<Tile_Manager_Script>();
        Node_Attributes att = GetComponent<Node_Attributes>();
		  
       
        
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

      if(nearby[1] != null && nearby[1].GetComponent<Node_Attributes>()._type == nodeType._goal){
      if(this.connectTopL == true){
         nearby[1].GetComponent<Goal_Script>().current = true;
         nearby[1].GetComponent<Goal_Script>().victory();
      }
      } 
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
      
      if(nearby[2] != null){
      if(this.connectBotR == true && nearby[2].GetComponent<Wire_Script>().connectTopL == true && nearby[2].GetComponent<Wire_Script>().current == true){
         if(nearby[2].GetComponent<Node_Attributes>()._type == nodeType._wire){
            nearby[2].GetComponent<Wire_Script>().current = false;
            nearby[2].GetComponent<Wire_Script>().RemoveCurrent();
         }
      }
      }
      
      if(nearby[3] != null){
      if(this.connectBotL == true && nearby[3].GetComponent<Wire_Script>().connectTopR == true && nearby[3].GetComponent<Wire_Script>().current == true){
         if(nearby[3].GetComponent<Node_Attributes>()._type == nodeType._wire){
            nearby[3].GetComponent<Wire_Script>().current = false;
            nearby[3].GetComponent<Wire_Script>().RemoveCurrent();
         }
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
   
  }

