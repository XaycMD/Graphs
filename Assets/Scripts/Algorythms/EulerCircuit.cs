using System;
using System.Collections.Generic;
using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public static class EulerCircuit
  {
    private static Stack<int> tempPath = new Stack<int>();
    private static List<int> finalPath = new List<int>(); //to store the final path
    private static int[] nodeList; //to store the nodes
    private static bool[,] GraphMatrix; //to store the edge representation of Graph

    private static int total, count; //total->total no of nodes, count->no of even degree node

    //To Get the all input from user
    private static void GetInput(List<Vertex> vertices)
    {
      //Get the number of nodes in a Graph
      total = vertices.Count;
      GraphMatrix = new bool[total, total];
      nodeList = new int[total];

      //To get the node/vertices to nodeList array
      for (int i = 0; i < total; i++)
      {
        nodeList[i] = vertices[i].Index;
      }

      //To get the edge details in a Graph
      for (int i = 0; i < total; i++)
      {
        for (int j = 0; j < total; j++)
        {
          GraphMatrix[i, j] = Graph.VertexConnected(i, j);
        }
      }
    }

    //To get the number of edges connected to vertex at index i of nodeList array
    private static int GetDegree(int i)
    {
      int j, deg = 0;
      for (j = 0; j < total; j++)
      {
        if (GraphMatrix[i, j]) deg++;
      }

      return deg;
    }
    //To assign the root of the graph
    //Condition 1: If all Nodes have even degree, there should be a euler Circuit/Cycle
    //We can start path from any node
    //Condition 2: If exactly 2 nodes have odd degree, there should be euler path.
    //We must start from node which has odd degree
    //Condition 3: If more than 2 nodes or exactly one node have odd degree, 
    //euler path/circuit not possible.

    //findRoot() will return 0 if euler path/circuit not possible
    //otherwise it will return array index of any node as root
    private static int FindRoot()
    {
      int root = 1; //Assume root as 1
      count = 0;
      for (int i = 0; i < total; i++)
      {
        if (GetDegree(i) % 2 != 0)
        {
          count++;
          root = i; //Store the node which has odd degree to root variable
        }
      }

      //If count is not exactly 2 then euler path/circuit not possible so return 0
      if (count != 0 && count != 2)
      {
        return 0;
      }
      else return root; // if exactly 2 nodes have odd degree, 

      //it will return one of those node as root otherwise return 1 as root  as assumed
    }

    //To get the current index of node in the array nodeList[] of nodes
    private static int GetIndex(char c)
    {
      int index = 0;
      while (c != nodeList[index])
        index++;
      return index;
    }

    //To check weather all adjecent vertices/nodes are visited or not

    private static Boolean AllVisited(int node)
    {
      for (int l = 0; l < total; l++)
      {
        if (GraphMatrix[node, l])
          return false;
      }

      return true;
    }

    //To find the Euler circuit/path and store it in finalPath arrayList
    private static void FindEuler(int root)
    {
      int ind;
      tempPath.Clear();
      //push root into the stack
      tempPath.Push(nodeList[root]);
      while (tempPath.Count != 0) //until Stack going to empty
      {
        //get the array index of top of the stack
        ind = GetIndex((char) tempPath.Peek());
        if (AllVisited(ind))
        {
          //If all adjacent nodes are already visited
          //pop element from stack and store it in finalpath arrayList
          finalPath.Add(tempPath.Pop());
        }
        else
        {
          //If any unvisited node available push that node into stack
          //mark that edge as already visited by marking 'n' in GraphMatrix[][]
          //break the iteration
          for (int j = 0; j < total; j++)
          {
            if (GraphMatrix[ind, j])
            {
              GraphMatrix[ind, j] = false;
              GraphMatrix[j, ind] = false;
              tempPath.Push(nodeList[j]);
              break;
            }
          }
        }
      }
    }

    //THis is the Main Program
    public static List<Vertex> FindEulerCircuit(List<Vertex> vertices)
    {
      //Get the Graph representation from user
      GetInput(vertices);
      //Decide the root
      int root = FindRoot();
      //findRoot() will return 0 if euler path/circuit not possible
      //otherwise it will return array index of any node as root
      if (root != 0)
      {
        if (count != 0) Console.WriteLine("Available Euler Path is");
        else Console.WriteLine("Available Euler circuit is");
        //Find the Euler circuit
        FindEuler(root);
        var tour = new List<Vertex>();

        foreach (var index in finalPath)
        {
          tour.Add(Graph.GetVertex(index));
        }

        return tour;
      }
      else
      {
        Debug.LogError("Euler Path or Circuit not Possible");
        return null;
      }
    }
  }
}