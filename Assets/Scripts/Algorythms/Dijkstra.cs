using System;
using System.Collections.Generic;
using System.Linq;

namespace edu.ua.pavlusyk.masters
{
  public static class Dijkstra
  {
    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public static List<Vertex> DijkstraAlgo(List<Vertex> vertices, int source, int destination)
    {
      var graph = ListToMatrix(vertices);
      var path = DijkstraAlgorithm(graph, vertices.IndexOf(vertices.First(x => x.Index == source)),
        vertices.IndexOf(vertices.First(x => x.Index == destination)));

      if (path == null) return null;
      
      var result = new List<Vertex>();
      
      foreach (var vertex in path)
      {
        result.Add(Graph.GetVertex(vertices[vertex].Index));  
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

    private static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode)
    {
      var n = graph.GetLength(0);

      var distance = new int[n];
      for (int i = 0; i < n; i++)
      {
        distance[i] = int.MaxValue;
      }

      distance[sourceNode] = 0;

      var used = new bool[n];
      var previous = new int?[n];

      while (true)
      {
        var minDistance = int.MaxValue;
        var minNode = 0;
        for (int i = 0; i < n; i++)
        {
          if (!used[i] && minDistance > distance[i])
          {
            minDistance = distance[i];
            minNode = i;
          }
        }

        if (minDistance == int.MaxValue)
        {
          break;
        }

        used[minNode] = true;

        for (int i = 0; i < n; i++)
        {
          if (graph[minNode, i] > 0)
          {
            var shortestToMinNode = distance[minNode];
            var distanceToNextNode = graph[minNode, i];

            var totalDistance = shortestToMinNode + distanceToNextNode;

            if (totalDistance < distance[i])
            {
              distance[i] = totalDistance;
              previous[i] = minNode;
            }
          }
        }
      }

      if (distance[destinationNode] == int.MaxValue)
      {
        return null;
      }

      var path = new LinkedList<int>();
      int? currentNode = destinationNode;
      while (currentNode != null)
      {
        path.AddFirst(currentNode.Value);
        currentNode = previous[currentNode.Value];
      }

      return path.ToList();
    }
  }
}