using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    [Header("Spawn Rate Settings")]
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
        }
    }

    private void Spawn()
    {
        float randomValue = Random.value;

        foreach (SpawnableObject obj in objects)
        {
            if (randomValue < obj.spawnChance)
            {
                GameObject instance = Instantiate(obj.prefab);
                instance.transform.position = transform.position;
                break;
            }
            randomValue -= obj.spawnChance;
        }
    }
}
