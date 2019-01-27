using UnityEngine;
using System.Collections;

public class HandlePickups : MonoBehaviour {

  public InventoryManager InventoryManager;

  // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag ("PickUp") == false)
      return;

    PickUpDetails pud = other.gameObject.GetComponent<PickUpDetails> ();

    InventoryManager.AddItem (pud.ItemName);
    Destroy (other.gameObject);
  }
}
