using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public class MouseFollower : Singleton<MouseFollower>
  {
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
      Transform.anchoredPosition = Input.mousePosition;
    }
  }
}