using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CharacterVisuals : MonoBehaviour {
    [SerializeField]
    CharacterChannel characterEventChannel = default;
    [SerializeField]
    ObstacleChannel obstacleEventChannel = default;
    [SerializeField]
    LeanTweenType sizingEaseType = default;
    [SerializeField]
    float sizingEaseTime = 0.1f;

    [Header("End Game Options")]
    [SerializeField]
    List<GameObject> objectsToDisable = default;
    [SerializeField]
    ParticleSystem backgroundParticle = default;
    [SerializeField]
    VisualEffect particleTrail = default;
    [SerializeField]
    TrailRenderer trailRenderer = default;

    Vector3 newScale = Vector3.zero;
    LTDescr lastTween = null;

    protected void OnEnable() {
        lastTween = LeanTween.scale(gameObject, newScale, sizingEaseTime).setEase(LeanTweenType.notUsed);
        characterEventChannel.OnCharacterResize += VisualizeResizing;
        obstacleEventChannel.OnObstacleEnd += EndGame;
    }

    protected void OnDisable() {
        characterEventChannel.OnCharacterResize -= VisualizeResizing;
        obstacleEventChannel.OnObstacleEnd -= EndGame;
    }

    void VisualizeResizing(CharacterMotor character) {
        LeanTween.cancel(lastTween.id);
        newScale = character.GetCurrentSize() switch {
            Size.DEFAULT => new Vector3(character.visualSizeDefault, character.visualSizeDefault, 1),
            Size.SMALL => new Vector3(character.visualSizeSmall, character.visualSizeSmall, 1),
            Size.LARGE => new Vector3(character.visualSizeLarge, character.visualSizeLarge, 1),
            _ => throw new NotImplementedException(),
        };
        lastTween = LeanTween.scale(gameObject, newScale, sizingEaseTime).setEase(sizingEaseType).setOnComplete(() => { transform.localScale = newScale; });
    }

    void EndGame(Obstacle obstacle, CharacterMotor characterMotor) {
        //backgroundParticle.Stop();
        //particleTrail.Stop();
        //particleTrail.gameObject.SetActive(false);
        //trailRenderer.enabled = false;
        foreach (var item in objectsToDisable) {
            item.SetActive(false);
        }
    }
}
