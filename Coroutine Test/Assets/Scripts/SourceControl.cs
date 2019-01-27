using UnityEngine;
using System.Collections;

public class SourceControl : MonoBehaviour {

  public int maxSpheres;
  public GameObject[] objectsToSpawn;
  public int boomEvery;

  // Use this for initialization
	void Start () {
    StartCoroutine (Spawn ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  IEnumerator Spawn()
  {
    int boomCount = 0;

    for (int i = 0; i < maxSpheres; i++)
    {
      if (boomCount == boomEvery)
      {
        Vector3 boomPos = new Vector3 (0.0f, 0.5f, 0.0f);

        GameObject newSphere = Instantiate (objectsToSpawn [0], boomPos, Quaternion.identity) as GameObject;

        newSphere.GetComponent<Rigidbody> ().AddForce (new Vector3 (0.0f, 1000000000.0f, 0.0f));

        boomCount = 0;
      } 
      else
      {

        boomCount++;

        Vector3 newPosition = new Vector3 (transform.position.x + Random.Range (-1.5f, 1.5f),
                                           transform.position.y,
                                           transform.position.z + Random.Range (-1.5f, 1.5f));

        int objNumber = Random.Range (0, objectsToSpawn.GetLength (0));

        GameObject newSphere = Instantiate (objectsToSpawn [objNumber], newPosition, Quaternion.identity) as GameObject;

        newSphere.GetComponent<Rigidbody> ().AddTorque (new Vector3 (Random.Range (50.0f, 100.0f), 
                                                                     50.0f, 
                                                                     Random.Range (50.0f, 100.0f)));
      }

      yield return new WaitForSeconds (Random.Range (0.01f, 0.1f));
    }
  }
}
