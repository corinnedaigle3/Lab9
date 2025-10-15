using UnityEngine;
using static UnityEngine.Object;

public class CreateTargetBuilder : ITargetBuilder
{
    private Target target;

    public void CreateTargetBuild()
    {
        target = new GameObject("Target").AddComponent<Target>();
    }

    public void SetSpeed(float speed)
    {
        target.speed = speed;
    }

    public void SetHealth(int health)
    {
        target.health = health;
    }

    public void SetPointValue(int pointValue)
    {
        target.pointValue = pointValue;
    }

    public void SetItem(GameObject itemPrefab)
    {
        if (itemPrefab != null)
        {
            target.item = Instantiate(itemPrefab);
        }
    }

    public Target GetResult()
    {
        return target;
    }
}
