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
    [SerializeField] private Text _textValue;

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
        if(value > 0) _currentState.sprite = _states[1];
        if(value < 0) _currentState.sprite = _states[2];
        if (value == 0) _currentState.sprite = _states[0];
        _value = value;
        _textValue.text = value.ToString();
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