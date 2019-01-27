using UnityEngine;
using System.Collections;

public class FInishDetector : MonoBehaviour {

  public AmazingRacerControl controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter(Collider other)
  {
    controller.EndGame ();
  }
}
