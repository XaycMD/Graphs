using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class VertexUI : MonoBehaviour
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
  }
}