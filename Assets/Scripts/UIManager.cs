using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private RenderManager _renderManager;

    [SerializeField]
    private FloorsDataSO dataSo;
    
    public void FindPath() {
        if (_renderManager.TryGetPoints(out Nod start, out Nod finish)) {
            List<Nod> path = AStarManager.FindPathByDeepSearch(dataSo.Nods,dataSo.GetNodsMap(), start, finish);
            _renderManager.SelectPath(path);
        } else {
            Debug.Log("Choose start & finish points!");
        }
    }
}