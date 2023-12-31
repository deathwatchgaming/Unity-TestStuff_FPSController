Create Simple Testing Plane, Testing Terrain & Tester FPSController:
____________________________________________________________________

TestStuff FPS Controller: "TS_FPSController"


Description:
------------

Simple Test Controller for Testing Stuff.


Setup Instructions:
-------------------

* Simply follow the simple steps found below.

Note: if you already have some sort of a test terrain in place, well, 
then you can simple skip the steps: #1, 2 & 3 and go directly to #4.

____________________________________________________________________

Step 1: * Added for Nicer Organization
____________________________________________________________________

Create Empty: "Environment"

Position: X: 0 Y: 0 Z: 0
Rotation: X: 0 Y: 0 Z: 0
Scale:    X: 1 Y: 1 Z: 1

First since this is just a "holder" added for better organization, 
let us go ahead & drag the "Directional Light" into "Environment".

* also if you wanted you could add a "dir" called Environment and opt 
  to place your TerrainData inside that in its own Dir "TerrainData",
  and as such any other Environment stuff respectively in as desired
  the "Environment" dir. Just a suggestion.

Also: We don't need the "Main Camera" in Sample Scene, so..., 
we can go ahead and delete it.

____________________________________________________________________

Step 2:
____________________________________________________________________

Create New 3D Object: "Plane"

Position: X: 0 Y: 0 Z: 0
Rotation: X: 0 Y: 0 Z: 0
Scale:    X: 10 Y: 0 Z: 10

Rename to: "Terrain Plane"

We can now opt to clean up for better organization, simply drag the:
"Terrain Plane" into the holder "Environment" we added for just this 
exact purpose!

____________________________________________________________________

Step 3:
____________________________________________________________________

Create New 3D Object: "Terrain"

Modify via:

"Terrain" Inspector: Terrain Settings

Mesh Resolution (On Terrain Data):

Terrain Width: 100
Terrain Length: 100

Position: X: -50 Y: 0.01 Z: -50
Rotation: X: 0 Y: 0 Z: 0
Scale:    X: 1 Y: 1 Z: 1

We can now opt to clean up for better organization, simply drag the:
"Terrain" into the holder "Environment" we added for just this 
exact purpose!

____________________________________________________________________

Step 4:
____________________________________________________________________

Create Empty Game Object: "FPSController" And Add Tag: "Player"

Position: X: 0 Y: 0.01 Z: 0
Rotation: X: 0 Y: 0 Z: 0
Scale:    X: 1 Y: 1 Z: 1

____________________________________________________________________

Step 5:
____________________________________________________________________

In "FPSController" Create: 3D Object "Capsule" And Rename To: 

"Player Capsule"

Remove "Capsule Collider" from: "Player Capsule"

Position: X: 0 Y: 1 Z: 0
Rotation: X: 0 Y: 0 Z: 0
Scale:    X: 1 Y: 1 Z: 1

Add Material: "capsule_material.mat"

____________________________________________________________________

Step 6:
____________________________________________________________________

In "FPSController" Create: "Camera" And Rename To: 

"Player Camera"

Position: X: 0 Y: 1.6 Z: 0
Rotation: X: 0 Y: 0 Z: 0
Scale:    X: 1 Y: 1 Z: 1

____________________________________________________________________

Step 7:
____________________________________________________________________

Now Attach The Component / Script To: 

"FPSController" 

Script: "TS_FPSController.cs"

Modify In "FPSController" Inspector, The Component: 
"TS_FPS Controller (Script)" -> Player Camera: "Player Camera"


Then also modify settings for footstep sounds:

Modify In "FPSController" Inspector:

Walking Steps Audio:

Footstep Sounds:
Element 0: Footstep01
Element 1: Footstep02
Element 2: Footstep03
Element 3: Footstep04

Min Time Between Footsteps: 0.3
Max Time Between Footsteps: 0.6
Footstep Audio Source: FPSController (Audio Source)
Time Since Last Footstep: 0

Running Steps Audio:

Sprintstep Sounds:
Element 0: Footstep01
Element 1: Footstep02
Element 2: Footstep03
Element 3: Footstep04

Min Time Between Sprintsteps: 0.2
Max Time Between Sprintsteps: 0.4
Sprintstep Audio Source: FPSController (Audio Source)
Time Since Last Sprintstep: 0

Jumping Steps Audio:

Jumpstep Audio Source: FPSController (Audio Source)
Jump Sound: Jump
Landing Sound: Land


____________________________________________________________________

Step 8:
____________________________________________________________________

Now Attach The TS_FPSHeadBobber, Component / Script To: 

 In "FPSController" -> "Player Camera" 

 Script: "TS_FPSHeadBobber.cs"

 Next, Modify In: "FPSController" -> Player Camera Inspector, 
 The Component: 

"TS_FPS HeadBobber (Script)"

Script: TS_FPSHeadBobber
Walking Bobbing Speed: 14
Bobbing Amount: 0.05
Controller: FPSController (TS_FPS Controller)


----------------------------------------------------------------------

Note: on Wall Jumping 

----------------------------------------------------------------------

* You must add a new layer named "Wall" for any such objects that you 
  desire to allow being able to be wall-jumped off of.


----------------------------------------------------------------------

Final Note:

----------------------------------------------------------------------

* That is it, simple enough, right?! So now you have a very simple test 
  area and tester controller so that you can test whatever.

  Best of Luck!

