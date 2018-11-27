using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
	public class EdgeUI : MonoBehaviour
	{
		//---------------------------------------------------------------------
		// Internal
		//---------------------------------------------------------------------

		private RectTransform _transform;
		private bool _drawn;
		
		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------

		public RectTransform Start { get; set; }
		public RectTransform End { get; set; }
		public float Width { get; set; }

		//---------------------------------------------------------------------
		// Messages
		//---------------------------------------------------------------------

		private void Awake()
		{
			_transform = transform as RectTransform;
		}

		private void Update()
		{
			if(!_drawn) return;

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
			_drawn = true;
		}

		public void Delete()
		{
			Destroy(gameObject);
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
			_transform.anchoredPosition = (start + end) / 2;
			_transform.sizeDelta = new Vector2(Vector2.Distance(start, end), width);
		}
	}
}