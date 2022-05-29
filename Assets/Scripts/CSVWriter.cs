using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVWriter : MonoBehaviour
{
    public string fileName = "CSV";
    public string[] data;
    public UnityEngine.UI.InputField filenameInput;
    public int timePrinted = 0;
    void Start()
    {
        filenameInput.text = fileName; 
    }

    public void updateFilename() 
    {
        changeFileName(filenameInput.text);
    }

    public void changeFileName(string s) 
    {
        fileName = Application.persistentDataPath + "/" + s+ "(" +timePrinted+")" +".csv";
    }

    public void writeCSVFromData(List<SensorData> _data) 
    {
        for (int i = 0; i < _data.Count; i++) 
        {
            Vector3 r = _data[i].rotation;
            string input;
            float xValue = r.x;
            float yValue = r.y;
            float zValue = r.z;
            string timeValue = _data[i].time;
            input = xValue + "," + yValue + "," + zValue + "," + timeValue;
            WriteCSV(input);
        }
    }

    public void WriteCSV(string input)
    {
        TextWriter tw;
        if (!File.Exists(fileName))
        {
            tw = new StreamWriter(fileName, false);
            string header = data[0];
            for (int i = 1; i < data.Length; i++) 
            {
                header += ","+data[i];
            }
            tw.WriteLine(header);
            tw.Close();
        }
        writeCSVLine(input);
    }

    void writeCSVLine(string input) 
    {
        TextWriter tw;
        tw = new StreamWriter(fileName, true);
        tw.WriteLine(input);
        tw.Close();
    }
}
