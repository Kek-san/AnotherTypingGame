using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Action<Enemy> OnEnemySpawned;

    [SerializeField] Transform[] _spawnLocationList ;
    [SerializeField] Transform _enemyPrefab;

    private void Start() {
        StartCoroutine(SpawnRoutine(3));
    }

    private IEnumerator SpawnRoutine(int amountToSpawn, float duration = 0f) {
        int amount = amountToSpawn;
        float time = 0f;
        int index = 0;

        while (amount > 0f) {
            while (time < duration) {
                time += Time.deltaTime;
                yield return null;
            }


            Transform enemyTransform = Instantiate(_enemyPrefab, _spawnLocationList[index].position, Quaternion.identity);
            Enemy enemy = enemyTransform.GetComponent<Enemy>();
            OnEnemySpawned?.Invoke(enemy);
            index++;
            amount--;
            time = 0f;
        }
        
    }
}
