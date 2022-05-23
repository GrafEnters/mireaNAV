using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodObject : MonoBehaviour {
    [SerializeField]
    private MeshRenderer _meshRenderer;

    public void SetState(int stateIndex) {
        States state = (States) stateIndex;
        _meshRenderer.enabled = state != States.Invisible;
        switch (state) {
            case States.Empty:
                _meshRenderer.material.color = Color.white;
                break;

            case States.Wall:
                _meshRenderer.material.color = Color.black;
                break;

            case States.Stairs:
                _meshRenderer.material.color = Color.grey;
                break;

            case States.Selected:
                _meshRenderer.material.color = Color.yellow;
                break;

            case States.Path:
                _meshRenderer.material.color = Color.red;
                break;
        }
    }
}

public enum States {
    Invisible = -1,
    Empty,
    Wall,
    Stairs,
    Selected,
    Path
}