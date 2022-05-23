using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AStarManager : MonoBehaviour {
    public static Stack<Nod> FindPathByAStar(List<Nod> nods,Dictionary<Vector3Int, Nod> NodsMap, Nod start, Nod finish) {
        Debug.Log("Path successfully found");
        return null;
    }
    
    public static Stack<Nod> FindPathByWidthSearch(List<Nod> nods,Dictionary<Vector3Int, Nod> NodsMap, Nod start, Nod finish) {
        List<Nod> visited = new List<Nod>();
        Queue<Nod> frontier = new Queue<Nod>();
        start.previous = null;
        frontier.Enqueue(start);
            
        while (frontier.Count > 0)
        {
            Nod current = frontier.Dequeue();
            visited.Add(current);
                
            if (current == finish)
                return GetPathBack(current, new Stack<Nod>());

            var neighbours = current.Neighbours;
            foreach(var neighbour in neighbours)
                if (!visited.Contains(NodsMap[neighbour])) {
                    frontier.Enqueue(NodsMap[neighbour]);
                    NodsMap[neighbour].previous = current;
                }
        }

        return default;
    }

    private static Stack<Nod> GetPathBack(Nod finish,Stack<Nod> curPath ) {
        curPath.Push(finish);
        if (finish.previous != null) {
            return GetPathBack(finish.previous, curPath);
        } 
        return curPath;
    }

    public static Stack<Nod> FindPathByDeepSearch(List<Nod> nods, Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        List<Nod> visited = new();
        Stack<Nod> path = new();

        bool RecursivePushNod(Nod next) {
            path.Push(next);
            visited.Add(next);
            if (next == finish) {
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
    }
}