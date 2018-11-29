using System;
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
        return;
      } 

      //masters.FloydWarshall.FloydWarshallAlgorithm(Graph.Vertices);
      
      //var path = masters.FloydWarshall.FloydWarshallAlgorithm(Graph.Vertices, from, to);

//      if (path == null)
//      {
//        SnackbarError.Instance.Show("Path does not exist");
//        return;
//      }
//
//      GraphCanvas.Instance.HighlightPath(path);
    }

    public void Kruskal()
    {
      GraphCanvas.Instance.HighlightOff();

      if (Graph.VertexCount == 0)
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

      if (Graph.VertexCount == 0)
      {
        SnackbarError.Instance.Show("Draw graph");
        return;
      }

      var spanningTree = masters.Prim.PrimAlgorithm(Graph.Vertices);
      GraphCanvas.Instance.HighlightTree(spanningTree);
    }
  }
}