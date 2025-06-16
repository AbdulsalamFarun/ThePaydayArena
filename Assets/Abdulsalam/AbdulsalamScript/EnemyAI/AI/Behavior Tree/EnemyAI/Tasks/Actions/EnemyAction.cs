// EnemyActions.cs
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    // You can adjust this in the Unity Inspector for each enemy
    [Tooltip("How fast the enemy turns to face its target.")]
    public float turnSpeed = 5f;

    // The current target to look at.
    // The Behavior Graph will set this variable.
    private Transform currentTarget;

    // The Behavior Graph will call this method to set the target.
    public void SetTarget(Transform target)
    {
        currentTarget = target;  
    }

    // This is the core logic method that the Behavior Graph will trigger.
    public void SmoothLookAtTarget()
    {
        if (currentTarget == null)
        {
            return; // Do nothing if there's no target
        }

        //// Find the direction vector pointing from us to the target
        //Vector3 direction = currentTarget.position - transform.position;
        //direction.y = 0; // Keep the enemy upright, don't look up/down

        //// Create a rotation that looks along that direction
        //Quaternion lookRotation = Quaternion.LookRotation(direction);

        //// Smoothly rotate from our current rotation to the target rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        transform.LookAt(currentTarget);
    }
}