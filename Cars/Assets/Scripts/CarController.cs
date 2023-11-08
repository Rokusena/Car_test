using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rigthWheel;
    public bool motor;
    public bool stearing;
}

     

public class CarController : MonoBehaviour
{
    void ApplyLocalPositionToVisuals(WheelCollider colider)
    {
        if (colider.transform.childCount == 0) return;

        Transform visualWheel = colider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        colider.GetWorldPose(out position, out rotation);

        visualWheel.position = position;
        visualWheel.rotation = rotation;


        }
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxstearingAngle;


    private void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxstearingAngle * Input.GetAxis("Horizontal");

        foreach(var axle in axleInfos)
        {
            if(axle.stearing)
            {
                axle.leftWheel.steerAngle = steering;
                axle.rigthWheel.steerAngle = steering;
            }
            if(axle.motor)
            {
                axle.leftWheel.motorTorque = motor;
                axle.rigthWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axle.leftWheel);
            ApplyLocalPositionToVisuals(axle.rigthWheel);
        }
    }

    void Update()
    {
        
    }
}
