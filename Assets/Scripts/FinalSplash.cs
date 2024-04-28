using System;
using UnityEngine;

public class FinalSplash : MonoBehaviour {
    [SerializeField]
    ObstacleChannel obstacleEventChannel = default;
    [SerializeField]
    ParticleSystem splashParticle = default;

    protected void OnEnable() {
        obstacleEventChannel.OnObstacleEnd += ExcetuteSplash;
    }

    void ExcetuteSplash(Obstacle obstacle, CharacterMotor characterMotor) {
        splashParticle.Play();
    }
}
