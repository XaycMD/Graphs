using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
	public class ToggleUI : MonoBehaviour
	{
		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------

		[Header("Elements")]
		[SerializeField] private Image _background;
		[SerializeField] private Image _toggle;
		
		[Space]
		[Header("Specs")]
		[SerializeField] private Color _onColor;
		[SerializeField] private Color _offColor;
		[SerializeField] private float _toggleOnPosition;
		[SerializeField] private float _toggleOffPosition;
		
		//---------------------------------------------------------------------
		// Internal
		//---------------------------------------------------------------------

		private float _time = .1f;
		private Color _bgOnColor;
		private Color _bgOffColor;
		private RectTransform _toggleTransform;
		private bool _on;
		
		//---------------------------------------------------------------------
		// Messages
		//---------------------------------------------------------------------

		private void Awake()
		{
			_bgOnColor = new Color(_onColor.r, _onColor.g, _onColor.b, 152f/255);
			_bgOffColor = new Color(_offColor.r, _offColor.g, _offColor.b, 152f/255);
			_toggleTransform = _toggle.gameObject.GetComponent<RectTransform>();
		}

		//---------------------------------------------------------------------
		// Public
		//---------------------------------------------------------------------

		public void OnClick()
		{
			if(_on) ToggleOff();
			else ToggleOn();
			_on = !_on;
			Graph.Oriented = _on;
		}
		
		//---------------------------------------------------------------------
		// Helpers
		//---------------------------------------------------------------------

		private void ToggleOn()
		{
			StartCoroutine(ToggleCoroutine(_onColor, _bgOnColor, _toggleOnPosition));
		}

		private void ToggleOff()
		{
			StartCoroutine(ToggleCoroutine(_offColor, _bgOffColor, _toggleOffPosition));
		}
		
		private IEnumerator ToggleCoroutine(Color toggleDestinationColor, Color bgDestinationColor, float toggleDestinationPosition)
		{
			var _currentTime = 0f;

			while (_currentTime < _time)
			{
				var progress = _currentTime / _time;
				
				LerpColor(_toggle, toggleDestinationColor, progress);
				LerpColor(_background, bgDestinationColor, progress);
				LerpPosition(_toggleTransform, toggleDestinationPosition, progress);
				
				_currentTime += Time.deltaTime;
				yield return null;
			}
			
			_toggle.color = toggleDestinationColor;
			_background.color = bgDestinationColor;
			_toggleTransform.anchoredPosition = new Vector2(toggleDestinationPosition, _toggleTransform.anchoredPosition.y);
		}

		private void LerpColor(Image target, Color b, float progress)
		{
			var currentColor = target.color;
			currentColor = Color.Lerp(currentColor, b, progress);
			target.color = currentColor;
		}

		private void LerpPosition(RectTransform target, float b, float progress)
		{
			var currentPos = target.anchoredPosition;
			currentPos = new Vector2(Mathf.Lerp(currentPos.x, b, progress), currentPos.y);
      _toggleTransform.anchoredPosition = currentPos;
		}
	}
}