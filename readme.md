## Goal
Implement the Ant colony path optimization algorithm using an agent system. The agent world is a
connected graph.
- One node is the home base, i.e. the anthill.
- The others may have "food" - the amount of food is represented by an integer value. Nodes with
  value 0 have no food, while others have food units in various amounts (i.e. non-zero values).
- The edges of the graph have weights which are initially 0.
- Several ant agents start at the home base and, by default, start searching for food randomly, i.e.
  they randomly move from node to node along the edges of the graph.
- Once an ant has found food (has landed on a non-zero node), it takes away a food unit (the value of
  the node decreases by 1) and:
  o Version 1: returns the food unit to the anthill node
  o Version 2: deposits the food unit on a node half-way between its current node and the anthill
  node.
- In both versions, the ant carrying back a food unit increases the weight of each edge it traverses by
2. Once it deposits the food unit, the ant starts searching again.
- If an ant with no food unit (which is searching) lands on a node with an adjacent non-zero edge, it
  follows that edge and all subsequent connecting edges with non-zero weights, always preferring the
  edges with the highest weight. If it lands on a node with no non-zero edges (other than the one that
  was used to reach the node), it starts randomly searching again. The ant searches until it finds a
  non-zero node, in which case it returns the food item as described above.
- The weights of the nodes decay at a certain time interval (for instance, by -0.1 per second).
- Implement both versions of the ant behavior and compare the two approaches in terms of the time
  taken to collect all food units (time until all nodes end up with zero values). A visual representation of the
  graph illustrating the node and weight values would help analyze the steps of the algorithm.
  The example below illustrates an ant colony graph. The importance of the routes is represented via
  the thickness of the connecting graph and the most important routes are highlighted in green. The individual
  ants are represented through the smaller black dots.

## Points 
-  Agents types
- Agents communication
- Agents behavior
- Graph representation