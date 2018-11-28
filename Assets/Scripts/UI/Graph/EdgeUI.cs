using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
	public class EdgeUI : MonoBehaviour, IPointerClickHandler
	{
		//---------------------------------------------------------------------
		// Editor
		//---------------------------------------------------------------------

		[SerializeField] private InputField _weightText;
		[SerializeField] private Image _arrowEnd;
		[SerializeField] private Image _arrowStart;
		[SerializeField] private Image _body;
		[SerializeField] private Color _usualColor;
		[SerializeField] private Color _highlightedColor;
		
		//---------------------------------------------------------------------
		// Internal
		//---------------------------------------------------------------------

		private RectTransform _transform;
		private bool _drawing;
		private int _weight;
		
		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------

		public RectTransform Start { get; set; }
		public RectTransform End { get; set; }
		public float Width { get; set; }
		public int StartVertex { get; set; }
		public int EndVertex { get; set; }
		public bool Drawn { get; set; }

		public int Weight
		{
			get { return _weight; }
			set
			{
				_weight = value;
				_weightText.text = value.ToString();
				if(Drawn) Graph.SetEdgeWeight(StartVertex, EndVertex, value);
			}
		}

		//---------------------------------------------------------------------
		// Messages
		//---------------------------------------------------------------------

		private void Awake()
		{
			_transform = transform as RectTransform;
			_weightText.onEndEdit.AddListener(SetWeight);
		}

		private void Update()
		{
			if(!_drawing) return;
			try
			{
				DrawLine(Start.anchoredPosition, End.anchoredPosition, Width);
			}
			catch
			{
				Delete();
			}
		}
		
		//---------------------------------------------------------------------
		// Public
		//---------------------------------------------------------------------

		public void StartDrawing(RectTransform start, RectTransform end, float width)
		{
			Start = start;
			End = end;
			Width = width;
			_drawing = true;
			_arrowEnd.gameObject.SetActive(Graph.Oriented);
		}

		public void Delete()
		{
			Destroy(gameObject);
		}

		public void SetArrowActive()
		{
			_arrowEnd.gameObject.SetActive(Graph.Oriented);
		}

		public void AdjustArrowPosition()
		{
			_arrowEnd.GetComponent<RectTransform>().anchoredPosition = new Vector2(-35, 0);
		}

		public void SetHighlighted(bool value)
		{
			if(!value) _arrowStart.gameObject.SetActive(false);
			SetArrowActive();
			_body.color = _arrowEnd.color = _arrowStart.color = value ? _highlightedColor : _usualColor;
		}

		public void OnHighlight(int start, int end)
		{
			if (StartVertex == start && EndVertex == end)
			{
				SetHighlighted(true);
				_arrowEnd.gameObject.SetActive(true);
				_arrowStart.gameObject.SetActive(false);
			}

			if (StartVertex == end && EndVertex == start)
			{
				SetHighlighted(true);
				_arrowEnd.gameObject.SetActive(false);
				_arrowStart.gameObject.SetActive(true);
			}
		}

		//---------------------------------------------------------------------
		// Helpers
		//---------------------------------------------------------------------
		
		private void DrawLine(Vector2 start, Vector2 end, float width)
		{
			var direction = end - start;
			var angle = Vector2.Angle(direction, Vector2.right);
			_transform.localEulerAngles = 
				new Vector3(_transform.localEulerAngles.x, _transform.localEulerAngles.y, start.y < end.y ? angle : -angle);
			(_weightText.transform as RectTransform).localEulerAngles = 
				new Vector3(_transform.localEulerAngles.x, _transform.localEulerAngles.y, -_transform.localEulerAngles.z);
			_transform.anchoredPosition = (start + end) / 2;
			_transform.sizeDelta = new Vector2(Vector2.Distance(start, end) + 10, width);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			switch (eventData.button)
			{
				case PointerEventData.InputButton.Right:
					if(Drawn) Graph.DisconnectVertex(StartVertex, EndVertex);
					EdgeDrawer.Instance.CancelDrawing();
					Delete();
					break;
			}
		}

		private void SetWeight(string value)
		{
			Weight = Math.Abs(Convert.ToInt32(value));
		}
	}
}