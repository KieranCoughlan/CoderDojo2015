using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
  public GameObject cameraPos;
  public float maxRotate;

  void LateUpdate()
  {
    Quaternion targetRotation = cameraPos.transform.rotation;
    Quaternion currentRotation = transform.rotation;

    transform.position = cameraPos.transform.position;
    transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, maxRotate);
  }
}
