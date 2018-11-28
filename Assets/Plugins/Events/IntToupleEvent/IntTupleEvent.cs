using System.Collections.Generic;
using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  [CreateAssetMenu(menuName = "Events/TupleEvent")]
  public class IntTupleEvent : ScriptableObject
  {
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------
    
    private List<IntTupleEventListener> _listeners = new List<IntTupleEventListener>();

    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------
    
    public void Raise(int j, int k)
    {
      for (var i = _listeners.Count - 1; i >= 0; i--)
      {
        _listeners[i].OnEventRaised(j, k);
      }
    }

    public void RegisterListener(IntTupleEventListener listener)
    {
      _listeners.Add(listener);
    }

    public void UnRegisterListener(IntTupleEventListener listener)
    {
      _listeners.Remove(listener);
    }
  }
}