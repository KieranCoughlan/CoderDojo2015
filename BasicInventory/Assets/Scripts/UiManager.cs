using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

  public InventoryManager InventoryManager;

  public Text BananaCount;
  public Text AppleCount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnGUI()
  {
    BananaCount.text = "Bananas: " + InventoryManager.GetCount ("Banana");
    AppleCount.text = "Apple: " + InventoryManager.GetCount ("Apple");
  }
}
