using System;
using Unity.VisualScripting;
using UnityEngine;

public class RenderManager : MonoBehaviour {
    [SerializeField]
    private FloorsDataSO dataSo;

    [SerializeField]
    private NodObject nodPrefab;

    [SerializeField]
    private Transform nodsContainer;

    private NodObject start, finish;

    public bool TryGetPoints(out Vector3 _start, out Vector3 _finish) {
        if (start == null || finish == null) {
            _start = Vector3.zero;
            _finish = Vector3.zero;
            return false;
        }

        _start = start.transform.position;
        _finish = finish.transform.position;
        return true;
    }

    private void Start() {
        DrawNods();
    }

    private void DrawNods() {
        foreach (Nod nod in dataSo.Nods) {
            NodObject nodObj = Instantiate(nodPrefab, nod.coordinates, Quaternion.identity, nodsContainer);
            nodObj.Init(nod.type);
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            TrySelectStart();
        }

        if (Input.GetMouseButtonDown(1)) {
            TrySelectFinish();
        }
    }

    private void TrySelectStart() {
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100)) {
            return;
        }

        NodObject obj = hitInfo.transform.gameObject.GetComponent<NodObject>();
        if (obj.curState == States.Wall) {
            Debug.Log("Cant select wall!");
            return;
        }

        if (obj == finish) {
            Debug.Log("Choose different nods!");
            return;
        }

        if (start != null) {
            start.RevertToNormal();
        }

        start = obj;
        obj.SetState(States.Selected);
    }

    private void TrySelectFinish() {
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100)) {
            return;
        }

        NodObject obj = hitInfo.transform.gameObject.GetComponent<NodObject>();
        if (obj.curState == States.Wall) {
            Debug.Log("Cant select wall!");
            return;
        }

        if (obj == start) {
            Debug.Log("Choose different nods!");
            return;
        }

        if (finish != null) {
            finish.RevertToNormal();
        }

        finish = obj;
        obj.SetState(States.Selected);
    }
}