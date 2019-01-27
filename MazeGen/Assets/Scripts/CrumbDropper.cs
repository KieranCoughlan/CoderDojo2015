using UnityEngine;
using System.Collections;

public class CrumbDropper : MonoBehaviour {

  public GameObject breadcrumbPrefab;
  public float breadcrumbDistance = 1.0f;

  private Vector3 lastCrumbPos;

	// Use this for initialization
	void Start () {
    lastCrumbPos = transform.position;
    StartCoroutine (DropCrumb ());
	}
	
  IEnumerator DropCrumb()
  {
    while (true)
    {
      if (Vector3.Distance (lastCrumbPos, transform.position) > breadcrumbDistance)
      {
        lastCrumbPos = transform.position;

        Vector3 pos = new Vector3 (transform.position.x, 0.07f, transform.position.z);
        Instantiate (breadcrumbPrefab, pos, Quaternion.identity);
      }

      yield return new WaitForSeconds(0.1f);
    }
  }
}
