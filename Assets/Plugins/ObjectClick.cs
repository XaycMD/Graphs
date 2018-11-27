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

    [SerializeField] private UnityEvent _onClick;

    //---------------------------------------------------------------------
    // Events
    //---------------------------------------------------------------------

    public void OnPointerClick(PointerEventData eventData)
    {
      Debug.Log("OnClick: " + transform.name);
      _onClick.Invoke();
    }
  }
}