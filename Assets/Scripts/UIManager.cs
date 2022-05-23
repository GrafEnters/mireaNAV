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
        List<string> options = new() {
            Algorithm.DeepSearch.ToString(),
            Algorithm.WidthSearch.ToString(),
            Algorithm.AStar.ToString()
        };

        algoritmDropdown.ClearOptions();
        algoritmDropdown.AddOptions(options);
    }

    public void SelectAlgorithm(int value) {
        _curAlgorithm = (Algorithm) value;
    }

    public void FindPath() {
        if (_renderManager.TryGetPoints(out Nod start, out Nod finish)) {
            Stack<Nod> path = null;
            switch (_curAlgorithm) {
                case Algorithm.DeepSearch:
                    path = AStarManager.FindPathByDeepSearch(dataSo.GetNodsMap(), start, finish);
                    break;

                case Algorithm.WidthSearch:
                    path = AStarManager.FindPathByWidthSearch(dataSo.GetNodsMap(), start, finish);
                    break;

                case Algorithm.AStar:
                    path = AStarManager.FindPathByAStar(dataSo.GetNodsMap(), start, finish);
                    break;
            }

            _renderManager.SelectPath(path);
            Debug.Log($"Path found in {AStarManager.CheckCount} checks and {AStarManager.MillisecondsPast} milliseconds!");
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