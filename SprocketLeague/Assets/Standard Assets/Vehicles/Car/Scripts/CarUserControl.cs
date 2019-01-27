using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
  [RequireComponent (typeof(CarController))]
  public class CarUserControl : MonoBehaviour
  {
    private CarController m_Car;
    // the car controller we want to use

    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";
    public string JumpAxis = "Jump";

    private void Awake ()
    {
      // get the car controller
      m_Car = GetComponent<CarController> ();
    }


    private void FixedUpdate ()
    {
      // pass the input to the car!
      float h = CrossPlatformInputManager.GetAxis (HorizontalAxis);
      float v = CrossPlatformInputManager.GetAxis (VerticalAxis);
#if !MOBILE_INPUT
      float handbrake = CrossPlatformInputManager.GetAxis (JumpAxis);
      m_Car.Move (h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
    }
  }
}
