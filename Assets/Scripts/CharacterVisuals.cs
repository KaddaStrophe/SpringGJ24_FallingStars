using System;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour {
    [SerializeField]
    CharacterChannel characterEventChannel = default;
    [SerializeField]
    LeanTweenType easeType = default;
    [SerializeField]
    float easeTime = 0.1f;

    Vector2 newScale = Vector2.zero;
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
            Size.DEFAULT => new Vector2(character.visualSizeDefault, character.visualSizeDefault),
            Size.SMALL => new Vector2(character.visualSizeSmall, character.visualSizeSmall),
            Size.LARGE => new Vector2(character.visualSizeLarge, character.visualSizeLarge),
            _ => throw new NotImplementedException(),
        };
        lastTween = LeanTween.scale(gameObject, newScale, easeTime).setEase(easeType);
    }
}
