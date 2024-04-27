using Cinemachine;
using UnityEngine;

public class CinematicEnding : MonoBehaviour {
    [SerializeField]
    ObstacleChannel obstacleEventChannel = default;
    [SerializeField]
    Camera mainCamera = default;
    [SerializeField]
    CinemachineVirtualCamera virtualCamera = default;
    [SerializeField]
    float rotationTime = 5f;
    [SerializeField]
    float zoomoutTime = 10f;
    [SerializeField]
    LeanTweenType rotationEaseType = LeanTweenType.easeInBack;
    [SerializeField]
    LeanTweenType zoomoutEaseType = LeanTweenType.easeInBack;

    protected void OnEnable() {
        obstacleEventChannel.OnObstacleEnd += TriggerEndSequence;
    }

    protected void OnDisable() {
        obstacleEventChannel.OnObstacleEnd -= TriggerEndSequence;
    }

    void TriggerEndSequence(Obstacle obstacle, CharacterMotor characterMotor) {
        LeanTween.rotateLocal(mainCamera.gameObject, new Vector3(0, 0, 180), rotationTime).setEase(rotationEaseType);
        LeanTween.value(virtualCamera.gameObject, virtualCamera.m_Lens.OrthographicSize, 60f, zoomoutTime).setEase(zoomoutEaseType).setOnUpdate((float val) => { virtualCamera.m_Lens.OrthographicSize = val; });
    }
}
