using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour {

	public AmazingRacerControl controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider other){
		controller.ReturnPlayerToSpawn ();
	}
}
