﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Script : MonoBehaviour {
   public bool doorOpen = false;
   public bool isExit = false;  //holds whether or not the door leads to the next level or is just to open a new path

  public void openDoor(){
  GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_020");
  this.doorOpen = true;
  
  //Door stuff
  }
  public void closeDoor(){
  
  }
}