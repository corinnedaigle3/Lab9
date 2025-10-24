# Lab9

## **Save and Load**

• ISaveable.cs is an interface that defines the saved data using an Id. It has a saving class that manages the actual saving and loading of objects.

• TransformerSaver.cs uses the ISaveable interface to retrieve and update the json data of the player and target positions.

• SaveManager.cs implements the saving/loading when the respective keyboard buttons are pressed and destroys the save when it is loaded.

• SaveScore.cs saves and loads the score using a Binary file. 

## **Builder Pattern**


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


## **Observer Pattern**


Subject (Subject.cs)

• Defines a delegate TargetHitEvent and an event OnTargetHit.

• Calls OnTargetHit?.Invoke(points) whenever the target is hit, notifying all subscribers.

Observer (ScoreSystem.cs)

• ScoreSystem subscribes to each spawned target’s OnTargetHit event via SubscribeToTarget().

• The method AddScore(int points) is automatically invoked whenever the event is triggered.

• Updates the internal score variable and the UI (TextMeshProUGUI) only when a target is hit.

<img width="546" height="561" alt="image" src="https://github.com/user-attachments/assets/4fdc28b6-bfc4-4d94-ad2d-9f3e131f44f8" />



## **Object Pool Pattern**

Subject(Bullet.cs)

• Returns the bullet game object back into the pool through the DisableBullet function in the ObjectPooling script when 2 seconds have passed or when the bullet has hit a target

Object Pooler(ObjectPooling.cs)

• Creates a pool that can hold a predetermined number of game objects and instantiates them all on Start

• GetBullet function that sets active a bullet gameobject from the pool and returns it 

• DisableBullet function that sets the active bullet game object to not active and returns it back into the pool

Shooting (ShootBullet.cs)

• Retrieves and sets the transform positions of a bullet game object from the GetBullet function in the ObjectPooling script when the Space bar is pressed

<img width="560" height="283" alt="Screenshot 2025-10-16 193527" src="https://github.com/user-attachments/assets/f4e4368c-791a-4092-a5dc-9851e193d448" />

