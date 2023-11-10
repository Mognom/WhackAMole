using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;


public class MoleSpawner : MonoBehaviour {
    [SerializeField] Mole molePrefab;

    [SerializeField] private float spawnRate; // Moles / sec
    [SerializeField] private float spawnRateIncreaseRate;
    private float spawnInterval;
    private float timeSinceLastSpawn;
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private EventChannel gameOverEventChannel;

    private IObjectPool<Mole> molePool;

    private void Awake() {
        spawnInterval = 1 / spawnRate;
        gameOverEventChannel.channel += OnGameOver;

        // Prepare the mole pool
        molePool = new ObjectPool<Mole>(
        () => {
            return Instantiate(molePrefab);
        }, mole => {
            mole.gameObject.SetActive(true);
        }, mole => {
            mole.gameObject.SetActive(false);
        }, mole => {
            Destroy(mole);
        }, false, 20, 40); // 20 starting moles, 40 max
    }

    private void OnGameOver() {
        // Disable this script if the game is over to stop new moles from spawning
        this.enabled = false;
    }

    private void Update() {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval) {
            SpawnMole();
            timeSinceLastSpawn = 0;
        }
    }

    private void SpawnMole() {
        Transform spawnLocation = spawnPositions[Random.Range(0, spawnPositions.Count)];
        spawnPositions.Remove(spawnLocation);
        Mole newMole = molePool.Get();
        newMole.transform.position = spawnLocation.position;
        newMole.OnLeave += FreeSpot(spawnLocation);
        SetSpawnRate(spawnRate + spawnRateIncreaseRate);
    }

    private void SetSpawnRate(float newSpawnRate) {
        spawnRate = newSpawnRate;
        spawnInterval = 1 / spawnRate;
    }

    private Action FreeSpot(Transform location) {
        return () => { spawnPositions.Add(location); };
    }
}
