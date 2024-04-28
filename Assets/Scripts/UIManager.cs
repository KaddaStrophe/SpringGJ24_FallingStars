using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField]
    ObstacleChannel obstacleEventChannel = default;

    [Header("Start Screen")]
    [SerializeField]
    CanvasGroup startCanvas = default;
    [SerializeField]
    float startCanvasBlendOutDelay = 2f;
    [SerializeField]
    float startCanvasBlendOutTime = 2;

    [Header("End Screen")]
    [SerializeField]
    CanvasGroup endCanvas = default;
    [SerializeField]
    float endCanvasBlendInDelay = 2f;
    [SerializeField]
    float endCanvasBlendInTime = 2;
    [SerializeField]
    float endCanvasBlendOutDelay = 2f;
    [SerializeField]
    float endCanvasBlendOutTime = 2;


    protected void OnEnable() {
        obstacleEventChannel.OnObstacleEnd += TriggerEndSequence;
        DisableEndScreen();
    }

    void DisableEndScreen() {
        endCanvas.gameObject.SetActive(false);
        endCanvas.alpha = 0;
    }

    protected void OnDisable() {
        obstacleEventChannel.OnObstacleEnd -= TriggerEndSequence;
    }

    void TriggerEndSequence(Obstacle obstacle, CharacterMotor characterMotor) {
        endCanvas.gameObject.SetActive(true);
        LeanTween.alphaCanvas(endCanvas, 1, endCanvasBlendInTime).setDelay(endCanvasBlendInDelay);
    }

    public void BeginGame() {
        LeanTween.alphaCanvas(startCanvas, 0, startCanvasBlendOutTime).setDelay(startCanvasBlendOutDelay).setOnComplete(() => { startCanvas.gameObject.SetActive(false); });
    }

    public void RestartGame() {
        LeanTween.alphaCanvas(endCanvas, 0, endCanvasBlendOutTime).setDelay(endCanvasBlendOutDelay).setOnComplete(() => { endCanvas.gameObject.SetActive(false); });
    }

}
