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

    [Header("Character Look")]
    [SerializeField]
    GameObject renderer = default;
    [SerializeField]
    CircleCollider2D collider = default;

    [Header("Debug")]
    [SerializeField]
    Vector2 movement = new Vector2(10f, 10f);

    Vector2 currentAcceleration = Vector2.zero;
    bool isCompressed = false;

    protected void OnEnable() {
        if (!physicsComponent) {
            TryGetComponent(out physicsComponent);
        }
        if (!characterInputComponent) {
            TryGetComponent(out characterInputComponent);
        }
        if (!collider) {
            TryGetComponent(out collider);
        }
    }

    protected void FixedUpdate() {
        physicsComponent.acceleration = currentAcceleration;
        if (characterInputComponent.shouldCompress && !isCompressed) {
            Debug.Log("Compress");
            isCompressed = true;
            ResizeCharacter(Size.SMALL);
        }
        if(!characterInputComponent.shouldCompress && isCompressed) {
            Debug.Log("Decompress");
            isCompressed = false;
            ResizeCharacter(Size.DEFAULT);
        }
    }

    void ResizeCharacter(Size size) {
        switch (size) {
            case Size.DEFAULT:
                renderer.transform.localScale = Vector3.one;
                collider.radius = 0.5f;
                break;
            case Size.SMALL:
                renderer.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                collider.radius = 0.25f;
                break;
            case Size.LARGE:
                renderer.transform.localScale = new Vector3(2.3f, 2.3f, 1f);
                collider.radius = 1.15f;
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void BounceBack() {
        physicsComponent.velocity *= -1.5f;
    }
}
