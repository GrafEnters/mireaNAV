using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AStarManager : MonoBehaviour {
    public static int CheckCount = 0;
    public static double MillisecondsPast = 0;
    

    public static Stack<Nod> FindPathByAStar(Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        CheckCount = 0;
        MillisecondsPast = 0;
        DateTime before = DateTime.Now;
        List<Nod> visited = new();
        Queue<Nod> frontier = new();
        start.previous = null;
        frontier.Enqueue(start);

        while (frontier.Count > 0) {
            CheckCount++;
            Nod current = frontier.Dequeue();
            visited.Add(current);

            if (current == finish) {
                MillisecondsPast = (DateTime.Now - before).TotalMilliseconds;
                return GetPathBack(current, new Stack<Nod>());
            }

            List<Nod> neighbours = new();
            foreach (var neighbour in current.Neighbours) {
                if (!visited.Contains(NodsMap[neighbour])) {
                    neighbours.Add(NodsMap[neighbour]);
                    NodsMap[neighbour].additionalValue =
                        Vector3Int.Distance(NodsMap[neighbour].coordinates, finish.coordinates);
                }
            }

            neighbours = neighbours.OrderBy(a => a.additionalValue).ToList();
            foreach (var neighbour in neighbours) {
                frontier.Enqueue(neighbour);
                neighbour.previous = current;
            }
        }
        MillisecondsPast = (DateTime.Now - before).TotalMilliseconds;
        return default;
    }

    public static Stack<Nod> FindPathByWidthSearch(Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        CheckCount = 0;
        MillisecondsPast = 0;
        DateTime before = DateTime.Now;
        List<Nod> visited = new();
        Queue<Nod> frontier = new();
        start.previous = null;
        frontier.Enqueue(start);

        while (frontier.Count > 0) {
            CheckCount++;
            Nod current = frontier.Dequeue();
            visited.Add(current);

            if (current == finish) {
                MillisecondsPast = (DateTime.Now - before).TotalMilliseconds;
                return GetPathBack(current, new Stack<Nod>());
            }

            List<Vector3Int> neighbours = current.Neighbours;
            neighbours = neighbours.OrderBy(a => Random.Range(0, 1f)).ToList();
            foreach (var neighbour in neighbours)
                if (!visited.Contains(NodsMap[neighbour])) {
                    frontier.Enqueue(NodsMap[neighbour]);
                    NodsMap[neighbour].previous = current;
                }
        }
        MillisecondsPast = (DateTime.Now - before).TotalMilliseconds;
        return default;
    }

    public static Stack<Nod> FindPathByDeepSearch(Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        CheckCount = 0;
        MillisecondsPast = 0;
        DateTime before = DateTime.Now;
        List<Nod> visited = new();
        Stack<Nod> frontier = new();
        frontier.Push(start);
        start.previous = null;

        while (frontier.Count > 0) {
            CheckCount++;
            var current = frontier.Pop();
            visited.Add(current);

            if (current == finish) {
                MillisecondsPast = (DateTime.Now - before).TotalMilliseconds;
                return GetPathBack(current, new Stack<Nod>());
            }

            List<Vector3Int> neighbours = current.Neighbours;
            neighbours = neighbours.OrderBy(a => Random.Range(0, 1f)).ToList();
            foreach (Vector3Int neighbour in neighbours)
                if (!visited.Contains(NodsMap[neighbour])) {
                    frontier.Push(NodsMap[neighbour]);
                    NodsMap[neighbour].previous = current;
                }
        }
        MillisecondsPast = (DateTime.Now - before).TotalMilliseconds;
        return default;
    }

    private static Stack<Nod> GetPathBack(Nod finish, Stack<Nod> curPath) {
        curPath.Push(finish);
        if (finish.previous != null) {
            return GetPathBack(finish.previous, curPath);
        }

        return curPath;
    }
}