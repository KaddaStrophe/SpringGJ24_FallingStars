using UnityEngine;

[CreateAssetMenu(fileName = "EC_ObstacleChannel", menuName = "ScriptableObjects/EventChannel/ObstacleChannel")]
public class ObstacleChannel : ScriptableObject {

    public delegate void ObstacleHitCallback(Obstacle obstacle, CharacterMotor characterMotor);
    public ObstacleHitCallback OnObstacleHit;

    public delegate void ObstacleCrashCallback(Obstacle obstacle, CharacterMotor characterMotor);
    public ObstacleCrashCallback OnObstacleCrash;

    public delegate void ObstacleEndCallback(Obstacle obstacle, CharacterMotor characterMotor);
    public ObstacleEndCallback OnObstacleEnd;

    public void RaiseObstacleHit(Obstacle obstacle, CharacterMotor characterMotor) {
        OnObstacleHit?.Invoke(obstacle, characterMotor);
    }

    public void RaiseObstacleCrash(Obstacle obstacle, CharacterMotor characterMotor) {
        OnObstacleCrash?.Invoke(obstacle, characterMotor);
    }
    public void RaiseObstacleEnd(Obstacle obstacle, CharacterMotor characterMotor) {
        OnObstacleEnd?.Invoke(obstacle, characterMotor);
    }
}