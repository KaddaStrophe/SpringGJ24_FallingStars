using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class ScreenshakeHandler : MonoBehaviour {
    [Header("Character Related")]
    [SerializeField]
    CinemachineVirtualCamera cinemachineCamera = default;
    [SerializeField]
    CharacterChannel characterEventChannel = default;
    [SerializeField]
    Vector2 amplitudeAndFrequencyLargeSize = new Vector2(3f, 2f);

    [Header("Obstacle Collision")]
    [SerializeField]
    ObstacleChannel obstacleEventChannel = default;

    [SerializeField]
    Vector2 amplitudeAndFrequencyObstacleHit = new Vector2(3f, 2f);
    [SerializeField]
    float screenShakeDurationObstacleHit = 1f;
    [SerializeField]
    Vector3 obstacleHitSizeMultiplier = new Vector3(0.5f, 1f, 1.5f);

    [SerializeField]
    Vector2 amplitudeAndFrequencyObstacleCrash = new Vector2(5f, 5f);
    [SerializeField]
    float screenShakeDurationObstacleCrash = 2f;

    Vector2 currentScreenShake = Vector2.zero;
    CinemachineBasicMultiChannelPerlin cinemachinePerlinNoise = default;
    bool endGame = false;

    protected void OnEnable() {
        characterEventChannel.OnCharacterResize += MovingScreenshake;
        obstacleEventChannel.OnObstacleHit += ObstacleHitShake;
        obstacleEventChannel.OnObstacleCrash += ObstacleCrashShake;
        obstacleEventChannel.OnObstacleEnd += ObstacleEndShake;
        cinemachinePerlinNoise = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    protected void OnDisable() {
        characterEventChannel.OnCharacterResize -= MovingScreenshake;
        obstacleEventChannel.OnObstacleHit -= ObstacleHitShake;
        obstacleEventChannel.OnObstacleCrash -= ObstacleCrashShake;
        obstacleEventChannel.OnObstacleEnd -= ObstacleEndShake;
    }

    void ObstacleHitShake(Obstacle obstacle, CharacterMotor character) {
        var newValues = new Vector2(amplitudeAndFrequencyObstacleHit.x, amplitudeAndFrequencyObstacleHit.y);
        newValues *= character.GetCurrentSize() switch {
            Size.SMALL => obstacleHitSizeMultiplier.x,
            Size.DEFAULT => obstacleHitSizeMultiplier.y,
            Size.LARGE => obstacleHitSizeMultiplier.z,
            _ => throw new NotImplementedException(),
        };

        cinemachinePerlinNoise.m_AmplitudeGain = newValues.x;
        cinemachinePerlinNoise.m_FrequencyGain = newValues.y;
        StartCoroutine(ScreenShakingTime(screenShakeDurationObstacleHit));
    }

    void ObstacleCrashShake(Obstacle obstacle, CharacterMotor characterMotor) {
        cinemachinePerlinNoise.m_AmplitudeGain = amplitudeAndFrequencyObstacleCrash.x;
        cinemachinePerlinNoise.m_FrequencyGain = amplitudeAndFrequencyObstacleCrash.y;
        StartCoroutine(ScreenShakingTime(screenShakeDurationObstacleCrash));
    }

    void ObstacleEndShake(Obstacle obstacle, CharacterMotor characterMotor) {
        endGame = true;
        cinemachinePerlinNoise.m_AmplitudeGain = amplitudeAndFrequencyObstacleCrash.x;
        cinemachinePerlinNoise.m_FrequencyGain = amplitudeAndFrequencyObstacleCrash.y;
        StartCoroutine(ScreenShakingTime(screenShakeDurationObstacleCrash));
    }

    void MovingScreenshake(CharacterMotor character) {
        if (!endGame) {
            if (character.GetCurrentSize() == Size.LARGE) {
                cinemachinePerlinNoise.m_AmplitudeGain = amplitudeAndFrequencyLargeSize.x;
                cinemachinePerlinNoise.m_FrequencyGain = amplitudeAndFrequencyLargeSize.y;
                currentScreenShake = amplitudeAndFrequencyLargeSize;
            }
            if (character.GetCurrentSize() != Size.LARGE) {
                cinemachinePerlinNoise.m_AmplitudeGain = 0f;
                cinemachinePerlinNoise.m_FrequencyGain = 0f;
                currentScreenShake = Vector2.zero;
            }
        }
    }

    IEnumerator ScreenShakingTime(float screenShakeDurationObstacleHit) {
        yield return new WaitForSeconds(screenShakeDurationObstacleHit);
        cinemachinePerlinNoise.m_AmplitudeGain = currentScreenShake.x;
        cinemachinePerlinNoise.m_FrequencyGain = currentScreenShake.y;
    }
}
