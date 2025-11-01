using UnityEngine;

// The TargetBuilder class is used to construct and spawn Target objects with specific attributes.
// It follows the Builder design pattern for flexible object creation.
public class TargetBuilder
{
    // Target attributes with private setters for controlled access
    public float health { get; private set; }
    public float speed { get; private set; }
    public int pointValue { get; private set; }
    public GameObject prefab { get; private set; }

    // Private constructor ensures this class can only be instantiated through the Builder class
    private TargetBuilder() { }

    // Nested Builder class used to configure and construct a TargetBuilder instance
    public class Builder
    {
        // Holds an instance of TargetBuilder that this Builder configures
        private TargetBuilder targetBuilder = new TargetBuilder();

        // Sets the health value for the target
        public Builder SetHealth(float health)
        {
            targetBuilder.health = health;
            return this;
        }

        // Sets the movement speed for the target
        public Builder SetSpeed(float speed)
        {
            targetBuilder.speed = speed;
            return this;
        }

        // Sets the score value (points) the player receives for destroying this target
        public Builder SetPointValue(int pointValue)
        {
            targetBuilder.pointValue = pointValue;
            return this;
        }

        // Assigns the prefab GameObject that represents this target visually
        public Builder SetPrefab(GameObject prefab)
        {
            targetBuilder.prefab = prefab;
            return this;
        }

        // Instantiates and initializes the final Target GameObject
        public GameObject Build()
        {
            // Ensure a prefab was assigned before instantiation
            if (targetBuilder.prefab == null)
            {
                Debug.LogError("TargetBuilder: Prefab is null!");
                return null;
            }

            // Instantiate the target prefab into the scene
            GameObject target = Object.Instantiate(targetBuilder.prefab);

            // Ensure the Target component exists or add it if missing
            Target targetComponent = target.GetComponent<Target>();
            Subject subject = target.GetComponent<Subject>();
            if (targetComponent == null)
            {
                targetComponent = target.AddComponent<Target>();
            }

            // Assign point value to Subject if it exists
            if (subject != null)
            {
                subject.points = targetBuilder.pointValue;
            }

            // Initialize Target-specific behavior (like speed)
            targetComponent.Initialize(targetBuilder.speed);

            // Assign a unique name and save ID if TransformSaver exists
            var saver = target.GetComponent<TransformSaver>();
            if (saver != null)
            {
                // Give each enemy a unique name
                saver.name = "Enemy_" + System.Guid.NewGuid().ToString();

                // Set the private saveID field via reflection
                var field = typeof(TransformSaver).GetField("saveID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                field.SetValue(saver, saver.name);
            }

            // Return the fully built and configured target
            return target;
        }
    }
}
