using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class VertexUI : MonoBehaviour, IDragHandler, IPointerClickHandler
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------
    
    [SerializeField] private Text _text;
    
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private int _index;
    private bool _dragged;
    
    //---------------------------------------------------------------------
    // Properties
    //---------------------------------------------------------------------

    public int Index
    {
      get { return _index; }
      set
      {
        _index = value;
        _text.text = value.ToString();
      }
    }
    
    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public void DeleteVertex()
    {
      Graph.DeleteVertex(Index);
      MatrixUI.Instance.DrawMatrix(Graph.Vertices);
      Destroy(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (eventData.button == PointerEventData.InputButton.Left)
      {
        (transform as RectTransform).anchoredPosition = Input.mousePosition;
        _dragged = true;
      }
      
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (_dragged)
      {
        _dragged = false;
        return;
      }
      
      switch (eventData.button)
      {
        case PointerEventData.InputButton.Left:
          
          if (!EdgeDrawer.Instance.Drawing)
          {
            EdgeDrawer.Instance.StartDrawing(transform as RectTransform, Index);
          }
          else
          {
            EdgeDrawer.Instance.StopDrawing(transform as RectTransform, Index);
          }
          break;
        case PointerEventData.InputButton.Right:
          DeleteVertex();
          break;
      }
    }
  }
}