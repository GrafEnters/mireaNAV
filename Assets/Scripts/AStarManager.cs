using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarManager : MonoBehaviour {
    public static Queue<Nod> FindPathByAStar(List<Nod> nods,Dictionary<Vector3Int, Nod> NodsMap, Nod start, Nod finish) {
        Debug.Log("Path successfully found");
        return null;
    }

    public static Stack<Nod> FindPathByDeepSearch(List<Nod> nods,Dictionary<Vector3Int, Nod> NodsMap, Nod start, Nod finish) {
        
        List<Nod> visited = new();
        Stack<Nod> frontier = new();
        Stack<Nod> path = new();
        frontier.Push(start);
        path.Push(start);
        
        bool RecursivePushNod(Nod next) {
            path.Push(next);
            visited.Add(next);
            if(next == finish) {
                return true;
            }

            next.Neighbours = next.Neighbours.OrderBy(a => Random.Range(0, 1f)).ToList();
            foreach (var neighbour in next.Neighbours) {
                if (!visited.Contains(NodsMap[neighbour]))
                    if (RecursivePushNod(NodsMap[neighbour])) {
                        return true;
                    }
            }
            
            
            path.Pop();
            return false;
        }
        
        if (RecursivePushNod(start)) {
            Debug.Log("Path successfully found");
        } else {
            Debug.Log("Couldnt find path :(");
        }

        return path;
        /*while (frontier.Count > 0)
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
        
        foreach(var neighbour in neighbours) {
            if (!visited.Contains(NodsMap[neighbour]))
                frontier.Push(NodsMap[neighbour]);
        }
        }*/

      
        
        Debug.Log("Couldnt find path :(");
        return null;
    }
}