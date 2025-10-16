using UnityEngine;
using static Observer;

public class Subject: MonoBehaviour
{
    public delegate void TargetHitEvent(int points);

    public event TargetHitEvent OnTargetHit;

    private int points = 1;

    public void Hit()
    {
        Debug.Log("Target was hit");

        OnTargetHit?.Invoke(points);
    }
}
