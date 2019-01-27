using UnityEngine;
using System.Collections;

public class FruitStealer : MonoBehaviour {

  public InventoryManager InventoryManager;
  public string ItemName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag ("Player") == false)
      return;

    InventoryManager.RemoveItem (ItemName);
  }
}
