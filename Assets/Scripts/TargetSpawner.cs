using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    [Header("Enemy Prefabs (set in Inspector)")]
    public List<GameObject> enemyPrefabs;

    [Header("Spawn Settings")]
    public float spawnInterval; // seconds between spawns
    public Vector2 spawnPosition; // same position for all enemies

    public ScoreSystem scoreSystem;

    void Start()
    {
        StartCoroutine(SpawnEnemiesForever());
    }

    IEnumerator SpawnEnemiesForever()
    {
        while (true)
        {
            if (enemyPrefabs.Count == 0)
            {
                Debug.LogWarning("No enemy prefabs assigned to EnemySpawner!");
                yield break;
            }

            // Choose a random prefab from the list
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];


            // Build enemy using Builder pattern
            GameObject target = new TargetBuilder.Builder()
                .SetHealth(2)
                .SetSpeed(4)
                .SetPointValue(2)
                .SetPrefab(prefab)
                .Build();

            // Spawn at the same fixed position
            target.transform.position = spawnPosition;

            Subject subject = target.GetComponent<Subject>();
            if (subject != null)
            {
                subject.points = 1; // or read from Builder if stored
                scoreSystem.SubscribeToTarget(subject);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}