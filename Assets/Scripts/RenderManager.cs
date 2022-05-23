using UnityEngine;

public class RenderManager : MonoBehaviour {
    [SerializeField]
    private FloorsDataSO dataSo;

    [SerializeField]
    private NodObject nodPrefab;
    
    [SerializeField]
    private Transform nodsContainer;
    
    private void Start() {
        DrawNods();
    }

    private void DrawNods() {
        foreach (Nod nod in dataSo.Nods) {
            NodObject nodObj =  Instantiate(nodPrefab, nod.coordinates, Quaternion.identity, nodsContainer);
            nodObj.SetState(nod.type);
        }
    }
    
}
