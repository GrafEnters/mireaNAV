using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private RenderManager _renderManager;

    [SerializeField]
    private FloorsDataSO dataSo;

    [SerializeField]
    private TMP_Dropdown algoritmDropdown;
    
    private Algorithm _curAlgorithm;

    private void Awake() {
        _curAlgorithm = Algorithm.DeepSearch;
        FillDropdown();
    }

    private void FillDropdown() {
        List<string> options = new();
 
        options.Add(Algorithm.DeepSearch.ToString());
        options.Add(Algorithm.WidthSearch.ToString());
        options.Add(Algorithm.AStar.ToString());
        algoritmDropdown.ClearOptions();
        algoritmDropdown.AddOptions(options);
    }

    public void SelectAlgorithm(int value) {
        _curAlgorithm = (Algorithm)value;
    }

    public void FindPath() {
        if (_renderManager.TryGetPoints(out Nod start, out Nod finish)) {
            Stack<Nod> path = AStarManager.FindPathByDeepSearch(dataSo.Nods,dataSo.GetNodsMap(), start, finish);
            switch (_curAlgorithm) {
                case Algorithm.DeepSearch:
                    path = AStarManager.FindPathByDeepSearch(dataSo.Nods,dataSo.GetNodsMap(), start, finish);
                    break;
                case Algorithm.WidthSearch:
                    path = AStarManager.FindPathByWidthSearch(dataSo.Nods,dataSo.GetNodsMap(), start, finish);
                    break;
                case Algorithm.AStar:
                    path = AStarManager.FindPathByAStar(dataSo.Nods,dataSo.GetNodsMap(), start, finish);
                    break;
            }
            _renderManager.SelectPath(path);
        } else {
            Debug.Log("Choose start & finish points!");
        }
    }
}

public enum Algorithm {
    DeepSearch,
    WidthSearch,
    //IterativeSearch,
    AStar
}