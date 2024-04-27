using System;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour {
    [SerializeField]
    CharacterChannel characterEventChannel = default;
    [SerializeField]
    LeanTweenType easeType = default;
    [SerializeField]
    float easeTime = 0.1f;

    Vector3 newScale = Vector3.zero;
    LTDescr lastTween = null;

    protected void OnEnable() {
        lastTween = LeanTween.scale(gameObject, newScale, easeTime).setEase(LeanTweenType.notUsed);
        characterEventChannel.OnCharacterResize += VisualizeResizing;
    }
    protected void OnDisable() {
        characterEventChannel.OnCharacterResize -= VisualizeResizing;
    }

    void VisualizeResizing(CharacterMotor character) {
        LeanTween.cancel(lastTween.id);
        newScale = character.GetCurrentSize() switch {
            Size.DEFAULT => new Vector3(character.visualSizeDefault, character.visualSizeDefault, 1),
            Size.SMALL => new Vector3(character.visualSizeSmall, character.visualSizeSmall, 1),
            Size.LARGE => new Vector3(character.visualSizeLarge, character.visualSizeLarge, 1),
            _ => throw new NotImplementedException(),
        };
        lastTween = LeanTween.scale(gameObject, newScale, easeTime).setEase(easeType);
    }
}
