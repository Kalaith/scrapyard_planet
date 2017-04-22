using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
using System.Linq;

public class Path_AStar {

    Queue<Tile> path;

    public Path_AStar(Map map, Tile tileStart, Tile tileEnd) {

        Dictionary<Tile, Path_Node<Tile>> nodes = map.TileGraph.Nodes;

        Path_Node<Tile> start = nodes[tileStart];
        Path_Node<Tile> goal = nodes[tileEnd];


        if (nodes.ContainsKey(tileStart) == false) {
            Debug.LogError("Starting tile is not in list of nodes.");
        }
        if (nodes.ContainsKey(tileEnd) == false) {
            Debug.LogError("Ending tile is not in list of nodes.");
        }

        List<Path_Node<Tile>> ClosedSet = new List<Path_Node<Tile>>();

        /*List<Path_Node<Tile>> OpenSet = new List<Path_Node<Tile>>();
        OpenSet.Add(nodes[tileStart]);*/

        SimplePriorityQueue<Path_Node<Tile>> OpenSet = new SimplePriorityQueue<Path_Node<Tile>>();
        OpenSet.Enqueue(start, 0);

        Dictionary<Path_Node<Tile>, Path_Node<Tile>> Came_From = new Dictionary<Path_Node<Tile>, Path_Node<Tile>> ();

        Dictionary<Path_Node<Tile>, float> g_score = new Dictionary<Path_Node<Tile>, float>();

        foreach(Path_Node<Tile> n in nodes.Values) {
            g_score[n] = Mathf.Infinity;
        }

        g_score[start] = 0;

        Dictionary<Path_Node<Tile>, float> f_score = new Dictionary<Path_Node<Tile>, float>();

        foreach (Path_Node<Tile> n in nodes.Values) {
            f_score[n] = Mathf.Infinity;
        }

        f_score[start] = heuristic_cost_estimate(start, goal);

        while(OpenSet.Count > 0) {

            Path_Node<Tile> current = OpenSet.Dequeue();

            if(current == goal) {
                
                reconstruct_path(Came_From, current);

                return;
            }

            ClosedSet.Add(current);
            foreach(Path_Edge<Tile> edge_neighbour in current.edges) {
                Path_Node<Tile> neighbour = edge_neighbour.node;

                if (ClosedSet.Contains(neighbour) == true)
                    continue;

                float tentative_g_score = g_score[current] + dist_between(current, neighbour);

                if(OpenSet.Contains(neighbour) && tentative_g_score >= g_score[neighbour])
                    continue;

                Came_From[neighbour] = current;
                g_score[neighbour] = tentative_g_score;
                f_score[neighbour] = g_score[neighbour] + heuristic_cost_estimate(neighbour, goal);

                if(OpenSet.Contains(neighbour) == false) {
                    OpenSet.Enqueue(neighbour, f_score[neighbour]);
                }
            }
        }
        // If we get here there is no path from start to goal
       
    }

    float heuristic_cost_estimate(Path_Node<Tile> a, Path_Node<Tile> b) {
        return Mathf.Sqrt(
            Mathf.Pow(a.data.X - b.data.X, 2) + 
            Mathf.Pow(a.data.Y - b.data.Y, 2));
    }

    float dist_between(Path_Node<Tile> a, Path_Node<Tile> b) {
        if(Mathf.Abs(a.data.X - b.data.X) + Mathf.Abs(a.data.Y - b.data.Y) == 1) {
            return 1f;
        }

        if (Mathf.Abs(a.data.X - b.data.X) == 1 && Mathf.Abs(a.data.Y - b.data.Y) == 1) {
            return 1.41421356237f;
        }

        return Mathf.Sqrt(
            Mathf.Pow(a.data.X - b.data.X, 2) +
            Mathf.Pow(a.data.Y - b.data.Y, 2));

    }

    // Go backwards through the path.
    void reconstruct_path(
        Dictionary<Path_Node<Tile>, Path_Node<Tile>> Came_From, 
        Path_Node<Tile> current) {

        Queue<Tile> total_path = new Queue<Tile>();
        total_path.Enqueue(current.data);

        while(Came_From.ContainsKey(current)) {
            current = Came_From[current];
            total_path.Enqueue(current.data);
        }

        path = new Queue<Tile>(total_path.Reverse());

    }

    Tile GetNextTile() {
        return path.Dequeue();
    }
}
