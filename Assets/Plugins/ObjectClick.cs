using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace edu.ua.pavlusyk.masters
{
  public class ObjectClick : MonoBehaviour, IPointerClickHandler
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private UnityEvent _onLeftButtonClick;
    [SerializeField] private UnityEvent _onRightButtonClick;

    //---------------------------------------------------------------------
    // Events
    //---------------------------------------------------------------------

    public void OnPointerClick(PointerEventData eventData)
    {
      if(eventData.button == PointerEventData.InputButton.Left) _onLeftButtonClick.Invoke();
      if (eventData.button == PointerEventData.InputButton.Right) _onRightButtonClick.Invoke();
    }
  }
}