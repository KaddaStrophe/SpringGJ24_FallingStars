using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class CharacterMotor : MonoBehaviour {
    [SerializeField]
    StarPhysics physicsComponent = default;
    [SerializeField]
    CharacterInput characterInputComponent = default;
    [SerializeField]
    CharacterChannel characterEventChannel = default;
    [SerializeField]
    ObstacleChannel obstacleEventChannel = default;

    [Header("Character")]
    [SerializeField]
    TrailRenderer trailRenderer = default;
    [SerializeField]
    CircleCollider2D characterCollider = default;

    [Header("Character Deformation Parameters")]
    [SerializeField]
    public float visualSizeSmall = 0.5f;
    [SerializeField]
    float gravitySmall = -9.8f;
    [SerializeField]
    public float visualSizeDefault = 1.0f;
    [SerializeField]
    float gravityDefault = -9.8f;
    [SerializeField]
    public float visualSizeLarge = 2.3f;
    [SerializeField]
    float gravityLarge = -30f;
    [SerializeField]
    float gravityEnd = -1;
    [SerializeField]
    VisualEffect meteorVFX = default;

    [Header("Debug")]
    [SerializeField]
    float bounceBackTimer = 1f;
    [SerializeField, ReadOnly]
    Size currentSize = Size.DEFAULT;

    Vector2 currentAcceleration = Vector2.zero;
    bool isCompressed = false;
    bool isInflated = false;
    bool alreadyBouncedBack = false;
    bool usingOnScreenButtons = false;
    bool usingInputDevices = false;
    bool endGame = false;

    protected void OnEnable() {
        endGame = false;
        if (!physicsComponent) {
            TryGetComponent(out physicsComponent);
        }
        if (!characterInputComponent) {
            TryGetComponent(out characterInputComponent);
        }
        if (!characterCollider) {
            TryGetComponent(out characterCollider);
        }
        obstacleEventChannel.OnObstacleHit += BounceBack;
        obstacleEventChannel.OnObstacleCrash += BreakThrough;
        obstacleEventChannel.OnObstacleEnd += EndGame;
    }

    protected void OnDisable() {
        obstacleEventChannel.OnObstacleHit -= BounceBack;
        obstacleEventChannel.OnObstacleCrash -= BreakThrough;
        obstacleEventChannel.OnObstacleEnd -= EndGame;
    }

    protected void Start() {
        NormalizeSize();
        meteorVFX.Stop();
    }

    protected void FixedUpdate() {
        physicsComponent.acceleration = currentAcceleration;
        if (!usingOnScreenButtons) {
            if (!isInflated) {
                if (characterInputComponent.shouldCompress && !isCompressed) {
                    usingInputDevices = true;
                    Compress();
                }
                if (!characterInputComponent.shouldCompress && isCompressed) {
                    usingInputDevices = false;
                    NormalizeSize();
                }
            }
            if (!isCompressed) {
                if (characterInputComponent.shouldInflate && !isInflated) {
                    usingInputDevices = true;
                    Inflate();
                }
                if (!characterInputComponent.shouldInflate && isInflated) {
                    usingInputDevices = false;
                    NormalizeSize();
                }
            }
        }
    }

    void Compress() {
        if (!endGame) {
            isCompressed = true;
            ResizeCharacter(Size.SMALL);
            characterEventChannel.RaiseCharacterResize(this);
        }
    }

    void Inflate() {
        if (!endGame) {
            isInflated = true;
            ResizeCharacter(Size.LARGE);
            meteorVFX.Reinit();
            meteorVFX.Play();
            characterEventChannel.RaiseCharacterResize(this);
        }
    }
    void NormalizeSize() {
        isInflated = false;
        isCompressed = false;
        ResizeCharacter(Size.DEFAULT);
        meteorVFX.Stop();
        characterEventChannel.RaiseCharacterResize(this);
    }

    void ResizeCharacter(Size size) {
        switch (size) {
            case Size.DEFAULT:
                trailRenderer.widthMultiplier = 1;
                physicsComponent.SetGravity(gravityDefault);
                currentSize = Size.DEFAULT;
                break;
            case Size.SMALL:
                trailRenderer.widthMultiplier = 0.5f;
                physicsComponent.SetGravity(gravitySmall);
                currentSize = Size.SMALL;
                break;
            case Size.LARGE:
                trailRenderer.widthMultiplier = 2f;
                physicsComponent.SetGravity(gravityLarge);
                currentSize = Size.LARGE;
                break;
            default:
                throw new NotImplementedException();
        }
    }


    void BounceBack(Obstacle obstacle, CharacterMotor characterMotor) {
        if (!alreadyBouncedBack) {
            physicsComponent.velocity *= -1.5f;
            alreadyBouncedBack = true;
        }
        StartCoroutine(Countdown());
    }

    void BreakThrough(Obstacle obstacle, CharacterMotor characterMotor) {
        physicsComponent.velocity = Vector2.zero;
    }

    void EndGame(Obstacle obstacle, CharacterMotor characterMotor) {
        endGame = true;
        physicsComponent.velocity = Vector2.zero;
        physicsComponent.SetGravity(gravityEnd);
    }
    public void TriggerInflate() {
        if (!usingInputDevices) {
            usingOnScreenButtons = true;
            if (!isCompressed && !isInflated) {
                Inflate();
            }
        }
    }

    public void TriggerCompress() {
        if (!usingInputDevices) {
            usingOnScreenButtons = true;
            if (!isCompressed && !isInflated) {
                Compress();
            }
        }
    }

    public void TriggerNormalize() {
        if (!usingInputDevices) {
            usingOnScreenButtons = false;
            if (isCompressed || isInflated) {
                NormalizeSize();
            }
        }
    }
    IEnumerator Countdown() {
        yield return new WaitForSeconds(bounceBackTimer);
        alreadyBouncedBack = false;
    }
    public Size GetCurrentSize() {
        return currentSize;
    }
}
