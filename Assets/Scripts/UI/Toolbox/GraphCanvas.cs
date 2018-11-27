using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public class GraphCanvas : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private VertexUI _vertexUiPrefab;
    
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
      var vertex = Instantiate(_vertexUiPrefab, transform);
      (vertex.transform as RectTransform).anchoredPosition = Input.mousePosition;
      vertex.Index = Graph.VertexCount;
      Graph.AddVertex();
      
      RedrawMatrix();
    }

    public void RedrawMatrix()
    {
      IncidenceMatrixUI.Instance.DrawMatrix(Graph.Vertices);
    }
  }
}