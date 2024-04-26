using System;
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

    Vector2 currentAcceleration = Vector2.zero;
    bool isCompressed = false;
    bool isInflated = false;

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
    }

    protected void FixedUpdate() {
        physicsComponent.acceleration = currentAcceleration;

        if (!isInflated) {
            if (characterInputComponent.shouldCompress && !isCompressed) {
                Debug.Log("Compress");
                isCompressed = true;
                ResizeCharacter(Size.SMALL);
            }
            if (!characterInputComponent.shouldCompress && isCompressed) {
                Debug.Log("Decompress");
                isCompressed = false;
                ResizeCharacter(Size.DEFAULT);
            }
        }
        if (!isCompressed) {
            if (characterInputComponent.shouldInflate && !isInflated) {
                isInflated = true;
                ResizeCharacter(Size.LARGE);
            }
            if (!characterInputComponent.shouldInflate && isInflated) {
                isInflated = false;
                ResizeCharacter(Size.DEFAULT);
            }
        }
    }

    void ResizeCharacter(Size size) {
        switch (size) {
            case Size.DEFAULT:
                characterRenderer.transform.localScale = new Vector3(visualSizeDefault, visualSizeDefault, 1f);
                characterCollider.radius = visualSizeDefault / 2f;
                physicsComponent.SetGravity(gravityDefault);
                break;
            case Size.SMALL:
                characterRenderer.transform.localScale = new Vector3(visualSizeSmall, visualSizeSmall, 1f);
                characterCollider.radius = visualSizeSmall / 2f;
                physicsComponent.SetGravity(gravitySmall);
                break;
            case Size.LARGE:
                characterRenderer.transform.localScale = new Vector3(visualSizeLarge, visualSizeLarge, 1f);
                characterCollider.radius = visualSizeLarge / 2f;
                physicsComponent.SetGravity(gravityLarge);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void BounceBack() {
        physicsComponent.velocity *= -1.5f;
    }
}
