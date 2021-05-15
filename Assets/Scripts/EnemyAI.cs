using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour
{
    // What to chase?
	public Transform target;

    // How many times each second we will update our path
	public float updateRate = 2f;

    // Caching
	private Seeker seeker;
	private Rigidbody2D rb;

    //The calculated path
	public Path path;

    //The AI's speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

    [HideInInspector]
	public bool pathIsEnded = false;

    // The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 3;

    // The waypoint we are currently moving towards
	private int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        //  So we don't have to get them again and again.
        seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

        //  No target means trouble.
        if (target == null) {
			Debug.LogError("No Player found? PANIC!");
			return;
		}


        // Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath(transform.position, target.position, OnPathComplete);

        //  Start path recalculation cycle in a separate thread.
        StartCoroutine(UpdatePath());
    }
    
    //  Function for recalculating path every now and then.
    IEnumerator UpdatePath () {
        //  No target is bad again.
        if (target == null) {
            yield return false;
        }

        AstarPath.active.Scan();

        // Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath(transform.position, target.position, OnPathComplete);

        //  Wait for a while.
        yield return new WaitForSeconds( 1f/updateRate );

        //  Start a new calculation
        StartCoroutine (UpdatePath());
    }

    //  In case we have a path.
    public void OnPathComplete (Path p) {
        Debug.Log ("We got a path. Did it have an error? " + p.error);
        //  If there is no error
        if (!p.error) {
            //  Store tne new path and set out next point to the sart.
            path = p;
            currentWaypoint = 0;
        }
    }

    //  Calculating movement in FixedUpdate
    void FixedUpdate()
    {
        if (target == null) {
			//TODO: Insert a player search here.
			return;
		}

        //  No path? Nothing to do.
        if (path == null)
			return;
        
        //  If we reach the end of path
        if (currentWaypoint >= path.vectorPath.Count) {
            //  If we already knew -> do nothing
			if (pathIsEnded)
				return;

            //  If this is new information. Store it and then do nothing.
			Debug.Log ("End of path reached.");
			pathIsEnded = true;
			return;
		}
        pathIsEnded = false;

        //Direction to the next waypoint
		Vector3 dir = ( path.vectorPath[currentWaypoint] - transform.position ).normalized;
		dir *= speed * Time.fixedDeltaTime;

        //Move the AI
		rb.AddForce (dir, fMode);

        //  If we get close to the current waypoint. Set the next point as active.
        float dist = Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance) {
            currentWaypoint++;
			return;
        }
    }
}
