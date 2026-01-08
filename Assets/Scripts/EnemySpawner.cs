using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _groundTiles;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private float _spawnCooldownReductionMultiplier;
    private float _currentCooldown;
    private List<Vector3> _spawnPositions = new List<Vector3>();

    private void Start()
    {
        SetEnemySpawnPositions();
        InvokeRepeating(nameof(HandleGameDifficultyIncrease), 1f, 1f);
    }
    private void Update()
    {
        HandleEnemySpawning();
    }

    private void HandleEnemySpawning()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown > Time.time)
        {
            return;
        }

        _currentCooldown = Time.time + _spawnCooldown;
        SpawnEnemyToRandomLocation();
    }

    private void SpawnEnemyToRandomLocation()
    {
        Instantiate(_enemyPrefab, GetRandomPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        return _spawnPositions[Random.Range(0, _spawnPositions.Count)];
    }

    private void SetEnemySpawnPositions()
    {
        foreach (Vector3Int position in _groundTiles.cellBounds.allPositionsWithin)
        {
            if (_groundTiles.HasTile(position))
            {
                _spawnPositions.Add(_groundTiles.GetCellCenterWorld(position));
            }
        }
    }

    private void HandleGameDifficultyIncrease()
    {
        _spawnCooldown *= _spawnCooldownReductionMultiplier;
    }
}
