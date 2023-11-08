using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MoleSpawner : MonoBehaviour {
    [SerializeField] Mole molePrefab;
    [SerializeField] private float spawnRate;
    private float spawnInterval;
    private float timeSinceLastSpawn;
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private EventChannel gameOverEventChannel;

    //TODO Mole pool

    private void Awake() {
        spawnInterval = 1 / spawnRate;
        gameOverEventChannel.channel += OnGameOver;
    }

    private void OnGameOver() {
        // Disable this script if the game is over
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
        Mole newMole = Instantiate(molePrefab, spawnLocation.position, Quaternion.identity);
        newMole.OnLeave += FreeSpot(spawnLocation);
    }

    private Action FreeSpot(Transform location) {
        return () => { spawnPositions.Add(location); };
    }
}
