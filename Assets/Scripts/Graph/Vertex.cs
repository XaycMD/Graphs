using System.Collections.Generic;

namespace edu.ua.pavlusyk.masters
{
  public class Vertex
  {
    public int Index { get; set; }
    public Dictionary<Vertex, int> ConnectedTo { get; }

    public Vertex()
    {
      ConnectedTo = new Dictionary<Vertex, int>();
    }
  }
}