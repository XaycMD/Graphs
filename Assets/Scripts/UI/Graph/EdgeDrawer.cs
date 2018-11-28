using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public class EdgeDrawer : Singleton<EdgeDrawer>
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private EdgeUI _edgeUiPrefab;
    [SerializeField] private RectTransform _edgesPlaceholder;

    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private const float DEFAULT_LINE_WIDTH = 18f;
    private EdgeUI _currentEdge;
    private int _start;

    //---------------------------------------------------------------------
    // Properties
    //---------------------------------------------------------------------

    public bool Drawing { get; private set; }

    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public void StartDrawing(RectTransform start, int index)
    {
      _currentEdge = Instantiate(_edgeUiPrefab, _edgesPlaceholder);
      _currentEdge.StartDrawing(start, MouseFollower.Instance.Transform, DEFAULT_LINE_WIDTH);
      _currentEdge.StartVertex = index;
      Drawing = true;
      _start = index;
    }

    public void StopDrawing(RectTransform end, int index)
    {
      if (_currentEdge.StartVertex == index || Graph.VertexConnected(_currentEdge.StartVertex, index))
      {
        _currentEdge.Delete();
        CancelDrawing();
        return;
      }
      
      Graph.ConnectVertex(_start, index, 1);
      _currentEdge.EndVertex = index;
      _currentEdge.Weight = 1;
      _currentEdge.End = end;
      _currentEdge.Drawn = true;
      _currentEdge.AdjustArrowPosition();
      Drawing = false;
    }

    public void CancelDrawing()
    {
      _currentEdge = null;
      Drawing = false;
    }

    public void Clear()
    {
      foreach (Transform edge in _edgesPlaceholder)
      {
        Destroy(edge.gameObject);
      }
    }
  }
}