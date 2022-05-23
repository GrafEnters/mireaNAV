using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private RenderManager _renderManager;

    public void FindPath() {
        if (_renderManager.TryGetPoints(out Vector3 start, out Vector3 finish)) {
            AStarManager.FindPath(start, finish);
        } else {
            Debug.Log("Choose start & finish points!");
        }
    }
}