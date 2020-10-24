using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class DataStorage 
{
    // making a required data structure
    public Data allData;
    string path;
    public DataStorage() {
        allData = new Data();
        path = Path.Combine(Application.persistentDataPath,"data.json");
    }
    //Loading the data if Exist or creating one
    public void LoadFromJson() {
        if (File.Exists(path))
        {
            string savaData = File.ReadAllText(path);
            allData = JsonUtility.FromJson<Data>(savaData);
        }
        else {
            allData.numHintPressed = 0;
            allData.numLetterPressed = 0;
            allData.letters = "";
        }
    }
    // saving the data to json 
    public void SaveToJson() {
        string saveData = JsonUtility.ToJson(allData);
        File.WriteAllText(path, saveData);
    }
}
// data structure
[Serializable]
public class Data {
    public int numHintPressed;
    public int numLetterPressed;
    public string letters;
}
