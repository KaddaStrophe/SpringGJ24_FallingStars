using UnityEngine;

public class StarPhysics : MonoBehaviour {
    [SerializeField]
    float _drag = 0f;
    [SerializeField]
    float _rotationalDrag = 0f;
    [SerializeField]
    float _mass = 1f;
    [SerializeField]
    float _minVelocity = 0.1f;
    [SerializeField]
    float _minRotVelocity = 0.1f;
    [SerializeField]
    float gravity = -9.8f;

    public float drag { get => _drag; internal set => _drag = value; }
    public float rotationalDrag { get => _rotationalDrag; internal set => _rotationalDrag = value; }
    public float mass { get => _mass; internal set => _mass = value; }
    public float minVelocity { get => _minVelocity; internal set => _minVelocity = value; }
    public float minRotVelocity { get => _minRotVelocity; internal set => _minRotVelocity = value; }

    public Vector2 position { get; internal set; }

    public Vector2 forward { get => transform.up; }
    public Vector2 velocity { get; internal set; }
    public Vector2 acceleration { get; set; }

    public float rotation { get; internal set; }
    public float rotationalVelocity { get; internal set; }
    public float rotationalAcceleration { get; set; }

    protected void OnEnable() {
        position = transform.position;

    }
    protected void FixedUpdate() {
        // __ Calculate Drag Acceleration __
        var poweredVel = new Vector2(velocity.x * velocity.x, velocity.y * velocity.y);
        var calculatedForce = new Vector2(poweredVel.x * _drag, poweredVel.y * _drag);
        var calculatedDragAcc = new Vector2(calculatedForce.x / _mass, calculatedForce.y / _mass);
        var dragVelocity = new Vector2(calculatedDragAcc.x * Time.deltaTime, calculatedDragAcc.y * Time.deltaTime);
        if (velocity.x > 0) {
            dragVelocity = new Vector2(-dragVelocity.x, dragVelocity.y);
        }
        if (velocity.y > 0) {
            dragVelocity = new Vector2(dragVelocity.x, -dragVelocity.y);
        }

        //
        // __ Calculate Position __
        velocity += new Vector2((acceleration.x * Time.deltaTime) + dragVelocity.x, ((acceleration.y + gravity) * Time.deltaTime) + dragVelocity.y);
        if (velocity.magnitude <= _minVelocity && acceleration.magnitude == 0f) {
            velocity = Vector2.zero;
        }
        position += new Vector2(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime);

        // __ Calculate Rotation __
        //float poweredRotVel = rotationalVelocity * rotationalVelocity;
        //float calculatedRotDragAcc = poweredRotVel * _rotationalDrag / _mass;
        //float rotDragVelocity = calculatedRotDragAcc * Time.deltaTime;

        //bool rotIsNegative = rotationalVelocity < 0 ? true : false;
        //if (rotationalVelocity < 0) {
        //    rotationalVelocity += (rotationalAcceleration * Time.deltaTime) + rotDragVelocity;
        //} else {
        //    rotationalVelocity += (rotationalAcceleration * Time.deltaTime) - rotDragVelocity;
        //}
        //if ((Math.Abs(rotationalVelocity) <= _minRotVelocity || rotIsNegative != (rotationalVelocity < 0))
        //    && rotationalAcceleration == 0f) {
        //    rotationalVelocity = 0f;
        //}

        //rotation += rotationalVelocity * Time.deltaTime;
        //rotation %= 360;
        //if (rotation < 0) {
        //    rotation += 360;
        //}
        //Debug.Log(rotationalVelocity);
        transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, rotation));
    }
}