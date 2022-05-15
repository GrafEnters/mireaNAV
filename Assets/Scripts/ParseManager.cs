using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ParseManager : MonoBehaviour {
    private const string scriptableObjectPath = "Assets/Data/FloorsDataSO.asset";
    private const string firstFloorPath = "Assets/Data/Floor1.txt";

    [MenuItem("MireaNAV/ParseFloors")]
    public static void ParseFloors() {
        FloorsDataSO dataSo = ScriptableObject.CreateInstance<FloorsDataSO>();

        // Наполни dataSO своими данными

        AssetDatabase.CreateAsset(dataSo, scriptableObjectPath);
        Debug.Log($"Parsed Successful. Total nods count: {dataSo.nods.Count}.");
    }
}