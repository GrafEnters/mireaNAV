using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class ParseManager : MonoBehaviour {
    private const string scriptableObjectPath = "Assets/Data/FloorsDataSO.asset";
    private const string genericFloorPath = "Assets/Data/Floor{0}.txt";
    private const int floorsCount = 3;

    [MenuItem("MireaNAV/ParseFloors")]
    public static void ParseFloors() {
        FloorsDataSO dataSo = ScriptableObject.CreateInstance<FloorsDataSO>();

        int[][,] floors = new int[floorsCount][,];
        for (int i = 0; i < floorsCount; i++) {
            string fullPath = string.Format(genericFloorPath, i + 1);
            string[] textData = File.ReadAllLines(fullPath);
            floors[i] = ParseFloor(textData);
        }
        // Наполни dataSO своими данными

        AssetDatabase.CreateAsset(dataSo, scriptableObjectPath);
        Debug.Log($"Parsed Successful. Total nods count: {dataSo.nods.Count}.");
    }

    private static int[,] ParseFloor(string[] textData) {
        int lineLength = textData[0].Length;
        int[,] res = new int[lineLength, textData.Length];
        for (int i = 0; i < lineLength; i++) {
            for (int j = 0; j < textData.Length; j++) {
                res[i, j] = textData[j][i];
            }
        }

        return res;
    }
    
}