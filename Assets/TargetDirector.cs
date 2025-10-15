using UnityEngine;

public class TargetDirector : MonoBehaviour
{
    public Target ConstructTargetOne(ITargetBuilder builder, GameObject itemOnePrefab)
    {
        builder.SetPointValue(2);
        builder.SetHealth(1);
        builder.SetSpeed(1.0f);
        builder.SetItem(itemOnePrefab);
        return builder.GetResult();
    }

    public Target ConstructTargetTwo(ITargetBuilder builder, GameObject itemTwoPrefab)
    {
        builder.SetPointValue(5);
        builder.SetHealth(2);
        builder.SetSpeed(2.0f);
        builder.SetItem(itemTwoPrefab);
        return builder.GetResult();
    }

    public Target ConstructTargetThree(ITargetBuilder builder, GameObject itemThreePrefab)
    {
        builder.SetPointValue(10);
        builder.SetHealth(3);
        builder.SetSpeed(1.0f);
        builder.SetItem(itemThreePrefab);
        return builder.GetResult();
    }
}