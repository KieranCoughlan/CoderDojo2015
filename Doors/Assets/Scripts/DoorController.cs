using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

  private Animator animator;

	// Use this for initialization
	void Start () {
    animator = GetComponentInChildren<Animator> ();
	}
	
  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag ("Player") == false)
      return;

    Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint(other.transform.position);

    if (localPos.z < 0)
      animator.SetTrigger ("DoOpenIn");
    else
      animator.SetTrigger ("DoOpenOut");
      
  }

  void OnTriggerExit(Collider other)
  {
    animator.SetTrigger ("DoClose");
  }
}
