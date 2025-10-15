using UnityEngine;

public class TargetEx : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetBuilder movingTarget = new TargetBuilder.Builder()
            .SetHealth(100f)
            .SetSpeed(5)
            .SetPointValue(10)
            .SetBody(GameObject.CreatePrimitive(PrimitiveType.Cube))
            .Build();

        TargetBuilder movingTargetTwo = new TargetBuilder.Builder()
            .SetHealth(100f)
            .SetSpeed(5)
            .SetPointValue(10)
            .SetBody(GameObject.CreatePrimitive(PrimitiveType.Sphere))
            .Build();

        TargetBuilder movingTargetThree = new TargetBuilder.Builder()
            .SetHealth(100f)
            .SetSpeed(5)
            .SetPointValue(10)
            .SetBody(GameObject.CreatePrimitive(PrimitiveType.Cylinder))
            .Build();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
