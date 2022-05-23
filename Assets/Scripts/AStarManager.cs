using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AStarManager : MonoBehaviour {
    public static int checkCount = 0;

    public static Stack<Nod> FindPathByAStar(Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        checkCount = 0;
        List<Nod> visited = new();
        Queue<Nod> frontier = new();
        start.previous = null;
        frontier.Enqueue(start);

        while (frontier.Count > 0) {
            checkCount++;
            Nod current = frontier.Dequeue();
            visited.Add(current);

            if (current == finish)
                return GetPathBack(current, new Stack<Nod>());

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

        return default;
    }

    public static Stack<Nod> FindPathByWidthSearch(Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        checkCount = 0;
        List<Nod> visited = new();
        Queue<Nod> frontier = new();
        start.previous = null;
        frontier.Enqueue(start);

        while (frontier.Count > 0) {
            checkCount++;
            Nod current = frontier.Dequeue();
            visited.Add(current);

            if (current == finish)
                return GetPathBack(current, new Stack<Nod>());

            List<Vector3Int> neighbours = current.Neighbours;
            neighbours = neighbours.OrderBy(a => Random.Range(0, 1f)).ToList();
            foreach (var neighbour in neighbours)
                if (!visited.Contains(NodsMap[neighbour])) {
                    frontier.Enqueue(NodsMap[neighbour]);
                    NodsMap[neighbour].previous = current;
                }
        }

        return default;
    }

    public static Stack<Nod> FindPathByDeepSearch(Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        checkCount = 0;
        List<Nod> visited = new();
        Stack<Nod> frontier = new();
        frontier.Push(start);
        start.previous = null;

        while (frontier.Count > 0) {
            checkCount++;
            var current = frontier.Pop();
            visited.Add(current);

            if (current == finish)
                return GetPathBack(current, new Stack<Nod>());

            List<Vector3Int> neighbours = current.Neighbours;
            neighbours = neighbours.OrderBy(a => Random.Range(0, 1f)).ToList();
            foreach (Vector3Int neighbour in neighbours)
                if (!visited.Contains(NodsMap[neighbour])) {
                    frontier.Push(NodsMap[neighbour]);
                    NodsMap[neighbour].previous = current;
                }
        }

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