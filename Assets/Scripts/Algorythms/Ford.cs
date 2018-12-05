using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace edu.ua.pavlusyk.masters
{
  public class Ford
  {
    public Ford(int size)
    {
      NUM_VERTICES = size;
    }

    public Ford(List<Vertex> vertices)
    {
      NUM_VERTICES = vertices.Count;
      c = ListToMatrix(vertices);
    }
    
    private static int[,] ListToMatrix(List<Vertex> vertices)
    {
      int n = vertices.Count;
      int[,] graph = new int[n, n];

      for (int i = 0; i < n; i++)
      {
        for (int j = 0; j < n; j++)
        {
          graph[i, j] = vertices[i].ConnectedTo.ContainsKey(vertices[j])
            ? Math.Abs(vertices[i].ConnectedTo[vertices[j]])
            : 0;
        }
      }

      return graph;
    }

    const int MAX_VERTICES = 100;

    int NUM_VERTICES; // число вершин в графе
    const int INFINITY = 10000; // условное число обозначающее бесконечность

    // f - массив садержащий текушее значение потока
    // f[i][j] - поток текущий от вершины i к j
    public int[,] f = new int[MAX_VERTICES, MAX_VERTICES];

    // с - массив содержащий вместимоти ребер,
    // т.е. c[i][j] - максимальная величину потока способная течь по ребру (i,j)
    public int[,] c = new int[MAX_VERTICES, MAX_VERTICES];

    // набор вспомогательных переменных используемых функцией FindPath - обхода в ширину
    // Flow - значение потока чарез данную вершину на данном шаге поиска
    int[] Flow = new int[MAX_VERTICES];

    // Link используется для нахождения собственно пути
    // Link[i] хранит номер предыдущей вешины на пути i -> исток
    int[] Link = new int[MAX_VERTICES];
    List<int> tmp = new List<int>();
    int[] Queue = new int[MAX_VERTICES]; // очередь
    int QP, QC; // QP - указатель начала очереди и QC - число эл-тов в очереди

    public List<int> GetPath()
    {
      var path = new List<int>();
      
      foreach (var vertex in Link)
      {
        path.Add(vertex);  
      }

      return path;
    }

    public int FindPath(int source, int target) // source - исток, target - сток
    {
      QP = 0;
      QC = 1;
      Queue[0] = source;
      Link[target] = -1; // особая метка для стока
      int CurVertex;
      Array.Clear(Flow, 0, Flow.Length); // в начале из всех вершин кроме истока течет 0
      Flow[source] = INFINITY; // а из истока может вытечь сколько угодно
      
      while (Link[target] == -1 && QP < QC)
      {
        // смотрим какие вершины могут быть достигнуты из начала очереди
        CurVertex = Queue[QP];
        for (int i = 0; i < NUM_VERTICES; i++)
          // проверяем можем ли мы пустить поток по ребру (CurVertex,i):
          if ((c[CurVertex, i] - f[CurVertex, i]) > 0 && Flow[i] == 0)
          {
            //  if (f[CurVertex, i] + f[Link[i - 1], CurVertex] > c[Link[i - 1], CurVertex])
            {
              // если можем, то добавляем i в конец очереди
              Queue[QC] = i;
              QC++;
              Link[i] = CurVertex; // указываем, что в i добрались из CurVertex
              // и находим значение потока текущее через вершину i

              if ((c[CurVertex, i] - f[CurVertex, i] < Flow[CurVertex]))
                Flow[i] = c[CurVertex, i];
              else
                Flow[i] = Flow[CurVertex];
            }
          }

        QP++; // прерходим к следующей в очереди вершине
      }

      // закончив поиск пути
      if (Link[target] == -1) return 0; // мы или не находим путь и выходим
      // или находим:
      // тогда Flow[target] будет равен потоку который "дотек" по данному пути из истока в сток
      // тогда изменяем значения массива f для  данного пути на величину Flow[target]
      CurVertex = target;
      while (CurVertex != source) // путь из стока в исток мы восстанавливаем с помощбю массива Link
      {
        //if ((f[Link[CurVertex], CurVertex] + Flow[target]) > c[Link[CurVertex], CurVertex])
        {
          //f[CurVertex,Link[CurVertex]] -= Flow[target];
          // if (f[Link[CurVertex], CurVertex] + Flow[target] <= c[Link[CurVertex], CurVertex])
          // {
          f[Link[CurVertex], CurVertex] += Flow[target];
          // }
          CurVertex = Link[CurVertex];
        }
      }

      return Flow[target]; // Возвращаем значение потока которое мы еще смогли "пустить" по графу
    }

    public int MaxFlow(int source, int target) // source - исток, target - сток
    {
      // инициализируем переменные:
      //memset(f, 0, sizeof(int) * MAX_VERTICES * MAX_VERTICES); // по графу ничего не течет
      Array.Clear(f, 0, f.Length);
      int MaxFlow = 0; // начальное значение потока
      int AddFlow;
      do
      {
        // каждую итерацию ищем какй-либо простой путь из истока в сток
        // и какой еще поток мажет быть пущен по этому пути
        AddFlow = FindPath(source, target - 1);
        MaxFlow += AddFlow;
      } while (AddFlow > 0); // повторяем цикл пока поток увеличивается

      return MaxFlow;
    }
  }
}