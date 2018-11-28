using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public class MouseFollower : Singleton<MouseFollower>
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------
    
    [SerializeField] private Canvas _canvas;
    
    //---------------------------------------------------------------------
    // Properties
    //---------------------------------------------------------------------

    public RectTransform Transform { get; set; }

    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      Transform = transform as RectTransform;
    }

    private void Update()
    {
      Transform.anchoredPosition = Input.mousePosition / _canvas.scaleFactor;
    }
  }
}