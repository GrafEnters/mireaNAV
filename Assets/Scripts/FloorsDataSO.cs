using System.Collections.Generic;
using UnityEngine;

public class FloorsDataSO : ScriptableObject {
    public List<Nod> Nods;

    public FloorsDataSO() {
        Nods = new List<Nod>();
    }
}

[System.Serializable]
public class Nod {
    [SerializeField]
    public List<Vector3Int> Neighbours;

    public Vector3Int coordinates;
    public int type;
}