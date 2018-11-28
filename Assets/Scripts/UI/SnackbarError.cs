using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace edu.ua.pavlusyk.masters
{
  public class SnackbarError : Singleton<SnackbarError>
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private Image _snackbar;
    [SerializeField] private Text _message;

    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------
    private CanvasGroup _snackbarCanvasGroup;

    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _snackbarCanvasGroup = _snackbar.GetComponent<CanvasGroup>();
    }

    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public void Show(string msg)
    {
      _message.text = msg;
      SetSnackbarActive(true);
      Invoke(nameof(TurnOffSnackbar), 2f);
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
      StartCoroutine(ShowSnackbar(value));
    }

    private IEnumerator ShowSnackbar(bool value)
    {
      if (value) _snackbar.gameObject.SetActive(true);

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
      if (!value) _snackbar.gameObject.SetActive(false);
    }
  }
}