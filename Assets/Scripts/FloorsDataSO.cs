using System.Collections.Generic;
using UnityEngine;

public class FloorsDataSO : ScriptableObject {
    public List<Nod> nods;

    public FloorsDataSO() {
        nods = new List<Nod>();
    }
}

[System.Serializable]
public struct Nod {
    public List<Nod> Neighbours;
    public int type;
}