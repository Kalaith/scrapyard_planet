using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_TileGraph {

    Dictionary<Tile, Path_Node<Tile>> nodes;

    public Dictionary<Tile, Path_Node<Tile>> Nodes {
        get { return nodes; }
    }

    // Creates a graph of the world to use for pathfinding.
    public Path_TileGraph(Map map) {

        nodes = new Dictionary<Tile, Path_Node<Tile>>();

        for (int x = 0; x < map.Width; x++) {
            for (int y = 0; y < map.Height; y++) {

                Tile t = map.GetTileAt(x, y);

                // 0 is unwalkable.
                if (t.Cost > 0) {
                    Path_Node<Tile> n = new Path_Node<Tile>();
                    n.data = t;
                    nodes.Add(t, n);
                }
            }

        }

        foreach (Tile t in nodes.Keys) {
            Path_Node<Tile> n = nodes[t];

            List<Path_Edge<Tile>> edges = new List<Path_Edge<Tile>>();

            Tile[] neighbours = t.GetNeighbours(true);

            for (int i = 0; i < neighbours.Length; i++) {
                if (neighbours[i] != null && neighbours[i].Cost > 0) {
                    Path_Edge<Tile> e = new Path_Edge<Tile>();

                    e.cost = neighbours[i].Cost;
                    e.node = nodes[neighbours[i]];

                    edges.Add(e);
                }
            }

            n.edges = edges.ToArray();
        }

        Debug.Log("Pat_Tilegraph: Created " + nodes.Count + " nodes.");
    }
}
