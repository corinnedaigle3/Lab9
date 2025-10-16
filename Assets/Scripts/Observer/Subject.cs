using UnityEngine;

public class Subject: MonoBehaviour
{
    public delegate void TargetHitEvent(int points);

    public event TargetHitEvent OnTargetHit;

    public int points = 1;


    private void Start()
    {
    }

    public void Hit()
    {
        Debug.Log("Target was hit");

        OnTargetHit?.Invoke(points);
    }
}
