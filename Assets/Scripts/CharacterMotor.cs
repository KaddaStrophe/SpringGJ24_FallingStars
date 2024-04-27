using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
public enum Size {
    DEFAULT, SMALL, LARGE
}
public class CharacterMotor : MonoBehaviour {
    [SerializeField]
    StarPhysics physicsComponent = default;
    [SerializeField]
    CharacterInput characterInputComponent = default;

    [Header("Character")]
    [SerializeField]
    GameObject characterRenderer = default;
    [SerializeField]
    CircleCollider2D characterCollider = default;

    [Header("Character Deformation Parameters")]
    [SerializeField]
    float visualSizeSmall = 0.5f;
    [SerializeField]
    float gravitySmall = -9.8f;
    [SerializeField]
    float visualSizeDefault = 1.0f;
    [SerializeField]
    float gravityDefault = -9.8f;
    [SerializeField]
    float visualSizeLarge = 2.3f;
    [SerializeField]
    float gravityLarge = -30f;

    [Header("Debug")]
    [SerializeField]
    Vector2 movement = new Vector2(10f, 10f);
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

    protected void OnEnable() {
        if (!physicsComponent) {
            TryGetComponent(out physicsComponent);
        }
        if (!characterInputComponent) {
            TryGetComponent(out characterInputComponent);
        }
        if (!characterCollider) {
            TryGetComponent(out characterCollider);
        }
        ResizeCharacter(Size.DEFAULT);
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
        isCompressed = true;
        ResizeCharacter(Size.SMALL);
    }

    void Inflate() {
        isInflated = true;
        ResizeCharacter(Size.LARGE);
    }
    void NormalizeSize() {
        isInflated = false;
        isCompressed = false;
        ResizeCharacter(Size.DEFAULT);
    }

    void ResizeCharacter(Size size) {
        switch (size) {
            case Size.DEFAULT:
                characterRenderer.transform.localScale = new Vector3(visualSizeDefault, visualSizeDefault, 1f);
                characterCollider.radius = visualSizeDefault / 2f;
                physicsComponent.SetGravity(gravityDefault);
                currentSize = Size.DEFAULT;
                break;
            case Size.SMALL:
                characterRenderer.transform.localScale = new Vector3(visualSizeSmall, visualSizeSmall, 1f);
                characterCollider.radius = visualSizeSmall / 2f;
                physicsComponent.SetGravity(gravitySmall);
                currentSize = Size.SMALL;
                break;
            case Size.LARGE:
                characterRenderer.transform.localScale = new Vector3(visualSizeLarge, visualSizeLarge, 1f);
                characterCollider.radius = visualSizeLarge / 2f;
                physicsComponent.SetGravity(gravityLarge);
                currentSize = Size.LARGE;
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void BounceBack() {
        if (!alreadyBouncedBack) {
            physicsComponent.velocity *= -1.5f;
            alreadyBouncedBack = true;
        }
        StartCoroutine(Countdown());
    }

    public void BreakThrough() {
        if (currentSize == Size.LARGE) {
            physicsComponent.velocity = Vector2.zero;
        } else {
            BounceBack();
        }
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
}
