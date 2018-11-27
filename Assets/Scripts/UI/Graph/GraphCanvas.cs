using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public class GraphCanvas : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private VertexUI _vertexUiPrefab;
    [SerializeField] private RectTransform _verticesPlaceholder;
    
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
      (vertex.transform as RectTransform).anchoredPosition = Input.mousePosition;
      vertex.Index = Graph.VertexCount;
      Graph.AddVertex();
      
      RedrawMatrix();
    }

    public void RedrawMatrix()
    {
      MatrixUI.Instance.DrawMatrix(Graph.Vertices);
    }
    
    public void Clear()
    {
      foreach (Transform vertex in _verticesPlaceholder)
      {
        Destroy(vertex.gameObject);
      }
      
      Graph.Reset();
      RedrawMatrix();
    }
  }
}