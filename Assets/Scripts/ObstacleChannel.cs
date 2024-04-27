using UnityEngine;

[CreateAssetMenu(fileName = "EC_ObstacleChannel", menuName = "ScriptableObjects/EventChannel/ObstacleChannel")]
public class ObstacleChannel : ScriptableObject {

    public delegate void ObstacleHitCallback(Obstacle obstacle);
    public ObstacleHitCallback OnObstacleHit;

    public delegate void ObstacleCrashCallback(Obstacle obstacle);
    public ObstacleCrashCallback OnObstacleCrash;

    public void RaiseObstacleHit(Obstacle obstacle) {
        OnObstacleHit?.Invoke(obstacle);
    }

    public void RaiseObstacleCrash(Obstacle obstacle) {
        OnObstacleCrash?.Invoke(obstacle);
    }
}