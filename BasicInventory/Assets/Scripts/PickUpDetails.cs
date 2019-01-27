using UnityEngine;
using System.Collections;

public class PickUpDetails : MonoBehaviour {

  public string ItemName;

  // Use this for initialization
	void Start () {
    // Automagically tag this object correctly
    gameObject.tag = "PickUp";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
