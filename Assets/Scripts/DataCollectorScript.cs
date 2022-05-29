using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCollectorScript : MonoBehaviour
{

    [SerializeField]
    GyroControls gyroControls;
    public float endTime = 1;
    public float timer = 2;
    public CSVWriter _csvWriter;
    [SerializeField]
    string buttonTextWhenCollection = "Stop Collection";

    [SerializeField]
    string buttonTextNoCollection = "Collect data";

    [SerializeField]
    Text buttonText;

    public InputField inputField;
    public bool collectingData = false;
    public List<SensorData> dataCollected = new List<SensorData>();
    void Start()
    {
        inputField.text = endTime.ToString();
        Debug.Log(inputField.name);
        if (endTime <= 0) 
        {
            endTime += 1f;
        }
    }

    public void changeTime(string time) 
    {
        Debug.Log("time " + time);
        bool tryParse = float.TryParse(time, out timer);
        if (tryParse) 
        {
            endTime = float.Parse(time);
            
        }
    }

    void Update()
    {
        if (collectingData  && timer < endTime)
        {
            timer += Time.deltaTime;
            SensorData data = new SensorData(gyroControls.readValue());
            dataCollected.Add(data);
        }
        else if(collectingData && timer >= endTime) 
        {
            reset();
        }
    }

    void reset()
    {
        _csvWriter.writeCSVFromData(dataCollected);
        _csvWriter.timePrinted += 1;
        _csvWriter.updateFilename();
        buttonText.text = buttonTextNoCollection;
        dataCollected.Clear();
        collectingData = false;
    }

    public void startCollection() 
    {
        collectingData = !collectingData;

        if (collectingData)
        {
            buttonText.text = buttonTextWhenCollection;
            timer = 0;
        }
        else {
            reset();
        }
    }
}
[Serializable]
public class SensorData 
{
    public Vector3 rotation;
    public string time;

    public SensorData(Vector3 rotation) 
    {
        this.rotation = rotation;
        long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        time = DateTime.Now.ToString() + "g:" + milliseconds.ToString();
    }
}
