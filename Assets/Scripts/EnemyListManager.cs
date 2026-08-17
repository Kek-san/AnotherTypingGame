using System.Collections.Generic;
using UnityEngine;

public class EnemyListManager : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    private List<Enemy> _enemyList = new List<Enemy>();



    private void Start() {
        _enemySpawner.OnEnemySpawned += AddToList;
    }


    private void AddToList(Enemy enemy) {
        if (_enemyList.Contains(enemy)) return;
        _enemyList.Add(enemy);
        enemy.OnEnemyDies += RemoveFromList;
    }

    private void RemoveFromList(Enemy enemy) {
        if (!_enemyList.Contains(enemy)) return;
        _enemyList.Remove(enemy);
        enemy.OnEnemyDies -= RemoveFromList;
    }
}
