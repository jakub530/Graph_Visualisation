# Graph Algorithm Visualisation Project

This repository holds code for Graph Algorithm Visualisation project made using Unity Engine and written in C#. 

The main premise of the project is to easily create, test and visualise functioning of Graph Theory Algorithms. 

Graph creator supports both single and two directional edges. In addition user can create both weighted and unweigthed graphs.

Current version implements 3 different algorithms:
1. **Djikstra** algorithm for finding shortest path between two nodes.
2. **BFS (Breadth-first search)** algorithm for graph treversal.
3. **Cycle detection** algorithm, which as name implies searches for a cycle in algorithm.

The project is currently hosted on [Unity Play](https://play.unity.com/mg/other/my-new-microgame-5350).

# User Instructions
## General Navigation
### UI Control

The application has two different modes - graph creation mode and algorithm visualisation mode. In order to switch between two modes, click on the "Switch mode" button in top left corner.

In the algorithm visualisation mode there are two ribbons visible by default - Queue and Legend. In order to show/hide the ribbon click on the white arrow next to the ribbon.

[![Image from Gyazo](https://i.gyazo.com/b57eb03c97a634c972c2e5bdbb2121da.gif)](https://gyazo.com/b57eb03c97a634c972c2e5bdbb2121da)

### Camera Control
Camera control for the application is done using mouse. In order to zoom in/out use mouse scroll. In order to move the graph click on middle mouse button away from the center of the screen. Camera will follow that direction. 

[![Image from Gyazo](https://i.gyazo.com/5a424df08f89faed7020ba7c607bb48c.gif)](https://gyazo.com/5a424df08f89faed7020ba7c607bb48c)

## Graph Editing
### Adding Nodes
In order to create new nodes, click on the "Add Node" toggle on the left side. Afterwards simply click to create new nodes. After you finish adding desired number of nodes, click on "Add Node" toggle again to disable node addition. 

[![Image from Gyazo](https://i.gyazo.com/14b50b68e0500b7a76a6e8a1ac923703.gif)](https://gyazo.com/14b50b68e0500b7a76a6e8a1ac923703)

### Creating Edges
Edge creation is started by clicking and holding the plus symbol next to the desired node. Afterwards by dragging the mouse onto another node new edge can be created. 

There can exist only single edge between any two nodes. By default weight of all edges is equal to 1, therefore graph is treated as unweighted graph.

[![Image from Gyazo](https://i.gyazo.com/4fffbec8ac47fcad9f8cf16b61671672.gif)](https://gyazo.com/4fffbec8ac47fcad9f8cf16b61671672)

### Deleting Edges
Edge can be deleted by clicking on either end of the edge and moving it away from any nodes. After the mouse button is released edge will be deleted.

[![Image from Gyazo](https://i.gyazo.com/026ff8221f304d9aaed6da042b06e40d.gif)](https://gyazo.com/026ff8221f304d9aaed6da042b06e40d)

### Changing Cost
Cost of edge can be modified by clicking on existing cost and typing new cost. 

Note that while it is possible to enter negative cost, one of the implemented Algorithms (Djikstra Algorithm) will fail to function correctly for graph with negative cycles.

[![Image from Gyazo](https://i.gyazo.com/6fb335ba66ff576ce5b7fd47c503d059.gif)](https://gyazo.com/6fb335ba66ff576ce5b7fd47c503d059)

### Changing Edge Types
By default every edge that is created is a bidirectional edge. In order to change it click on one of the Source/Destinatin/Bidirectional buttons on the left side of the screen.

[![Image from Gyazo](https://i.gyazo.com/a45b3c02d11ebc697775c3b424e727b4.gif)](https://gyazo.com/a45b3c02d11ebc697775c3b424e727b4)

## Using Algorithms
### General Notes 
In order to visualize any algorithm user needs to click on the button corresponding to given algorithm and afterwards select a number of nodes. The number of nodes to select depends on the chosen algorithm. The visualisation will start after last node is selected.

Queue on the right side displays the order of next 5 active nodes along with properties, which are relevant for given algorithm (such as distance in case of shortest path algorithm).

The legend on the bottom explains the meaning behind each node color for a particular algorithm.

### Djikstra
Implementation of [Djikstra](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm) algorithm used to find a shortest path between two nodes. 

The algorithm requires selection of two nodes. Those will be "Start" and "End" node respectively. The visualisation ends whenever shortest path is found, or alternatively when no path between two nodes can be found. If the path is found the nodes, which are part of the path will get highlighted.

[![Image from Gyazo](https://i.gyazo.com/8ade518e284889119381460e410be4a8.gif)](https://gyazo.com/8ade518e284889119381460e410be4a8)

### BFS (Breadth-first search)
Implementation of [BFS](https://en.wikipedia.org/wiki/Breadth-first_search) graph traversal algorithm.

The algorithm requires selection of a single node. Starting at that node visualisation will continue until all nodes connected to the original node have been iterated over.

[![Image from Gyazo](https://i.gyazo.com/f8f2ddcab62ba7feb9d6686edc8179c9.gif)](https://gyazo.com/f8f2ddcab62ba7feb9d6686edc8179c9)

### Cycle Detection
Algorithm used to detect cycles in graph.

It doesn't take any nodes as input. It is based on BFS, and works by attempting to iterate over all nodes. If particular node has been reached twice from two different sources it must mean that cycle has been found. At that point the cycle will be highlighted on the screee. 

[![Image from Gyazo](https://i.gyazo.com/91b71857d0215d5ed8bcd0b5319f24d3.gif)](https://gyazo.com/91b71857d0215d5ed8bcd0b5319f24d3)

# Technical Documentation

## Adding new algorithms

All algorithms inherit from Algorithm class. There are three main functions, which need to be implemented in a new algorithm (presented on the example of simples Algorithm - BFS):

1. [algorithmPreInitialization](https://github.com/jakub530/Graph_Visualisation/blob/main/Assets/Scripts/Algorithms/BFS.cs#L13-L32):
Algorithm Pre-Initialization is main configuration steps. The main things to configure are how many input nodes will the algorithm take and color coding used throughout the algorithm.

2. [algorithmInitialization](https://github.com/jakub530/Graph_Visualisation/blob/main/Assets/Scripts/Algorithms/BFS.cs#L34-L39):
Algorithm Initialization takes input nodes and modifies them, adjusting their cost, presence in the queue etc.

3. [runStep](https://github.com/jakub530/Graph_Visualisation/blob/main/Assets/Scripts/Algorithms/BFS.cs#L41-L67):
RunStep as name indicates executes single step of the algorithm. When the condition is met it sends stop flag to function coordinating step execution. 


