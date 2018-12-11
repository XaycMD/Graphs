using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class SetText : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private Text _text;
    
    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _text = GetComponent<Text>();
    }
    
    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public void ShowPathLength(int value)
    {
      _text.text = "Path length: " + value;
    }
    
    public void ShowMaxFlow(int value)
    {
      _text.text = "Max flow: " + value;
    }

    public void Clear()
    {
      _text.text = "";
    }
  }
}