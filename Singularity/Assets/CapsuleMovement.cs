using UnityEngine;
using System.Collections;

public class CapsuleMovement : MonoBehaviour {

  public float Speed = 1.0f;  
  public float TurnSpeed = 1.0f;
  public float Gravity = 1.0f;

  private Rigidbody rb;

	// Use this for initialization
	void Start () {
    rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void FixedUpdate() {

    Vector3 localThrusterForce = new Vector3(0, Input.GetAxis("Vertical"), 0);
    Vector3 globalTrusterForce = transform.TransformDirection (localThrusterForce) * Speed;
    Vector3 globalGravity = Vector3.zero;
    Vector3 offsetFromCentre = Vector3.zero - transform.position;

    if (offsetFromCentre.magnitude > 1.0e-3)
      globalGravity = offsetFromCentre.normalized * Gravity;

    rb.AddForce (globalTrusterForce + globalGravity);
    rb.AddTorque (new Vector3(0, 0, Input.GetAxis ("Horizontal")) * Time.deltaTime * TurnSpeed);
  }
}
