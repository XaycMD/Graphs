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
    [SerializeField] private IntGameEvent _highlightStart;
    
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
      
      Graph.Reset();
    }

    public void HighlightPath(List<Vertex> path)
    {
      for (int i = 0; i < path.Count - 1; i++)
      {
        _highlight.Raise(path[i].Index, path[i + 1].Index);
      }
      
      _highlightStart.Raise(path[0].Index);
    }
  }
}