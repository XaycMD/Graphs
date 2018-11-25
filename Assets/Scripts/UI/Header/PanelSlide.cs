using System.Collections;
using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public class PanelSlide : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private int _slideDistance;
    [SerializeField] private int _positionCount;
    
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------
    
    private RectTransform _transform;
    private int _position;
    
    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _transform = transform as RectTransform;
    }

    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------
    
    public void SlideLeft()
    {
      _position--;
      _position = (_position < 0 ? _positionCount - 1 : _position) % _positionCount;
      StartCoroutine(Slide(-_position * _slideDistance, .25f));
    }
    
    public void SlideRight()
    {
      _position = (_position + 1) % _positionCount;
      StartCoroutine(Slide(-_position * _slideDistance, .25f));
    }
    
    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------

    private IEnumerator Slide(float position, float time)
    {
      var currentTime = 0f;

      while (time - currentTime > 0)
      {
        var progress = currentTime / time;
        _transform.anchoredPosition = new Vector2(
          Mathf.Lerp(_transform.anchoredPosition.x, position, progress), _transform.anchoredPosition.y);
        currentTime += Time.deltaTime;
        yield return null;
      }
      
      _transform.anchoredPosition = new Vector2(position, _transform.anchoredPosition.y);
    }
  }
}