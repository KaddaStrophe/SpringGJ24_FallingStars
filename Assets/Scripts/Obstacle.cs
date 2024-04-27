using UnityEngine;

public class Obstacle : MonoBehaviour {
    [SerializeField]
    ObstacleChannel obstacleEventChannel = default;

    protected void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            obstacleEventChannel.RaiseObstacleHit(this);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            obstacleEventChannel.RaiseObstacleCrash(this);
        }
    }
}
