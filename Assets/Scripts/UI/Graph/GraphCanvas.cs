using System.Collections.Generic;
using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public class GraphCanvas : Singleton<GraphCanvas>
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private VertexUI _vertexUiPrefab;
    [SerializeField] private RectTransform _verticesPlaceholder;
    [SerializeField] private IntTupleEvent _highlight;
    [SerializeField] private IntTupleEvent _highlightNoDirection;
    [SerializeField] private IntTupleEvent _pathIndex;
    [SerializeField] private GameEvent _highlightOff;
   
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private int MAX_VERTICES = 18;
    
    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      Graph.Reset();
    }

    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public void CreateVertex()
    {
      if (EdgeDrawer.Instance.Drawing) return;
      
      if (Graph.Vertices.Count >= 18)
      {
        SnackbarError.Instance.Show("Max vertices count");
        return;
      }
      
      
      var vertex = Instantiate(_vertexUiPrefab, _verticesPlaceholder);
      (vertex.transform as RectTransform).anchoredPosition = MouseFollower.Instance.Transform.anchoredPosition;
      vertex.Index = Graph.VertexCount;
      Graph.AddVertex();
    }
    
    public void Clear()
    {
      foreach (Transform vertex in _verticesPlaceholder)
      {
        Destroy(vertex.gameObject);
      }

      var directed = Graph.Oriented;
      Graph.Reset();
      Graph.Oriented = directed;
    }

    public void HighlightPath(List<Vertex> path)
    {
      for (int i = 0; i < path.Count - 1; i++)
      {
        _highlight.Raise(path[i].Index, path[i + 1].Index);
        _pathIndex.Raise(path[i].Index, i);
      }
      
      _pathIndex.Raise(path[path.Count - 1].Index, path.Count - 1);
    }
    
    public void HighlightTree(List<Edge> tree)
    {
      foreach (var edge in tree)
      {
        _highlightNoDirection.Raise(edge.StartNode, edge.EndNode);
      }
    }

    public void HighlightOff()
    {
      _highlightOff.Raise();
    }
  }
}