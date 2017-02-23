using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source_Script : MonoBehaviour {
   public bool current = false;
   public bool playingAudio = false;

   
   public void OnMouseDown(){
      
      GameObject am = GameObject.FindGameObjectWithTag("audio_manager");
      Audio_Script am_script = am.GetComponent<Audio_Script>();
      
      
      if(playingAudio == false){
      Debug.Log("Play");
         playingAudio = true;
         am_script.playAudio("Arrival-demo1",20.0,1);
      }else{
         playingAudio = false;
         stopAudio(1);
      }//playingAudio = true;am_script.playAudio("A_Puzzle_For_Raiju", 20.0,2);}
      current = !current;
      
      
      if(current){
      GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_022");
      }else{
      GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_021");
      }
      GetComponent<SpriteRenderer>().sortingOrder = 500;
      if(current){SendCurrent();}else{RemoveCurrent();}
   }//end OnMouseDown
   
   public void stopAudio(int track){
      playingAudio = false;
      GameObject am = GameObject.FindGameObjectWithTag("audio_manager");
      Audio_Script am_script = am.GetComponent<Audio_Script>();
      am_script.stopAudio(track);
   }
   
   //public void playAudio(params object[] list){
       
   //}
   //this method should be inherited
   public void SendCurrent(){
      current = true;
      GameObject tm = GameObject.FindGameObjectWithTag("tile_manager");
      Tile_Manager_Script tm_script = tm.GetComponent<Tile_Manager_Script>();
      Node_Attributes att = GetComponent<Node_Attributes>();
        
      int x = (int) att.x_coord;
      int y = (int) att.y_coord;

      GameObject[] nearby = new GameObject[4];
      //put this into a separate method
       
      nearby[0] = tm_script.GetTile(x, y + 1); //top right
      nearby[1] = tm_script.GetTile(x + 1, y); //top left
      nearby[2] = tm_script.GetTile(x - 1, y); //bottom right
      nearby[3] = tm_script.GetTile(x, y - 1); //bottom left

      foreach (GameObject tile in nearby){
         if(tile != null && tile.GetComponent<Node_Attributes>()._type == nodeType._wire){
            tile.GetComponent<Wire_Script>().current = true;
            tile.GetComponent<Wire_Script>().SendCurrent();
         }
      }
   }//end SendCurrent
   
   public void RemoveCurrent(){
   
      GameObject tm = GameObject.FindGameObjectWithTag("tile_manager");
      Tile_Manager_Script tm_script = tm.GetComponent<Tile_Manager_Script>();
      Node_Attributes att = GetComponent<Node_Attributes>();

      int x = (int) att.x_coord;
      int y = (int) att.y_coord;

      GameObject[] nearby = new GameObject[4];

        //put this into a separate method
        
      nearby[0] = tm_script.GetTile(x, y + 1); //top right
      nearby[1] = tm_script.GetTile(x + 1, y); //top left
      nearby[2] = tm_script.GetTile(x - 1, y); //bottom right
      nearby[3] = tm_script.GetTile(x, y - 1); //bottom left

      foreach (GameObject tile in nearby){
         if(tile != null && tile.GetComponent<Node_Attributes>()._type == nodeType._wire){
            tile.GetComponent<Wire_Script>().current = false;
            tile.GetComponent<Wire_Script>().RemoveCurrent();
         }
      }
   }//end RemoveCurrent
   
}//end Class Source_Script
