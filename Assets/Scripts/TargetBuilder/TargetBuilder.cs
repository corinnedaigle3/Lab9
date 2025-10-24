using UnityEngine;

public class TargetBuilder
{
    public float health { get; private set; }
    public float speed { get; private set; }
    public int pointValue { get; private set; }
    public GameObject prefab { get; private set; }


    private TargetBuilder() { }

    public class Builder
    {
        private TargetBuilder targetBuilder = new TargetBuilder();

        public Builder SetHealth(float health)
        {
            targetBuilder.health = health;
            return this;
        }

        public Builder SetSpeed(float speed)
        {
            targetBuilder.speed = speed;
            return this;
        }

        public Builder SetPointValue(int pointValue)
        {
            targetBuilder.pointValue = pointValue;
            return this;
        }

        public Builder SetPrefab(GameObject prefab)
        {
            targetBuilder.prefab = prefab;
            return this;
        }


        public GameObject Build()
        {
            if (targetBuilder.prefab == null)
            {
                Debug.LogError("TargetBuilder: Prefab is null!");
                return null;
            }

            GameObject target = Object.Instantiate(targetBuilder.prefab);
            Target targetComponent = target.GetComponent<Target>();
            Subject subject = target.GetComponent<Subject>();
            if (targetComponent == null)
            {
                targetComponent = target.AddComponent<Target>();
            }

            if (subject != null)
            {
                subject.points = targetBuilder.pointValue;
            }

            targetComponent.Initialize(targetBuilder.speed);

            var saver = target.GetComponent<TransformSaver>();
            if (saver != null)
            {
                saver.name = "Enemy_" + System.Guid.NewGuid().ToString();
                // unique saveID
                var field = typeof(TransformSaver).GetField("saveID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                field.SetValue(saver, saver.name);
            }


            return target;
        }


    }
}