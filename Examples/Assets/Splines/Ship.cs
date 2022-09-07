using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  PathCreation;

public class Ship : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float minSpeed = 0;
    public float maxSpeed = 5;
    private float _speed = 0; 
    public float acceleration = 2;
    public float breakStrenght;
    private float _distanceTravelled;

    void Start() {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Accelerate(acceleration);
        }
        else if (Input.GetMouseButton(1))
        {
            Accelerate(-breakStrenght);
        }
        else
        {
            Accelerate(-acceleration);
        }
        
        if (pathCreator != null)
        {
            _distanceTravelled += _speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled, endOfPathInstruction);
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        _distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    private void Accelerate(float amount)
    {
        _speed = Mathf.Clamp(_speed += amount * Time.deltaTime , minSpeed, maxSpeed);
    }
}
