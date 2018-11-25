using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class SnackbarLeave : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private Image _snackbar;
    
    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private CanvasGroup _snackbarCanvasGroup;
    private bool _escPressed;

    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _snackbarCanvasGroup = _snackbar.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        if (!_escPressed)
        {
          SetSnackbarActive(true);
          Invoke(nameof(TurnOffSnackbar), 1.5f);
        }
        else
        {
          Application.Quit();
        }
      }
    }
    
    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------
    
    private void TurnOffSnackbar()
    {
      SetSnackbarActive(false);
    }

    private void SetSnackbarActive(bool value)
    {
      _escPressed = value;
      StartCoroutine(ShowSnackbar(value));
    }

    private IEnumerator ShowSnackbar(bool value)
    {
      if(value) _snackbar.gameObject.SetActive(true);
      
      var targetAlpha = value ? 1 : 0;
      var currentTime = 0f;
      var animationLength = .25f;

      while (currentTime < animationLength)
      {
        var progress = currentTime / animationLength;
        _snackbarCanvasGroup.alpha = Mathf.Lerp(_snackbarCanvasGroup.alpha, targetAlpha, progress);
        currentTime += Time.deltaTime;
        yield return null;
      }

      _snackbarCanvasGroup.alpha = targetAlpha;
      if(!value) _snackbar.gameObject.SetActive(false);
    }
  }
}