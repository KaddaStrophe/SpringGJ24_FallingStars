using UnityEngine;

public class CharacterCollision : MonoBehaviour {
    [SerializeField]
    CharacterMotor motor;

    protected void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            motor.BounceBack();
        }
    }
}
