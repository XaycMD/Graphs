using System.Collections.Generic;

namespace edu.ua.pavlusyk.masters
{
  public class Vertex
  {
    public int Index { get; set; }
    public List<Vertex> ConnectedTo { get; }

    public Vertex()
    {
      ConnectedTo = new List<Vertex>();
    }
  }
}