﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Script : MonoBehaviour {
   public bool current = false;
   

  public void victory(){
  GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_020");
  Debug.Log("VICTORY!!!");
  //DO VICTORIOUS THINGS
  }
  
  public void defeat(){
  GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("tiles/tile_019");
  Debug.Log("Power Down");
  //DO DEFEATIST THINGS
  }
}