using UnityEngine;

public class UIManager : MonoBehaviour {
    public void FindPath() {
        AStarManager.FindPath(new Nod(), new Nod());
    }
}