
## Scenes
### UILineRendererTestingScene
This Scene is created, maintained and used to testify a line drawing on a canvas which ignores the hierarchy of the transform it belongs.

### MeshVertexTestingScene
This Scene is created as a sub-testing part of earlier Scene: **UILineRendererTestingScene**; to find a way to get a vertex on a canvas ignoring the children's hierarchical transform effects.

### NodeExecutionType1TestingScene
This Scene is a very first scene created in this project, and its content is briefly as follows: **Start Node** sends a tick to the **End Node**, then the **End Node** prints a line in the console.

The approach used in this scene about nodes are not being used now, but its visualizing method is still effective, so it is left still.

### NodeExecutionType2TestingScene
This Scene contains the latest version of Node execution approach.

The approach is:
1. **NodeGraph**
	It generates ticks in update scope, and sends them to a root node, normally the **EndNode**. (Tick can be 1~infinity per second, if it techically fits).
	
2. **Node**
It returns a result when it receives a tick. It performs a calculation if necessary.
- Any node that performs a calculation will be called *Processor Node*
	- *Processor Node* will do:
		1. loops through its children
		2. triggers Tick() of its children to receive their **Result**
		3. unpacks children's **Result**
		4. calculate children's **Result** accordingly
		5. and finally saves the outcome as its **Result**.
- This script is consisted of:
- **Node**
	- All the functions mentioned above are all implemented in this class.
- **Node&lt;T&gt; : Node**
	- Performs initialization of **Result&lt;T&gt;** in its generic type T, as this type T is unkown.

3. **Result**
- A container of a value that a node is supposed to return.
- This script is consisted of:
	- **Result**
		This non-generic version of **Result&lt;T&gt;** only exists so that the class **Node** can hold a field **result** without knowing what its type will be.
	- **Result&lt;T&gt;**
		It contains a field named "Value" with a type of T, and its getter & setter methods.

Ideally the **Result** is better to be a struct. as its value shouldn't be affected by its parent when it gets detached, however, *Processor Nodes* like **AddNode** all keeping their own instance of **Result** even before its operation, and they are internally unpacking the received **Result**

4. **EndNode**
(This component is sample only!) It receives the very first tick generated from **NodeGraph** and it hands it over to its adjacent node.
Currently, there can be only one adjacent node.

5. **AddNode**
	One of *Processor Node*
		- adds two values from its children, set the result as its **Result** and return it to its parent over return value of Tick().

6. **SingleValueNode**
	It returns its float value when it receives a tick

7. **EchoNode**
	When it receives a tick, it will:
	1. take a result of its child
	2. set it as its own return value
	3. Debug.Log() a value extracted from it.
	- This script is consisted of:
		- enum **EchoType** 
			- it eases to pinpoint what type the result is holding inside.
		- public EchoNode OfType(EchoType type)
			- it eases a type definition of children this node should unpack being assigned when AddComponent&lt;EchoNode&gt;() is done.
		- Unlike **AddNode**, **EchoNode** extends from a class **Node**, but not **Node&lt;T&gt;**

### JsonParsingTestScene
This scene tests a recipe parsing.
- **JSONParser&lt;T&gt;** receives a path and a data so that it can read and write what is requested.
	- It does not care about what data is handed, or where it is being read or saved.
- DebugIngredientParseRequester is an actual testing script.