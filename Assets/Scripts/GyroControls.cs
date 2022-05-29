using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
//https://www.youtube.com/watch?v=P5JxTfCAOXo&ab_channel=N3KEN
public class GyroControls : MonoBehaviour
{
    UnityEngine.InputSystem.Gyroscope gyro;
    SensorControlls sensorControlls;

    private void Awake()
    {
        sensorControlls = new SensorControlls();
    }
    private void Start()
    {
        InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
        sensorControlls.Default.Enable();
    }

    public bool isEnable()
    {
        return UnityEngine.InputSystem.Gyroscope.current.enabled;
    }

    public Vector3 readValue() 
    {
        return sensorControlls.Default.Gyro.ReadValue<Vector3>();
    }

}
