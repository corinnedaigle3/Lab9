using UnityEngine;

public interface ITargetBuilder
{
    void SetSpeed(float speed);
    void SetHealth(int health);
    void SetPointValue(int pointValue);
    void SetItem(GameObject itemPrefab);
    Target GetResult();

}
