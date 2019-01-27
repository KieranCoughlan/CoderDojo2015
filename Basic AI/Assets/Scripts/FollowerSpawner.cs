using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class FollowerSpawner : MonoBehaviour {

  public Transform Target;
  public GameObject FollowerPrefab;

	// Use this for initialization
	void Start () {
    StartCoroutine (SpawnFollowers ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  IEnumerator SpawnFollowers()
  {
    while (true)
    {
      GameObject newGO = (GameObject)Instantiate (FollowerPrefab);
      newGO.GetComponent<AICharacterControl> ().target = Target;

      yield return new WaitForSeconds (1.5f);
    }
  }
}
