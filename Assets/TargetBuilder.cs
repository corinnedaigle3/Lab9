using UnityEngine;

public class TargetBuilder : MonoBehaviour
{
    public float health { get; private set; }
    public int speed { get; private set; }
    public int pointValue { get; private set; }
    public GameObject body { get; private set; }

    private TargetBuilder()
    {

    }

    public class Builder
    {
        private TargetBuilder targetBuilder = new TargetBuilder();

        public Builder SetHealth(float health)
        {
            targetBuilder.health = health;
            return this;
        }

        public Builder SetSpeed(int speed)
        {
            targetBuilder.speed = speed;
            return this;
        }

        public Builder SetPointValue(int pointValue)
        {
            targetBuilder.pointValue = pointValue;
            return this;
        }

        public Builder SetBody(GameObject body)
        {
            targetBuilder.body = body;
            return this;
        }

        public TargetBuilder Build()
        {

            return targetBuilder;
        }
    }
}
