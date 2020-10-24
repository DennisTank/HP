using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DB_Script : MonoBehaviour
{
    public Text h, l;

    [HideInInspector]public DataStorage DB;
    private void Awake() {
        DB = new DataStorage();
        DB.LoadFromJson();
        h.text = DB.allData.numHintPressed.ToString();
        l.text = DB.allData.numLetterPressed.ToString();
    }
    public void Save() {
        h.text = DB.allData.numHintPressed.ToString();
        l.text = DB.allData.numLetterPressed.ToString();
        DB.SaveToJson();
    }
    private void OnApplicationPause(bool pause)
    {
        DB.SaveToJson();
    }
    
}
