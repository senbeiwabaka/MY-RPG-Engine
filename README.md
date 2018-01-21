This is my attempt at a 3D engine. At this point in time, until I learn, the GUI on top will only make this engine look like an editor.

The engine can be removed from the project and modified in anyway you see fit. The GUI on top is just you can immediately see what all it can do.

The engine will be using just a list for all the objects until I, or someone joins the project, learn how to implement a scene graph. The choice, when scene graph is there, between a list of objects or scene graph will be yours.

The engine is currently using DirectX 11. The engine will as well be using SharpDX (4.0.1) which is included with the download via nuget.

The engine currently has:
  1. A basic camera that can move position (no rotation yet)
  2. 2D Cube and Triangle
  3. Build component that will generate a exe
    3.a. This includes the saved game file (json)
    3.b. The debug and logging
  4. Add class files
  5. Partial load and create projects
    
The enginge will have (soon TM):
  1. Textured objects
  2. Moveable objects
  3. Compiled/checked class files
  4. Bin folder for debug and release modes
  5. Fully wokring load and create projects

Note: All is subject to change
