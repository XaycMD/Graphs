using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class IncidenceMatrixUI : DrawableMatrix
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private int _itemSize;
    [SerializeField] private IncidenceToggle _togglePrefab;
    [SerializeField] private Text _textPrefab;
    [SerializeField] private int _defaultItemsCount = 2;

    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private RectTransform _transform;
    private IncidenceToggle[,] _toggles;
    
    //---------------------------------------------------------------------
    // Properties
    //---------------------------------------------------------------------
    
    public int[,] Matrix { get; set; }

    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _transform = transform as RectTransform;
    }

    private void Start()
    {
      DrawMatrix(_defaultItemsCount);
    }

    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public override void DrawMatrix(int size)
    {
      Clear();
      SetMatrix(size);
      SetSize(size);
      DrawText(size);
      DrawToggles(size);
    }
    
    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------

    private void OnToggleClick(int i, int j, int value)
    {
      Matrix[i, j] = value;
      Debug.Log("Matrix[" + i + ", " + j + "] = " + Matrix[i, j]);
    }
    
    private void Clear()
    {
      foreach (Transform item in transform)
      {
        Destroy(item.gameObject);
      }
    }
    
    private void SetMatrix(int size)
    {
      Matrix = new int[size, size];
    }
    
    private void SetSize(int itemsCount)
    {
      var size = _itemSize * (itemsCount + 1);
      _transform.sizeDelta = new Vector2(size, size);
    }

    private void DrawText(int itemsCount)
    {
      var position = _itemSize;

      for (var i = 0; i < itemsCount; i++)
      {
        InstantiateText(new Vector2(position + i * _itemSize, 0), i);
        InstantiateText(new Vector2(0, -(position + i * _itemSize)), i);
      }
    }

    private void InstantiateText(Vector2 position, int number)
    {
      var text = Instantiate(_textPrefab, _transform);
      text.GetComponent<RectTransform>().anchoredPosition = position;
      text.text = number.ToString();
    }

    private void DrawToggles(int itemsCount)
    {
      var position = _itemSize;
      _toggles = new IncidenceToggle[itemsCount, itemsCount];
      
      for (var i = 0; i < itemsCount; i++)
      {
        for (var j = 0; j < itemsCount; j++)
        {
          _toggles[i, j] = InstantiateToggle(new Vector2(position + i * _itemSize, -(position + j * _itemSize)), 0);
          _toggles[i, j].Index = new Vector2(i, j);
          _toggles[i, j].OnValueChanged = OnToggleClick;
        }
      }
    }

    private IncidenceToggle InstantiateToggle(Vector2 position, int value)
    {
      var toggle = Instantiate(_togglePrefab, _transform);
      toggle.GetComponent<RectTransform>().anchoredPosition = position;
      toggle.Value = value;

      return toggle;
    }
  }
}