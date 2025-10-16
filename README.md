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


Observer Pattern

Subject (Subject.cs)

•	Defines a delegate TargetHitEvent and an event OnTargetHit

•	Calls OnTargetHit?.Invoke(points) when it is hit

Observer (ScoreSystem.cs)
•	ScoreSystem.cs subscribes to each spawned target’s OnTargetHit event.

•	The method AddScore(int points) is called automatically whenever the event is triggered

•	Updates the score variable and the UI (TextMeshProUGUI) only when the event occurs


Object Pool Pattern

Subject(Bullet.cs)
• Returns the bullet game object back into the pool through the DisableBullet function in the ObjectPooling script when 2 seconds have passed or when the bullet has hit a target

Object Pooler(ObjectPooling.cs)
• Creates a pool that can hold a predetermined number of game objects and instantiates them all on Start
• GetBullet function that sets active a bullet gameobject from the pool and returns it 
• DisableBullet function that sets the active bullet game object to not active and returns it back into the pool

Shooting (ShootBullet.cs)
• Retrieves and sets the transform positions of a bullet game object from the GetBullet function in the ObjectPooling script when the Space bar is pressed


