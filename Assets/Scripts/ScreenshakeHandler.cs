using Cinemachine;
using UnityEngine;

public class ScreenshakeHandler : MonoBehaviour {
    [SerializeField]
    CinemachineVirtualCamera cinemachineCamera = default;
    [SerializeField]
    CharacterChannel characterEventChannel = default;
    [SerializeField]
    Vector2 amplitudeAndFrequencyLargeSize = new Vector2(3f, 2f);

    protected void OnEnable() {
        characterEventChannel.OnCharacterResize += CalculateScreenShake;
    }
    protected void OnDisable() {
        characterEventChannel.OnCharacterResize -= CalculateScreenShake;
    }

    void CalculateScreenShake(CharacterMotor character) {
        if (character.GetCurrentSize() == Size.LARGE) {
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeAndFrequencyLargeSize.x;
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = amplitudeAndFrequencyLargeSize.y;
        }
        if (character.GetCurrentSize() != Size.LARGE) {
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        }
    }
}
