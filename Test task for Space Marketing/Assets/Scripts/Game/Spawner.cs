using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform obstaclesContainer, coinsContainer;
    [SerializeField] GameObject obstaclePrefab, coinPrefab;

    private float _spawnPosZ = 200f;
    private float _spawnRangeX = 6f;

    private float _spawnObstacleDelay = 1.5f;
    private float _spawnCoinDelay = 2f;

    private float _spawnDelayModifier = 0.015f;
    private float _updateSpawnDelayTime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnObstaclesRoutine());
        StartCoroutine(SpawnCoinsRoutine());
        StartCoroutine(SpawnDelayRoutine());
    }

    private IEnumerator SpawnObstaclesRoutine()
    {
        while(!GameManager.isGameOver)
        {
            yield return new WaitWhile(() => GameManager.isGamePaused);
            yield return new WaitForSeconds(_spawnObstacleDelay);
            var obstacle = Instantiate(obstaclePrefab, GetRandomSpawnPos(obstaclePrefab), obstaclePrefab.transform.rotation);
            obstacle.transform.SetParent(obstaclesContainer, true);
        }
    }

    private IEnumerator SpawnCoinsRoutine()
    {
        while (!GameManager.isGameOver)
        {
            yield return new WaitWhile(() => GameManager.isGamePaused);
            yield return new WaitForSeconds(_spawnCoinDelay);
            var coin = Instantiate(coinPrefab, GetRandomSpawnPos(coinPrefab), coinPrefab.transform.rotation);
            coin.transform.SetParent(coinsContainer, true);
        }
    }

    private IEnumerator SpawnDelayRoutine()
    {
        while (!GameManager.isGameOver)
        {
            yield return new WaitWhile(() => GameManager.isGamePaused);
            yield return new WaitForSeconds(_updateSpawnDelayTime);
            
            _spawnObstacleDelay *= 1 - _spawnDelayModifier;
            _spawnCoinDelay *= 1 - _spawnDelayModifier;
        }
    }

    private Vector3 GetRandomSpawnPos(GameObject obj)
    {
        return new Vector3(Random.Range(-_spawnRangeX, _spawnRangeX), obj.transform.position.y, _spawnPosZ);
    }
}
