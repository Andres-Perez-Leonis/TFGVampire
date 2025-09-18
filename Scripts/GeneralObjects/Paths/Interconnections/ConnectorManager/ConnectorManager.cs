using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class ConnectorManager : Node
{
	private struct GraphEdge
	{
		public int From;
		public int To;
		public Path2D path;
		public GraphEdge(int from, int to, Path2D path)
		{
			From = from;
			To = to;
			this.path = path;
		}
	}

	private struct Graph
	{
		public List<GraphEdge> Edges;
		public Graph()
		{
			Edges = new List<GraphEdge>();
		}
		public void AddEdge(int from, int to, Path2D path)
		{
			Edges.Add(new GraphEdge(from, to, path));
		}
	}

	public override void _Ready()
	{
		CallDeferred("_callDeferredReady");
	}

	private void _callDeferredReady()
	{
		List<MarkerPathSwitch> markers = GetTree().GetNodesInGroup(NameGroups.InterconnectionRoute).OfType<MarkerPathSwitch>().ToList();

		MarkerPathSwitch marker = markers[0];
	}
}
