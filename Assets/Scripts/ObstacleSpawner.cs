using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
    [SerializeField]
    GameObject obstacleContainer = default;
    [SerializeField]
    List<GameObject> obstaclePool = default;
    [SerializeField]
    List<int> obstacleRatesLvl1 = default;
    [SerializeField]
    List<int> obstacleRatesLvl2 = default;
    [SerializeField]
    List<int> obstacleRatesLvl3 = default;
    [SerializeField]
    float obstacleDistance = 10f;
    [SerializeField]
    List<float> levelDurations = default;

    [Header("Debug")]
    [SerializeField]
    float lastObstacleVertPos = default;
    [SerializeField]
    float currentHeight = default;
    [SerializeField]
    float heightDelta = default;
    [SerializeField]
    List<GameObject> obstacleElements = default;
    [SerializeField]
    int currentLevel = 0;

    float nextLevelStart = 0;
    int levelCounter = 0;
    Dictionary<float, List<int>> levelRates = new Dictionary<float, List<int>>();

    protected void OnEnable() {
        levelRates.Add(levelCounter, obstacleRatesLvl1);
        levelRates.Add(1, obstacleRatesLvl2);
        levelRates.Add(2, obstacleRatesLvl3);

        lastObstacleVertPos = transform.position.y;
        nextLevelStart = lastObstacleVertPos - levelDurations[levelCounter];
        levelCounter++;
    }

    protected void FixedUpdate() {
        currentHeight = transform.position.y;
        heightDelta = Mathf.Abs(currentHeight - lastObstacleVertPos);
        if (heightDelta >= obstacleDistance) {
            lastObstacleVertPos = currentHeight;
            SpawnObstacle(currentHeight);
        }
        if (currentHeight <= nextLevelStart) {
            // TODO: Trigger Ending
            if (levelCounter < levelDurations.Count) {
                nextLevelStart -= levelDurations[levelCounter];
                levelCounter++;
            }
        }
        currentLevel = levelCounter;
    }

    void SpawnObstacle(float currentHeight) {
        // Decide which element to spawn
        obstacleElements = new List<GameObject>();
        for (int i = 0; i < obstaclePool.Count; i++) {
            var obstacle = obstaclePool[i];
            for (int j = 0; j < levelRates[levelCounter - 1][i]; j++) {
                obstacleElements.Add(obstacle);
            }
        }
        var obstacleArray = obstacleElements.ToArray();

        var instance = Instantiate(obstacleArray[Random.Range(0, obstacleArray.Length)], obstacleContainer.transform);
        instance.transform.position = new Vector3(0f, currentHeight, 0f);
    }
}
