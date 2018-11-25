using System;
using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class AdjacentToggle : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------
    
    private Toggle _toggle;

    //---------------------------------------------------------------------
    // Properties
    //---------------------------------------------------------------------
    
    public bool Value
    {
      get { return _toggle.isOn; }
      set { _toggle.isOn = value; }
    }
    
    public Vector2 Index { get; set; }
    public Action<int, int, bool> OnValueChanged { get; set; }
    
    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _toggle = GetComponent<Toggle>();
      _toggle.onValueChanged.AddListener(OnClick);
    }
    
    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------

    private void OnClick(bool value)
    {
      OnValueChanged.Invoke((int) Index.x, (int) Index.y, value);
    }
  }
}