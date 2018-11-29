using System;
using System.Collections.Generic;
using System.Linq;

namespace edu.ua.pavlusyk.masters
{
  public class Kruskal
  {
    public static List<Edge> KruskalAlg(List<Vertex> vertices)
    {
      var edges = KruskalAlg(vertices.Count, VerticesToEdgeList(vertices));
      var result = new List<Edge>();

      foreach (var edge in edges)
      {
        result.Add(new Edge
        {
          StartNode = Graph.GetVertex(vertices[edge.StartNode].Index).Index,
          EndNode = Graph.GetVertex(vertices[edge.EndNode].Index).Index
        });
      }

      return result;
    }

    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------

    private static List<Edge> VerticesToEdgeList(List<Vertex> vertices)
    {
      int n = vertices.Count;
      var edges = new List<Edge>();

      for (int i = 0; i < n; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (vertices[i].ConnectedTo.ContainsKey(vertices[j]))
          {
            edges.Add(new Edge
            {
              StartNode = i,
              EndNode = j,
              Weight = Math.Abs(vertices[i].ConnectedTo[vertices[j]])
            });
          }
        }
      }

      return edges;
    }

    private static List<Edge> KruskalAlg(int numberOfVertices, List<Edge> edges)
    {
      // Inital sort
      edges = edges.OrderBy(e => e.Weight).ToList();

      // Set parents table
      var parent = Enumerable.Range(0, numberOfVertices).ToArray();

      // Spanning tree list
      var spanningTree = new List<Edge>();
      foreach (var edge in edges)
      {
        var startNodeRoot = FindRoot(edge.StartNode, parent);
        var endNodeRoot = FindRoot(edge.EndNode, parent);

        if (startNodeRoot != endNodeRoot)
        {
          // Add edge to the spanning tree
          spanningTree.Add(edge);

          // Mark one root as parent of the other
          parent[endNodeRoot] = startNodeRoot;
        }
      }

      // Return the spanning tree
      return spanningTree;
    }

    private static int FindRoot(int node, int[] parent)
    {
      var root = node;
      while (root != parent[root])
      {
        root = parent[root];
      }

      while (node != root)
      {
        var oldParent = parent[node];
        parent[node] = root;
        node = oldParent;
      }

      return root;
    }
  }
}