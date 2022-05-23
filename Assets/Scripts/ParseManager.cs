using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ParseManager : MonoBehaviour {
    private const string scriptableObjectPath = "Assets/Data/FloorsDataSO.asset";
    private const string genericFloorPath = "Assets/Data/Floor{0}.txt";
    private const int floorsCount = 3;

    [MenuItem("MireaNAV/ParseFloors")]
    public static void ParseFloors() {
        FloorsDataSO dataSo = AssetDatabase.LoadAssetAtPath<FloorsDataSO>(scriptableObjectPath);
        if (dataSo == null) {
            dataSo = ScriptableObject.CreateInstance<FloorsDataSO>();
            AssetDatabase.CreateAsset(dataSo, scriptableObjectPath);
        }
       

        int[][,] floors = new int[floorsCount][,];
        for (int i = 0; i < floorsCount; i++) {
            string fullPath = string.Format(genericFloorPath, i + 1);
            string[] textData = File.ReadAllLines(fullPath);
            floors[i] = ParseFloor(textData);
        }

        List<Nod> nods = ConvertToNods(floors);
        
        
        // Наполни dataSO своими данными

        dataSo.nods = nods;

        Debug.Log($"Parsed Successful. Total nods count: {dataSo.nods.Count}.");
    }

    private static int[,] ParseFloor(string[] textData) {
        int lineLength = textData[0].Length;
        int[,] res = new int[lineLength, textData.Length];
        for (int i = 0; i < lineLength; i++) {
            for (int j = 0; j < textData.Length; j++) {
                res[i, j] = (int)char.GetNumericValue( textData[j][i]);
            }
        }

        return res;
    }

    private static List<Nod> ConvertToNods(int[][,] floorsData) {
        int x = floorsData[0].GetLength(0);
        int z = floorsData[0].GetLength(1);
        int y = floorsData.Length;

        List<Nod> nods = new();

        for (int yy = 0; yy < y; yy++) {
            for (int xx = 0; xx < x; xx++) {
                for (int zz = 0; zz < z; zz++) {
                    nods.Add(new Nod {
                        type = floorsData[yy][xx, zz],
                        coordinates = new Vector3(xx, yy, zz),
                        Neighbours = new List<Nod>()
                    });
                }
            }
        }

        return nods;
    }
    
    
    
}