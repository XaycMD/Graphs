using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public class Algorythms : MonoBehaviour
  {
    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------
    
    public void EulerTour()
    {
      GraphCanvas.Instance.HighlightPath(EulerCircuit.FindEulerCircuit(Graph.Vertices));
    }
  }
}