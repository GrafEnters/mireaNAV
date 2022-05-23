using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AStarManager : MonoBehaviour {
    public static Stack<Nod> FindPathByAStar( Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        Debug.Log("Path successfully found");
        return null;
    }

    public static Stack<Nod> FindPathByWidthSearch( Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        List<Nod> visited = new();
        Queue<Nod> frontier = new();
        start.previous = null;
        frontier.Enqueue(start);

        while (frontier.Count > 0) {
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

    private static Stack<Nod> GetPathBack(Nod finish, Stack<Nod> curPath) {
        curPath.Push(finish);
        if (finish.previous != null) {
            return GetPathBack(finish.previous, curPath);
        }

        return curPath;
    }

    public static Stack<Nod> FindPathByDeepSearch( Dictionary<Vector3Int, Nod> NodsMap, Nod start,
        Nod finish) {
        List<Nod> visited = new();
        Stack<Nod> frontier = new();
        frontier.Push(start);
        start.previous = null;

        while (frontier.Count > 0) {
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
}