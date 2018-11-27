using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class VertexUI : MonoBehaviour, IDragHandler
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------
    
    [SerializeField] private Text _text;
    
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private int _index;
    
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
      if(eventData.button == PointerEventData.InputButton.Left) 
        (transform as RectTransform).anchoredPosition = Input.mousePosition;
    }
  }
}