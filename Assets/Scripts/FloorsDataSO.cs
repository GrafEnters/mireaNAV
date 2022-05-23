using System.Collections.Generic;
using UnityEngine;

public class FloorsDataSO : ScriptableObject {
    [SerializeField]
    public List<Nod> nods;

    public FloorsDataSO() {
        nods = new List<Nod>();
    }
}

[System.Serializable]
public class Nod {
    [SerializeField]
    public List<Nod> Neighbours;
    public Vector3 coordinates;
    public int type;
}