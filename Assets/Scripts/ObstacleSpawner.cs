using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
    [SerializeField]
    Color backgroundStartColor = default;
    [SerializeField]
    Color backgroundGoalColor = default;
    [SerializeField]
    Camera mainCamera = default;
    [SerializeField]
    GameObject obstacleContainer = default;
    [SerializeField]
    List<GameObject> obstaclePool = default;

    [SerializeField]
    float obstacleEndDistance = 20f;
    [SerializeField]
    GameObject obstacleEnd = default;

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
    float heightDeltaSinceLastStep = default;
    [SerializeField]
    List<GameObject> obstacleElements = default;
    [SerializeField]
    int currentLevel = 0;

    [SerializeField]
    float totalLevelSize = 0;
    bool spawningIsActive = true;
    float nextLevelStart = 0;
    int levelCounter = 0;
    Dictionary<float, List<int>> levelRates = new Dictionary<float, List<int>>();
    Vector3 colorSteps = Vector3.zero;
    GameObject endObstacleInstance = default;

    protected void OnEnable() {
        levelRates.Add(levelCounter, obstacleRatesLvl1);
        levelRates.Add(1, obstacleRatesLvl2);
        levelRates.Add(2, obstacleRatesLvl3);

        lastObstacleVertPos = transform.position.y;
        nextLevelStart = lastObstacleVertPos - levelDurations[levelCounter];
        levelCounter++;
        CalculateColorSteps();

        endObstacleInstance = Instantiate(obstacleEnd, obstacleContainer.transform);
        endObstacleInstance.transform.position = new Vector3(1000f, 1000f, 1000f);
    }

    void CalculateColorSteps() {
        foreach (float duration in levelDurations) {
            totalLevelSize += duration;
        }
        totalLevelSize += obstacleEndDistance;
        totalLevelSize += obstacleDistance * 2;
        var colorDelta = new Vector3(backgroundGoalColor.r, backgroundGoalColor.g, backgroundGoalColor.b) - new Vector3(backgroundStartColor.r, backgroundStartColor.g, backgroundStartColor.b);
        colorSteps = colorDelta / totalLevelSize;
        mainCamera.backgroundColor = backgroundStartColor;
    }

    protected void FixedUpdate() {
        heightDeltaSinceLastStep = currentHeight - transform.position.y;
        BlendBackgroundColor(heightDeltaSinceLastStep);
        currentHeight = transform.position.y;
        if (spawningIsActive) {
            heightDelta = Mathf.Abs(currentHeight - lastObstacleVertPos);
            if (heightDelta >= obstacleDistance) {
                lastObstacleVertPos = currentHeight;
                SpawnObstacle(currentHeight);
            }
            if (currentHeight <= nextLevelStart) {
                if (levelCounter < levelDurations.Count) {
                    nextLevelStart -= levelDurations[levelCounter];
                    levelCounter++;
                } else {
                    spawningIsActive = false;
                    EndGame();
                }
            }
            currentLevel = levelCounter;
        }
    }

    void BlendBackgroundColor(float heightDelta) {
        mainCamera.backgroundColor += new Color(colorSteps.x * heightDelta, colorSteps.y * heightDelta, colorSteps.z * heightDelta, 1);
    }

    void EndGame() {
        endObstacleInstance.transform.position = new Vector3(0f, currentHeight - obstacleEndDistance, 0f);
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
