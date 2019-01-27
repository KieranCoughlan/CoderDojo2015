using UnityEngine;
using System.Collections;

public class ExitController : MonoBehaviour {

  private AudioSource audioSource;

	// Use this for initialization
	void Start () {
    audioSource = GetComponent<AudioSource> ();
	}
	
  void OnTriggerEnter(Collider other)
  {
    audioSource.Play ();
  }
}
