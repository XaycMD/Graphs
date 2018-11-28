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
      if (Graph.VertexCount == 0)
      {
        SnackbarError.Instance.Show("Draw graph");
        return;
      }
      
      var path = EulerCircuit.FindEulerCircuit(Graph.Vertices);

      if (path == null)
      {
        SnackbarError.Instance.Show("Euler Path or Circuit not Possible");
        return;
      }
      
      GraphCanvas.Instance.HighlightPath(path);
    }
  }
}