using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField]
    ObstacleChannel obstacleEventChannel = default;

    protected void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.TryGetComponent<CharacterMotor>(out var characterMotor);
            obstacleEventChannel.RaiseObstacleHit(this, characterMotor);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if(collision.TryGetComponent<CharacterMotor>(out var characterMotor)) {
                if(characterMotor.GetCurrentSize() == Size.LARGE) {
                    obstacleEventChannel.RaiseObstacleCrash(this, characterMotor);
                } else {
                    obstacleEventChannel.RaiseObstacleHit(this, characterMotor);
                }
            }
        }
    }
}
