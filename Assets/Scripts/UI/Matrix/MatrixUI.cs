using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class MatrixUI : Singleton<MatrixUI>
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private int _itemSize;
    [SerializeField] private IncidenceToggle _togglePrefab;
    [SerializeField] private Text _textPrefab;

    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private RectTransform _transform;

    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _transform = transform as RectTransform;
      Graph.OnGraphChanged = RedrawMatrix;
    }

    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public void DrawMatrix(List<Vertex> vertices)
    {
      Clear();
      SetSize(vertices.Count);
      DrawText(vertices);
      DrawToggles(vertices);
    }
    
    public void RedrawMatrix()
    {
      DrawMatrix(Graph.Vertices);
    }
    
    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------
    
    private void Clear()
    {
      foreach (Transform item in transform)
      {
        Destroy(item.gameObject);
      }
    }
    
    private void SetSize(int itemsCount)
    {
      var size = _itemSize * (itemsCount + 1);
      _transform.sizeDelta = new Vector2(size, size);
    }
    
    private void DrawText(List<Vertex> vertices)
    {
      var position = _itemSize;

      for (var i = 0; i < vertices.Count; i++)
      {
        InstantiateText(new Vector2(position + i * _itemSize, 0), vertices[i].Index);
        InstantiateText(new Vector2(0, -(position + i * _itemSize)), vertices[i].Index);
      }
    }

    private void InstantiateText(Vector2 position, int number)
    {
      var text = Instantiate(_textPrefab, _transform);
      text.GetComponent<RectTransform>().anchoredPosition = position;
      text.text = number.ToString();
    }
    
    private void DrawToggles(List<Vertex> vertices)
    {
      var position = _itemSize;
      
      for (var i = 0; i < vertices.Count; i++)
      {
        for (var j = 0; j < vertices.Count; j++)
        {
          InstantiateToggle(new Vector2(position + i * _itemSize, -(position + j * _itemSize)), 
            vertices[i].ConnectedTo.ContainsKey(vertices[j]) ? Graph.GetEdgeWeight(vertices[i].Index, vertices[j].Index) : 0);
        }
      }
    }

    private void InstantiateToggle(Vector2 position, int value)
    {
      var toggle = Instantiate(_togglePrefab, _transform);
      toggle.GetComponent<RectTransform>().anchoredPosition = position;
      toggle.Value = value;
    }
  }
}