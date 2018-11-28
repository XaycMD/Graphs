using System;
using System.Collections.Generic;
using System.Linq;

namespace edu.ua.pavlusyk.masters
{
  public static class Graph
  {
    //---------------------------------------------------------------------
    // Properties
    //---------------------------------------------------------------------
    
    public static int VertexCount { get; private set; }
    public static bool Oriented { get; set; }
    public static List<Vertex> Vertices { get; set; }
    
    //---------------------------------------------------------------------
    // Public
    //---------------------------------------------------------------------

    public static void AddVertex()
    {
      Vertices.Add(new Vertex
      {
        Index = VertexCount
      });

      VertexCount++;
    }

    public static void DeleteVertex(int index)
    {
      Vertices.Remove(GetVertex(index));
    }

    public static void ConnectVertex(int i, int j, int weight)
    {
      GetVertex(i).ConnectedTo.Add(GetVertex(j), weight);
    }

    public static void Reset()
    {
      VertexCount = 0;
      Oriented = false;
      Vertices = new List<Vertex>();
    }
    
    //---------------------------------------------------------------------
    // Helpers
    //---------------------------------------------------------------------

    private static bool Exist(int index)
    {
      return Vertices.Any(x => x.Index == index);
    }

    private static Vertex GetVertex(int index)
    {
      if(Exist(index)) return Vertices.First(x => x.Index == index);
      
      throw new Exception("There is no vertex with index [" + index + "]");
    }
  }
}