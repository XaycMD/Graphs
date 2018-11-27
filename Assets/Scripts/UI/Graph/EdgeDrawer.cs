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
    
    //---------------------------------------------------------------------
    // Properties
    //---------------------------------------------------------------------
    
    public bool Drawing { get; private set; }
    
    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public void StartDrawing(RectTransform start)
    {
      _currentEdge = Instantiate(_edgeUiPrefab, _edgesPlaceholder);
      _currentEdge.StartDrawing(start, MouseFollower.Instance.Transform, DEFAULT_LINE_WIDTH);
      Drawing = true;
    }

    public void StopDrawing(RectTransform end)
    {
      _currentEdge.End = end;
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