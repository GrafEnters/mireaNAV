using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour {
    public static Queue<Nod> FindPathByAStar(List<Nod> nods,Dictionary<Vector3Int, Nod> NodsMap, Nod start, Nod finish) {
        Debug.Log("Path successfully found");
        return null;
    }

    public static List<Nod> FindPathByDeepSearch(List<Nod> nods,Dictionary<Vector3Int, Nod> NodsMap, Nod start, Nod finish) {
        
        List<Nod> visited = new();
        Stack<Nod> frontier = new();
      
        frontier.Push(start);
      
            
        while (frontier.Count > 0)
        {
            Nod current = frontier.Pop();
            visited.Add(current);
               
            if (current == finish) {
                Debug.Log("Path successfully found");
                return visited;
            }

            var neighbours = current.Neighbours;
            foreach(var neighbour in neighbours)
                if (!visited.Contains(NodsMap[neighbour]))
                    frontier.Push(NodsMap[neighbour]);
        }
        Debug.Log("Couldnt find path :(");
        return null;
    }
}