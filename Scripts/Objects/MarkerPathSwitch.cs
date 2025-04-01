using Godot;
using Godot.Collections;
/**
    MarkerPathSwith class to control and create the table of re-sends
        it will act as controller. When a NPC come to a final of a path, this will resend the NPC for another path until his destination
        Extend of a Marker2D
*/
public partial class MarkerPathSwitch : Marker2D
{
    // Indicate the zone or stage where it is
    [Export] private int _zone = 0;
    // What Path2D's I am interconnecting 
    [Export] private Path2D[] _pathInterconectedByMe;
    // What MarkerPathSwitch's I have next to me
    [Export] private MarkerPathSwitch[] _markersAdjacent;
    // Represent the table of resends (The MarkerPathSwith1 is along this Path2D)
    private Dictionary<MarkerPathSwitch, Path2D> _redirectionDictionary = new();


    // Flag to tell every MarkerPathSwitch if it finish to create my initial table of resend
    private static int MarkersFinished = 0;
    // How much MarkerPathsSwith are in that Scene
    private static int TotalMarkers = 0;
    // Object which has all MarkerSignals
    private static MarkerSignals MarkerSignals = new();
    

    /** Ready Method - Runs during the start of a Scene 
            Initialize atributtes, signals and build the initial table resend
    */
    public override void _Ready()
    {
        // Connect the MarkerSignal with the method that it must do
        MarkerSignals.MarkerToCommunicate += SendRoutes;

        // Only the first Marker initialize the TotalMarkers variable
        if(TotalMarkers == 0) TotalMarkers = GetTree().GetNodeCountInGroup(NameGroups.InterconnectionRoute);

        //if(_markersAdjacent.Length == 1) {
        //    
        //    MarkersFinished++;
        //    return;
        //}


        // Build the initial table resend

        MarkerPathSwitch[] markersOfOther;
        for(int i = 0; i < _markersAdjacent.Length; i++) {
            AddRoute(_markersAdjacent[i], _pathInterconectedByMe[i]);
            markersOfOther = _markersAdjacent[i].MarkersAdjacent;
            for(int j = 0; j < markersOfOther.Length; j++) {
                AddRoute(markersOfOther[j], _pathInterconectedByMe[i]);
            }
        }

        // Tell everyone that it finished
        MarkersFinished++;
        // If everyone finished, Emit the signal to communicate his inital table
        if(MarkersFinished == TotalMarkers) {
            MarkerSignals.EmitCommunicateMarkerSignal();
        }

    }

    /** SendRoutes Method - Runs when the CommunicateMarkerSignal has been emited 
            Send the initial table resend to the MarkerPathSwitch adjacent
    */
    private void SendRoutes() {
        foreach(MarkerPathSwitch marker2D in _redirectionDictionary.Keys) {
            if(marker2D.GetDictionarySize() == TotalMarkers - 1) continue;
            marker2D.ReciveRoutes(_redirectionDictionary, this);
        }
    }

    /** ReciveRoutes Method - Runs when another MarkerPathSwitch send his routes
            Recive the another MarkerPathSwitch's routes and add them in his table routes (resends)
        @param directions -> A dictionary containing route connections from the sending MarkerPathSwitch.
        @param markerObject ->  The MarkerPathSwitch that is sending its routes.
    */
    public void ReciveRoutes(Dictionary<MarkerPathSwitch, Path2D> directions, MarkerPathSwitch markerObject) {

        int index = 0;
        for(int i = 0; i < _markersAdjacent.Length; i++) {
            if(_markersAdjacent[i] == markerObject) index = i;
        }
        foreach(MarkerPathSwitch marker in directions.Keys) {
            AddRoute(marker, _pathInterconectedByMe[index]);
        }
        //PrintRoutes();
    }
    
    /** AddRoute Method - Runs when it need to add a route to his table
            Comprobe if that route is not already added and if it is not him
                If both are false add the route
        @param marker2D -> The Marker2D (it works like a Key in the dictionary) that represent a point in the map
        @param path2D ->  The Path2D through which it will be sent will direct the entity if it looks for that point
    */
    private void AddRoute(MarkerPathSwitch marker2D, Path2D path2D) {
        if(!_redirectionDictionary.ContainsKey(marker2D) && marker2D != this) _redirectionDictionary.Add(marker2D, path2D);
    }

    /** ChangeTheNPCRoute Method - Runs when a entity need to continue his walk
            If this MarkerPathSwitch is his destination advised NPC that it do not need walk more
            If not find the next path that the NPC need to follow, change his Path and advice that he has been moved
        @param npcPathFollow -> The NpcPathFollow of the NPC
        @param destination -> The destionation of the NPC
    */
    public void ChangeTheNPCRoute(NpcPathFollow npcPathFollow, MarkerPathSwitch destination) {
        
        if(destination == this) {
            npcPathFollow.EmitInMyDestinationSignal();
            return;
        }
        //PrintRoutes();
        GD.Print("I want to find: " + destination.Name);
        Path2D nextPath = _redirectionDictionary[destination];
        npcPathFollow.EmitOnChangePathSignal(nextPath.ToGlobal(nextPath.Curve.GetPointPosition(nextPath.Curve.PointCount - 1)));
        npcPathFollow.GetParent<Path2D>().RemoveChild(npcPathFollow);
        npcPathFollow.LastPassMarker = this;
        nextPath.AddChild(npcPathFollow);
    }

    private void PrintRoutes() {
        GD.Print($"My dictionary {Name}: ");
        foreach(var node in _redirectionDictionary) {
            GD.Print($"Key: {node.Key.Name} - Value: {node.Value.Name}");
        }
    }

/*
    private void RemoveRouteByKey(MarkerPathSwitch marker) {
        if(_redirectionDictionary.ContainsKey(marker)) _redirectionDictionary.Remove(marker);
    }

    public void EndPointDetected(List<MarkerPathSwitch> interconnectionRoute) {
        for(int i = 0; i < interconnectionRoute.Count; i++) {
            AddRoute(interconnectionRoute[i], _pathInterconectedByMe[0]);
        }
    }

 */
    
    #region Getter and Setters
        // Get - Set the zone value
        public int Zone {
            get => _zone;
            set => _zone = value;
        }

        // Get the Path2D interconnected to this Marker
        public Path2D[] Intercontections {
            get => _pathInterconectedByMe;
        }
        // Get the MarkerPathSwitch interconnected to this Marker
        public MarkerPathSwitch[] MarkersAdjacent {
            get => _markersAdjacent;
        }
        // Get the Size of the table resend
        public int GetDictionarySize() {
            return _redirectionDictionary.Count;
        }
    #endregion
}
