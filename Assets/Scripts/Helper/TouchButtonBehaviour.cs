using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButtonBehaviour : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    [SerializeField]
    CharacterMotor characterMotor = default;
    [SerializeField]
    bool isInflater = false;

    public void OnPointerDown(PointerEventData eventData) {
        if (isInflater) {
            characterMotor.TriggerInflate();
        } else {
            characterMotor.TriggerCompress();
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        characterMotor.TriggerNormalize();
    }

}
