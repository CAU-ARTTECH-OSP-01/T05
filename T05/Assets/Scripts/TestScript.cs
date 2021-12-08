using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
        data = DataReader_TSV.ReadDataByTSV("Data/CardList");

        Debug.Log(data[0]["Name"]);
        Debug.Log(data[0]["Count"]);
        Debug.Log(data[0]["Rare"]);
        Debug.Log(data[0]["Component_0"]);
        Debug.Log(data[0]["Value_0"]);
    }

}
