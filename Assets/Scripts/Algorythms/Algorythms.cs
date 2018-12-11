using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class Algorythms : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private InputField _from;
    [SerializeField] private InputField _to;
    [SerializeField] private InputField _fromFord;
    [SerializeField] private InputField _toFord;
    [SerializeField] private IntGameEvent _showPathLength;
    [SerializeField] private IntGameEvent _showMaxFlow;

    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public void EulerTour()
    {
      GraphCanvas.Instance.HighlightOff();

      if (Graph.VertexCount == 0)
      {
        SnackbarError.Instance.Show("Draw graph");
        return;
      }

      var path = EulerCircuit.FindEulerCircuit(Graph.Vertices);

      if (path == null)
      {
        SnackbarError.Instance.Show("Euler Path or Circuit not Possible");
        return;
      }

      GraphCanvas.Instance.HighlightPath(path);
    }

    public void Dijkstra()
    {
      GraphCanvas.Instance.HighlightOff();

      if (Graph.VertexCount == 0)
      {
        SnackbarError.Instance.Show("Draw graph");
        return;
      }

      if (_from.text == "" || _to.text == "")
      {
        SnackbarError.Instance.Show("Enter vertices");
        return;
      }
      
      var from = Convert.ToInt32(_from.text);
      var to = Convert.ToInt32(_to.text);

      if (!Graph.Exist(from) || !Graph.Exist(to))
      {
        SnackbarError.Instance.Show("Vertex does not exist");
        return;
      } 

      var path = masters.Dijkstra.DijkstraAlgo(Graph.Vertices, from, to);

      if (path == null)
      {
        SnackbarError.Instance.Show("Path does not exist");
        return;
      }

      GraphCanvas.Instance.HighlightPath(path);
      _showPathLength.Raise(GetPathLength(path));
    }

    public void FloydWarshall()
    {
      GraphCanvas.Instance.HighlightOff();

      if (Graph.VertexCount == 0)
      {
        SnackbarError.Instance.Show("Draw graph");
        return;
      }

      if (_from.text == "" || _to.text == "")
      {
        SnackbarError.Instance.Show("Enter vertices");
        return;
      }
      
      var from = Convert.ToInt32(_from.text);
      var to = Convert.ToInt32(_to.text);

      if (!Graph.Exist(from) || !Graph.Exist(to))
      {
        SnackbarError.Instance.Show("Vertex does not exist");
      } 
    }

    public void Kruskal()
    {
      GraphCanvas.Instance.HighlightOff();

      if (Graph.Vertices.Count == 0)
      {
        SnackbarError.Instance.Show("Draw graph");
        return;
      }

      var spanningTree = masters.Kruskal.KruskalAlg(Graph.Vertices);
      GraphCanvas.Instance.HighlightTree(spanningTree);
    }
    
    public void Prim()
    {
      GraphCanvas.Instance.HighlightOff();

      if (Graph.Vertices.Count == 0)
      {
        SnackbarError.Instance.Show("Draw graph");
        return;
      }

      var spanningTree = masters.Prim.PrimAlgorithm(Graph.Vertices);
      GraphCanvas.Instance.HighlightTree(spanningTree);
    }

    public void Ford()
    {
      GraphCanvas.Instance.HighlightOff();

      if (Graph.Vertices.Count == 0)
      {
        SnackbarError.Instance.Show("Draw graph");
        return;
      }
      
      if (_fromFord.text == "" || _toFord.text == "")
      {
        SnackbarError.Instance.Show("Enter vertices");
        return;
      }
      
      var from = Convert.ToInt32(_fromFord.text);
      var to = Convert.ToInt32(_toFord.text);

      if (!Graph.Exist(from) || !Graph.Exist(to))
      {
        SnackbarError.Instance.Show("Vertex does not exist");
        return;
      }
      
      var ford = new FordFulkerson(Graph.Vertices);
      ford.Run(from, to);
      _showMaxFlow.Raise((int)ford.MaxFlow);
    }
    
    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------

    private int GetPathLength(List<Vertex> path)
    {
      var sum = 0;
      
      for (int i = 0; i < path.Count - 1; i++)
      {
        sum += Graph.GetEdgeWeight(path[i].Index, path[i + 1].Index);
      }

      return sum;
    }
  }
}