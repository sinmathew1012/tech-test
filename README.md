# tech-test

<h2>Build</h2>
Play the game using build hosted in Github page [here](https://sinmathew1012.github.io/tech-test-bulid/)
<br>
<h2>How To Play</h2>
Wide position - Idle State <br>
<img width="800" alt="Screenshot 2023-05-13 at 11 15 21 PM" src="https://github.com/sinmathew1012/tech-test/blob/main/readme_ref/Screenshot%202023-05-13%20at%2011.15.21%20PM.png"><br>

Zoom-in position - Modification State<br>
<img width="800" alt="Screenshot 2023-05-13 at 11 21 38 PM" src="https://github.com/sinmathew1012/tech-test/blob/main/readme_ref/Screenshot%202023-05-13%20at%2011.21.38%20PM.png"><br>


<h2>Level editing</h2>
<img width="800" alt="Screenshot 2023-05-13 at 10 56 35 PM" src="https://github.com/sinmathew1012/tech-test/blob/main/readme_ref/Screenshot%202023-05-13%20at%2010.56.35%20PM.png"></img>

Artist define material and mesh options here in the level scene. 
  - Default - first loaded together with the level. 
  - Additional - options for player to choose to change in runtime.
  - MeshIcon - 2D sprite display in UI panel, represent the mesh options.
Note: Mesh & MeshIcon need to be in same order.

# Time log
————————————Setup———————————— <br>
WebGL build with empty scene - 30mins <br>
WebGL build test on iPhone & Android - 4hour <br>
 <br>
————————————Function———————————— <br>
Switch level - 10 mins <br>
Switch mesh and mat - 30 mins <br>
Save/load sphere’s states = 2hour <br>
Default mat & mesh - 1 hour <br>
Camera rotation control (dropped) - 2 hour <br>
Raycast sphere selector - 2 hour <br>
 <br>
————————————UIUX———————————— <br>
UI state - 30mins <br>
UI for mat & mesh options - 1hours <br>
UI for switch level - 1hour <br>
Switch mesh & mat while selecting sphere - 1hour <br>
 <br>
Select sphere and move camera to focus - 30mins <br>
Camera angles setup - 30 mins <br>
Nicer 3D scene - 2hour <br>
 <br>
——————————Performance———————————— <br>
Optimize the performance  <br>
LOD - 20mins <br>
Static-batch - 10mins <br>
Baked Lightmap (failed) - 2 hours  <br>
Garbage collect, reduce memory usage - 30mins <br>
 <br>
Reduction of packaged size to minimum <br>
Texture compression (DXT is the best) - 1 hour <br>
Remove TMP textures, use smaller size graphics - 1hours <br>
 <br>
——————————Maintains———————————— <br>
Cleanup and comments in code - 4 hours <br>

