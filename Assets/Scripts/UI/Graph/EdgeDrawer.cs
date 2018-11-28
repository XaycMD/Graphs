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

    private const float DEFAULT_LINE_WIDTH = 10f;
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
      _currentEdge.End = end;
      Graph.ConnectVertex(_start, index, 1);
      _currentEdge.EndVertex = index;
      Drawing = false;
    }

    public void CancelDrawing()
    {
      _currentEdge.Delete();
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