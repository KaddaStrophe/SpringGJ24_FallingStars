using UnityEngine;

public class CharacterCollision : MonoBehaviour {
    [SerializeField]
    CharacterMotor motor;

    protected void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            motor.BounceBack();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Obstacle")) {
            motor.BreakThrough();
        }
    }
}
