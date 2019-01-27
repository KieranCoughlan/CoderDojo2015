using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {

  private Dictionary<string, int> _inventory = new Dictionary<string, int>();

  // Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void AddItem(string name)
  {
    if (_inventory.ContainsKey (name))
      _inventory [name]++;
    else
      _inventory.Add (name, 1);
  }

  public bool RemoveItem(string name)
  {
    if (_inventory.ContainsKey (name) && _inventory [name] > 0)
    {
      _inventory [name]--;
      return true;
    }

    return false;
  }

  public int GetCount(string name)
  {
    if (_inventory.ContainsKey (name))
      return _inventory [name];
  

    return 0;
  }
}
