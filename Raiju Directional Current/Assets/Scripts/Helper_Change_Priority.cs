﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_Change_Priority : MonoBehaviour {
   public int priority = 50;
   public int orig; 
   public bool swap;
   
   public void OnMouseDown(){
      GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_021");
      GetComponent<SpriteRenderer>().sortingOrder = 500;
   //   if(current){SendCurrent();}else{RemoveCurrent();}
   }//end OnMouseDown
}   