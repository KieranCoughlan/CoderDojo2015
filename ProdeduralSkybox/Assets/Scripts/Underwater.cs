using UnityEngine;
using System.Collections;

public class Underwater : MonoBehaviour {


  public Color UnderwaterFogColour;
  public float UnderWaterFogDensity;

  private Color StandardFogColour;
  private float StandardFogDensity;
  private bool StandardFogEnabled;

  private MeshRenderer MeshRenderer;

	// Use this for initialization
	void Start () {
    StandardFogEnabled = RenderSettings.fog;
    StandardFogColour = RenderSettings.fogColor;
    StandardFogDensity = RenderSettings.fogDensity;

    MeshRenderer = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag ("Player") == false)
      return;

    MeshRenderer.enabled = true;

    RenderSettings.fog = true;
    RenderSettings.fogColor = UnderwaterFogColour;
    RenderSettings.fogDensity = UnderWaterFogDensity;
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.CompareTag ("Player") == false)
      return;

    MeshRenderer.enabled = false;

    RenderSettings.fog = StandardFogEnabled;
    RenderSettings.fogColor = StandardFogColour;
    RenderSettings.fogDensity = StandardFogDensity;
  }
}
