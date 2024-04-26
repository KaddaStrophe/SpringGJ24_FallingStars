using UnityEngine;

public class CharacterMotor : MonoBehaviour {
    [SerializeField]
    StarPhysics physicsComponent = default;
    [SerializeField]
    CharacterInput characterInputComponent = default;

    [Header("Debug")]
    [SerializeField]
    Vector2 movement = new Vector2(10f, 10f);

    Vector2 currentAcceleration = Vector2.zero;

    protected void OnEnable() {
        if (!physicsComponent) {
            TryGetComponent(out physicsComponent);
        }
        if (!characterInputComponent) {
            TryGetComponent(out characterInputComponent);
        }
    }

    protected void FixedUpdate() {
        physicsComponent.acceleration = currentAcceleration;
    }

    public void BounceBack() {
        physicsComponent.velocity *= -1.5f;
    }
}
