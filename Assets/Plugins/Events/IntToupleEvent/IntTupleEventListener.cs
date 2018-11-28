using UnityEngine;
using UnityEngine.Events;

namespace edu.ua.pavlusyk.masters
{
  public class IntTupleEventListener : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------
		
    public IntTupleEvent Event;
    public IntTupleUnityEvent Response;

    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------
		
    private void OnEnable()
    {
      Event.RegisterListener(this);
    }

    private void OnDisable()
    {
      Event.UnRegisterListener(this);
    }

    //---------------------------------------------------------------------
    // Events
    //---------------------------------------------------------------------
		
    public void OnEventRaised(int i, int j)
    {
      Response.Invoke(i, j);
    }
  }
}