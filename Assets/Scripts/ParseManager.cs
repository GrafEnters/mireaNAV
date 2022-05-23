using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ParseManager : MonoBehaviour {
    private const string scriptableObjectPath = "Assets/Data/FloorsDataSO.asset";
    private const string genericFloorPath = "Assets/Data/Floor{0}.txt";
    private const int floorsCount = 3;
    private const int stairsKey = 2;

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

        Dictionary<Vector3, Nod> nods = ConvertToNods(floors);
        ConnectNods(nods);

        dataSo.Nods = new List<Nod>(nods.Values);

        Debug.Log($"Parsed Successful. Total Nods count: {dataSo.Nods.Count}.");
    }

    private static int[,] ParseFloor(string[] textData) {
        int lineLength = textData[0].Length;
        int[,] res = new int[lineLength, textData.Length];
        for (int i = 0; i < lineLength; i++) {
            for (int j = 0; j < textData.Length; j++) {
                res[i, j] = (int) char.GetNumericValue(textData[j][i]);
            }
        }

        return res;
    }

    private static Dictionary<Vector3, Nod> ConvertToNods(int[][,] floorsData) {
        int x = floorsData[0].GetLength(0);
        int z = floorsData[0].GetLength(1);
        int y = floorsData.Length;

        Dictionary<Vector3, Nod> nodsMap = new();

        for (int yy = 0; yy < y; yy++) {
            for (int xx = 0; xx < x; xx++) {
                for (int zz = 0; zz < z; zz++) {
                    Nod nod = new Nod {
                        type = floorsData[yy][xx, zz],
                        coordinates = new Vector3Int(xx, yy, zz),
                        Neighbours = new List<Vector3Int>()
                    };
                    nodsMap.Add(new Vector3(xx, yy, zz), nod);
                }
            }
        }

        return nodsMap;
    }

    private static void ConnectNods(Dictionary<Vector3, Nod> nods) {
        foreach (Nod nod in nods.Values) {
            TryConnectNode(nod, nods, Vector3Int.back);
            TryConnectNode(nod, nods, Vector3Int.forward);
            TryConnectNode(nod, nods, Vector3Int.right);
            TryConnectNode(nod, nods, Vector3Int.left);
            TryConnectStairsNode(nod, nods, Vector3Int.up);
            TryConnectStairsNode(nod, nods, Vector3Int.down);
        }
    }

    private static void TryConnectNode(Nod nod, Dictionary<Vector3, Nod> nods, Vector3Int shift) {
        if (nods.ContainsKey(nod.coordinates + shift)) {
            nod.Neighbours.Add(nod.coordinates + shift);
        }
    }

    private static void TryConnectStairsNode(Nod nod, Dictionary<Vector3, Nod> nods, Vector3Int shift) {
        if (nods.ContainsKey(nod.coordinates + shift)) {
            if (nods[nod.coordinates + shift].type == stairsKey) {
                nod.Neighbours.Add(nod.coordinates + shift);
            }
        }
    }
}