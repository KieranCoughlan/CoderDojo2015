using UnityEngine;
using System.Collections;

public class SunColourControl : MonoBehaviour {

  public Gradient SunColour;

  private Light theLight;

  // Use this for initialization
	void Start () {
    theLight = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
    float verticalness = Mathf.Abs (Vector3.Dot (Vector3.up, transform.forward));
    Color newColour = SunColour.Evaluate (verticalness);
    theLight.color = newColour;
	}
}
