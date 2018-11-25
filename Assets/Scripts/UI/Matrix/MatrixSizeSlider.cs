using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace edu.ua.pavlusyk.masters
{
  public class MatrixSizeSlider : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Editor
    //---------------------------------------------------------------------

    [SerializeField] private Text _size;
    [SerializeField] private AdjacencyMatrixUI _adjacencyMatrix;

    //---------------------------------------------------------------------
    // Internal
    //---------------------------------------------------------------------

    private Slider _slider;

    //---------------------------------------------------------------------
    // Messages
    //---------------------------------------------------------------------

    private void Awake()
    {
      _slider = GetComponent<Slider>();
    }

    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public void OnValueChanged()
    {
      SetText((int) _slider.value);
      _adjacencyMatrix.DrawMatrix((int) _slider.value);
    }

    public void SetText(int size)
    {
      _size.text = size.ToString(CultureInfo.InvariantCulture);
    }
  }
}