using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCarController : MonoBehaviour
{
  public List<AxleInfo> axelInfos;
  public float maxMotorTorque;
  public float maxSteeringAngle;

  public void ApplyLocalPositionToVisuals(WheelCollider collider)
  {
    if (collider.transform.childCount == 0)
      return;

    Transform visualWheel = collider.transform.GetChild(0);

    Vector3 position;
    Quaternion rotation;
    
    collider.GetWorldPose(out position, out rotation);

    visualWheel.transform.position = position;
    visualWheel.transform.rotation = rotation;
  }

  void FixedUpdate()
  {
    float motor = maxMotorTorque * Input.GetAxis("Vertical");
    float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

    foreach (AxleInfo axelInfo in axelInfos)
    {
      if (axelInfo.steering)
      {
        axelInfo.leftWheel.steerAngle = steering;
        axelInfo.rightWheel.steerAngle = steering;
      }

      if (axelInfo.motor)
      {
        axelInfo.leftWheel.motorTorque = motor;
        axelInfo.rightWheel.motorTorque = motor;
      }

      ApplyLocalPositionToVisuals(axelInfo.leftWheel);
      ApplyLocalPositionToVisuals(axelInfo.rightWheel);
    }
  }

  [System.Serializable]
  public class AxleInfo
  {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
  }

}
