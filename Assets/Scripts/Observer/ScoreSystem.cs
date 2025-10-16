using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public Subject target;
    public Observer observer;

    void Start()
    {
        target.OnTargetHit += observer.AddScore;
    }
}
