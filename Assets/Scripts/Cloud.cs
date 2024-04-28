using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    float speedMin = 0f;
    [SerializeField]
    float speedMax = 2f;

    [Header("Debug")]
    [SerializeField]
    float cloudSpeed = 0f;

    protected void OnEnable() {
        cloudSpeed = Random.Range(speedMin, speedMax);
    }

    protected void FixedUpdate() {
        transform.Translate(new Vector3(1f * cloudSpeed, 0f, 0f));
    }
}
