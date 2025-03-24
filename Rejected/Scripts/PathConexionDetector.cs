using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class PathConexionDetector : Node
{

  //private Dictionary<string, Dictionary<Marker2D, Path2D>> prove = new();
  //private 

  private struct Route{
    public Route(MarkerPathSwitch startMarker) {
      StartMarker = startMarker;
      StartPath = FinalMarker = null;
      Jumps = 0;
    }

    public MarkerPathSwitch StartMarker {get; set;}
    public MarkerPathSwitch StartPath {get; set;}
    public MarkerPathSwitch FinalMarker {get; set;}
    public int Jumps {get; set;}
  }




    private int nodeCount;


  public override void _Ready() {

    Array<Node> nodes = GetTree().GetNodesInGroup(NameGroups.InterconnectionRoute);

    List<MarkerPathSwitch> marker2Ds = new List<MarkerPathSwitch>();
    foreach(Node node in nodes) {
        marker2Ds.Add((MarkerPathSwitch)node);
    }

    
    Array<Node> uniquePaths = GetTree().GetNodesInGroup(NameGroups.NPCsRoutes);
/*
    bool[][] adjacencyMatrix = new bool[marker2Ds.Count][];

    for(int i = 0; i < marker2Ds.Count; i++) {
      adjacencyMatrix[i] = new bool[uniquePaths.Count];
      for(int j = 0; j < uniquePaths.Count; j++) {
        adjacencyMatrix[i][j] = marker2Ds[i].Intercontections.Contains(uniquePaths[j]);
      }
    }
*/

    
    int vertexCount = marker2Ds.Count;
    int pathCount = uniquePaths.Count;


    int[,] adjacencyMatrix = new int[vertexCount, pathCount];


    for(int i = 0; i < vertexCount; i++) {
      //adjacencyMatrix[i] = new BitArray(pathCount);
      for(int j = 0; j < pathCount; j++) {
        adjacencyMatrix[i, j] = marker2Ds[i].Intercontections.Contains(uniquePaths[j]) ? 1 : 0;
      }
    }

    nodeCount = adjacencyMatrix.GetLength(0);

    List<List<int>> ints = new();

    for(int i = 0; i < vertexCount - 1; i++) {
      for(int j = 1; j < vertexCount; j++) {
        ints.Add(FindShortestPath(i, j, adjacencyMatrix));
      }
    }

    for(int i = 0; i < ints.Count; i++) {
      GD.Print("Camino más corto: " + string.Join(" -> ", ints[i]));
    }



/*

    bool notEndPoint = true;
    int numberRoute = 0, numberJump = 0;
    int auxIndex; 
    List<List<Route>> routes = new();





    for(int i = 0; i < vertexCount; i++) {
      routes.AddRange(Enumerable.Repeat(new List<Route>(), marker2Ds[i].Intercontections.Length));
      for(int j = 0; j < pathCount; j++) {
        if(i != j) notEndPoint = adjacencyMatrix[i][j];
      }

      if(!notEndPoint) {
        notEndPoint = true;
        continue;
      }

      for(int j = 0; j < pathCount; j++) {
        if(adjacencyMatrix[i][j] && i != j) {
          Route route = new Route(marker2Ds[i]);
          //routes[numberRoute].Add();

          auxIndex = searchDirectConecction(adjacencyMatrix[i]);
          numberJump++;

          route.FinalMarker = marker2Ds[auxIndex];
          route.Jumps = numberJump;

          routes[numberRoute].Add(route);

          for(int k = 0; k < pathCount; k++) {

          }
          //marker2Ds[i].ReciveRoutes(marker2Ds[auxIndex], (Path2D) uniquePaths[j]);
          //findRoute(marker2Ds[i]);
          

        }
      }

    }

/*
    List<Path2D> markersPathConected = new();
    
    foreach(MarkerPathSwitch marker in marker2Ds) {
      markersPathConected.AddRange(marker.Intercontections.ToList());
    }
    List<Path2D> uniquePaths = new HashSet<Path2D>(markersPathConected).ToList();

    for(int i = 0; i < marker2Ds.Count; i++) {
      adjacencyMatrix[i] = new bool[uniquePaths.Count];
      for(int j = 0; j < uniquePaths.Count; j++) {
        adjacencyMatrix[i][j] = marker2Ds[i].Intercontections.Contains(uniquePaths[j]);
      }
    }
*/

/*

#region Prints


    List<StringName> nombresNodos = nodes.Select(node => node.Name).ToList();
    List<StringName> nombresMarkers = marker2Ds.Select(marker => marker.Name).ToList();
    List<StringName> nombresUniquePaths = uniquePaths.Select(paths => paths.Name).ToList();


    GD.Print("Nodos: " + string.Join(", ", nombresNodos));

    GD.Print("Markers: " + string.Join(", ", nombresMarkers));
    GD.Print("Caminos Unicos: " + string.Join(", ", nombresUniquePaths));
    string matrizString = "Matriz de Adyacencia:\n";
    for (int i = 0; i < adjacencyMatrix.Length; i++)
    {
        matrizString += "Fila " + i + ": ";
        for(int j = 0; j <pathCount; j++) matrizString += adjacencyMatrix[i][j].ToString() + ", ";
        matrizString +=  "\n";
    }
    GD.Print(matrizString);
#endregion
    */
  }

  private int searchDirectConecction(BitArray bitArray) {
    for(int i = 0; i < bitArray.Length; i++) if(bitArray[i]) return i;
    return -1;
  }


  private void findRoute(MarkerPathSwitch startMarker) {
    Route route = new();
    route.StartMarker = startMarker;

  }




    private List<int> FindShortestPath(int start, int end, int[,] adjacencyMatrix)
    {
        int[] distances = new int[nodeCount];
        bool[] visited = new bool[nodeCount];
        int[] previous = new int[nodeCount];

        for (int i = 0; i < nodeCount; i++)
        {
            distances[i] = int.MaxValue;
            previous[i] = -1;
        }

        distances[start] = 0;
        PriorityQueue<int, int> queue = new PriorityQueue<int, int>();
        queue.Enqueue(0, start);

        while (queue.Count > 0)
        {

           if (!queue.TryDequeue(out int currentNode, out int currentDistance))
             break; // Evita errores si la cola está vacía
            if (visited[currentNode]) continue;
            visited[currentNode] = true;

            if (currentNode == end) break;

            for (int neighbor = 0; neighbor < nodeCount; neighbor++)
            {
                if (adjacencyMatrix[currentNode, neighbor] == 1)
                {
                    int newDist = currentDistance + 1;
                    if (newDist < distances[neighbor])
                    {
                        distances[neighbor] = newDist;
                        previous[neighbor] = currentNode;
                        queue.Enqueue(newDist, neighbor);
                    }
                }
            }
        }

        return ReconstructPath(previous, start, end);
    }

    private List<int> ReconstructPath(int[] previous, int start, int end)
    {
        List<int> path = new List<int>();
        for (int at = end; at != -1; at = previous[at])
        {
            path.Add(at);
        }
        path.Reverse();

        return path[0] == start ? path : new List<int>(); // Si no hay camino, retorna lista vacía
    }



}
