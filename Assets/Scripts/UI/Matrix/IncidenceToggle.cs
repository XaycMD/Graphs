using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class IncidenceToggle : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private List<Sprite> _states;

    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private Image _currentState;
    private int _value;

    //---------------------------------------------------------------------
    // Properties
    //---------------------------------------------------------------------

    public int Value
    {
      get { return _value; }
      set
      {
        switch (value)
        {
          case 0:
            _currentState.sprite = _states[0];
            _value = 0;
            break;
          case 1:
            _value = 1;
            _currentState.sprite = _states[1];
            break;
          case -1:
            _value = -1;
            _currentState.sprite = _states[2];
            break;
          default:
            throw new Exception("Element can be only {-1, 0, 1}");
        }
      }
    }

    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _currentState = GetComponent<Image>();
    }
  }
}