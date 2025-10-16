# Lab9

Builder Pattern


Subject (Target.cs)

•	Sets the rigidbody velocity to move right * speed from TargetBuilder.cs to the spawned targets in TargetSpawner.cs 1-3

•	Destroys target after 5 seconds for optimization.


Builder (TargetBuilder.cs)

•	TargetBuilder class holds the variables 

•	The Builder class sets values and retuns the builder

•	Build function is where the GameObjects are created


Spawner (TargetSpawn1-3.cs)

•	Spawns’ random targets constructed targets from TargetBuilder.cs at set intervals

•	Sets the variables from TargetBuilder.cs

•	Returns the Build function

<img width="1436" height="970" alt="image" src="https://github.com/user-attachments/assets/bca11439-25e0-4a46-97be-06ab9fa5ba0e" />
