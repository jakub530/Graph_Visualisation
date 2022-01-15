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
### General Notes (Draft)
In order to use algorithm click on the button following by some number of nodes.
After correct number of nodes is clicked the algorithm will start
In order to reset algorithm switch modes back and forth
Queue on the right side will display the order of next 5 active nodes along with some properties depending on algorithm
The legend on the bottom displays meaning of each node colour 


### Djikstra
Implementation of [Djikstra](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm) algorithm used to find a shortest path between two nodes. 

In order to visualise it, first click on the "Djikstra" button. Following that click on two nodes of your choice. Those will be "Start" and "End" node respectively. At this point visualisation will start, ending whenever shortest path is found, or alternatively when no path between two nodes can be found.

[![Image from Gyazo](https://i.gyazo.com/8ade518e284889119381460e410be4a8.gif)](https://gyazo.com/8ade518e284889119381460e410be4a8)

### BFS
[![Image from Gyazo](https://i.gyazo.com/f8f2ddcab62ba7feb9d6686edc8179c9.gif)](https://gyazo.com/f8f2ddcab62ba7feb9d6686edc8179c9)

### Cycle Detection
[![Image from Gyazo](https://i.gyazo.com/91b71857d0215d5ed8bcd0b5319f24d3.gif)](https://gyazo.com/91b71857d0215d5ed8bcd0b5319f24d3)

# Technical Documentation

## Adding new algorithms
