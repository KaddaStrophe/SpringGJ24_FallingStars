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

    Vector2 currentScreenShake = Vector2.zero;


    protected void OnEnable() {
        characterEventChannel.OnCharacterResize += CalculateScreenShake;
        obstacleEventChannel.OnObstacleHit += ObstacleHitShake;
    }
    protected void OnDisable() {
        characterEventChannel.OnCharacterResize -= CalculateScreenShake;
        obstacleEventChannel.OnObstacleHit -= ObstacleHitShake;
    }

    void ObstacleHitShake(Obstacle obstacle) {
        cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeAndFrequencyObstacleHit.x;
        cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = amplitudeAndFrequencyObstacleHit.y;
        StartCoroutine(ScreenShakingTime(screenShakeDurationObstacleHit));
    }

    IEnumerator ScreenShakingTime(float screenShakeDurationObstacleHit) {
        yield return new WaitForSeconds(screenShakeDurationObstacleHit);
        cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = currentScreenShake.x;
        cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = currentScreenShake.y;
    }

    void CalculateScreenShake(CharacterMotor character) {
        if (character.GetCurrentSize() == Size.LARGE) {
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeAndFrequencyLargeSize.x;
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = amplitudeAndFrequencyLargeSize.y;
            currentScreenShake = amplitudeAndFrequencyLargeSize;
        }
        if (character.GetCurrentSize() != Size.LARGE) {
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
            currentScreenShake = Vector2.zero;
        }
    }
}
