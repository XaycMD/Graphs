using System;
using System.Collections.Generic;
using System.Linq;

namespace edu.ua.pavlusyk.masters
{
  public static class Prim
  {
    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------
    
    public static List<Edge> PrimAlgorithm(List<Vertex> vertices)
    {
      var graph = ListToMatrix(vertices);
      var edges = PrimAlgorithm(graph, vertices.Count);
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
    
    private static int[,] ListToMatrix(List<Vertex> vertices)
    {
      int n = vertices.Count;
      int[,] graph = new int[n, n];

      for (int i = 0; i < n; i++)
      {
        for (int j = 0; j < n; j++)
        {
          graph[i, j] = vertices[i].ConnectedTo.ContainsKey(vertices[j])
            ? Math.Abs(vertices[i].ConnectedTo[vertices[j]])
            : 0;
        }
      }

      return graph;
    }
    
    private static int MinKey(int[] key, bool[] set, int verticesCount)
    {
      int min = int.MaxValue, minIndex = 0;

      for (int v = 0; v < verticesCount; ++v)
      {
        if (set[v] == false && key[v] < min)
        {
          min = key[v];
          minIndex = v;
        }
      }

      return minIndex;
    }

    private static List<Edge> Print(int[] parent, int verticesCount)
    {
      var edges = new List<Edge>();
      
      for (int i = 1; i < verticesCount; ++i)
        edges.Add(new Edge
        {
          StartNode = parent[i],
          EndNode = i
        });

      return edges;
    }

    public static List<Edge> PrimAlgorithm(int[,] graph, int verticesCount)
    {
      int[] parent = new int[verticesCount];
      int[] key = new int[verticesCount];
      bool[] mstSet = new bool[verticesCount];

      for (int i = 0; i < verticesCount; ++i)
      {
        key[i] = int.MaxValue;
        mstSet[i] = false;
      }

      key[0] = 0;
      parent[0] = -1;

      for (int count = 0; count < verticesCount - 1; ++count)
      {
        int u = MinKey(key, mstSet, verticesCount);
        mstSet[u] = true;

        for (int v = 0; v < verticesCount; ++v)
        {
          if (Convert.ToBoolean(graph[u, v]) && mstSet[v] == false && graph[u, v] < key[v])
          {
            parent[v] = u;
            key[v] = graph[u, v];
          }
        }
      }

      return Print(parent, verticesCount);
    }
  }
}