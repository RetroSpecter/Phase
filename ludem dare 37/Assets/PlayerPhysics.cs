using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

    public LayerMask collisionMask;

    float maxClimbAngle = 70;
    float maxDescendAngle = 70;

    const float skinWidth = 0.1f;

    BoxCollider colliders;
    RaycastOrigins raycastOrigins;
    [HideInInspector]public CollisionInfo collisions;
    public int xRayCount = 4;
    public int zRayCount = 4;
    public int yRayCount = 4;

    float xRaySpacing;
    float zRaySpacing;
    float yRaySpacing;

    void Start()
    {
        colliders = GetComponent<BoxCollider>();
        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity) {
        UpdateRaycastOrigins();
        collisions.Reset();


        


        if (velocity.y != 0)
            yCollisions(ref velocity);

        if (velocity.y < 0)
            DescendSlope(ref velocity);

        if (velocity.z != 0 || velocity.x != 0)
            horizontalCollision(ref velocity);

        transform.Translate(new Vector3(0, velocity.y, velocity.x));
      
    }

    public Vector3 offset;
    void horizontalCollision(ref Vector3 velocity) {
        float directionZ = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        for (int j = 0; j < zRayCount; j++) {
            for (int i = 0; i < yRayCount; i++) {
                Vector3 rayOrigin = raycastOrigins.bottom4 - transform.right * transform.localScale.x;
                rayOrigin += this.transform.right * (xRaySpacing * j) + this.transform.up * (yRaySpacing * i);

                //Debug.DrawRay(rayOrigin, this.transform.forward * directionZ, Color.red);

                RaycastHit hit;
                if (Physics.Raycast(rayOrigin, this.transform.forward, out hit, rayLength, collisionMask)) {

                    float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

                    if (i == 0 && slopeAngle <= maxClimbAngle)
                    {
                        float distanceToSlopeStart = 0;
                        distanceToSlopeStart = hit.distance - skinWidth;
                        climbSlope(ref velocity, slopeAngle);
                    }
                    else {
                        velocity.x = (hit.distance - skinWidth) * directionZ;
                        rayLength = hit.distance;
                    }

                    
                    
                    collisions.backward = directionZ == -1;
                    collisions.forward = directionZ == 1;
                }
            }           
        }
    }

    void yCollisions(ref Vector3 velocity) {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        for (int j = 0; j < zRayCount; j++) {  
            for (int i = 0; i < yRayCount; i++) {
                Vector3 rayOrigin = (directionY == -1) ? raycastOrigins.bottom1 : raycastOrigins.top1;
                rayOrigin += this.transform.forward * (zRaySpacing * i ) + this.transform.right * (xRaySpacing * j );

                //Debug.DrawRay(rayOrigin, this.transform.up * directionY * rayLength * 10, Color.red);

                RaycastHit hit; 
                if(Physics.Raycast(rayOrigin, this.transform.up * directionY, out hit, rayLength, collisionMask)){
                    velocity.y = (hit.distance - skinWidth) * directionY;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                    }

                    collisions.below = directionY == -1;
                    collisions.above = directionY == 1;
                }
            }
        }
    }

    void climbSlope(ref Vector3 velocity, float slopeAngle) {

        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = (Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance);

        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.climbingSlope = true;
            collisions.below = true;
        }
    }

    void DescendSlope(ref Vector3 velocity) {
        Vector3 rayOrigin = raycastOrigins.bottom2;
        
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, -Vector3.up, out hit, Mathf.Infinity, collisionMask)) {
            //Debug.DrawRay(transform.position, Vector3.up * 10, Color.red);
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                
            }
        }
    }

    void UpdateRaycastOrigins() {
        Bounds bounds = colliders.bounds;
        bounds.Expand(skinWidth * -2);

        Vector3 boundsMax = transform.TransformPoint(colliders.center + colliders.size / 2);
        Vector3 boundsMin = transform.TransformPoint(colliders.center - colliders.size / 2);

        raycastOrigins.top1 = new Vector3(boundsMin.x, boundsMax.y, boundsMin.z);
        raycastOrigins.top2 = new Vector3(boundsMax.x, boundsMax.y, boundsMin.z);
        raycastOrigins.top3 = new Vector3(boundsMin.x, boundsMax.y, boundsMax.z);
        raycastOrigins.top4 = new Vector3(boundsMax.x, boundsMax.y, boundsMax.z);
        raycastOrigins.bottom1 = new Vector3(boundsMin.x, boundsMin.y, boundsMin.z);
        raycastOrigins.bottom2 = new Vector3(boundsMax.x, boundsMin.y, boundsMin.z);
        raycastOrigins.bottom3 = new Vector3(boundsMin.x, boundsMin.y, boundsMax.z);
        raycastOrigins.bottom4 = new Vector3(boundsMax.x, boundsMin.y, boundsMax.z);

        //Debug.DrawRay(raycastOrigins.bottom1, transform.forward * 100, Color.green);
    }

    void CalculateRaySpacing(){
        Bounds bounds = colliders.bounds;
        bounds.Expand(skinWidth * -2);

        xRayCount = Mathf.Clamp(xRayCount, 4, int.MaxValue);
        yRayCount = Mathf.Clamp(yRayCount, 4, int.MaxValue);
        zRayCount = Mathf.Clamp(zRayCount, 4, int.MaxValue);

        xRaySpacing = bounds.size.x / (xRayCount - 1);
        yRaySpacing = bounds.size.y / (yRayCount - 1);
        zRaySpacing = bounds.size.z / (zRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector3 top1, top2, top3, top4;
        public Vector3 bottom1, bottom2, bottom3, bottom4;
    }

    public struct CollisionInfo {
        public bool above, below, forward, backward, right, left;
        public bool climbingSlope; 
        public bool descendingSlope;
        public float slopeAngle, slopeAngleOld;

        public void Reset() {
            climbingSlope = false;
            descendingSlope = false;
            slopeAngle = 0;
            above = below = false;
            forward = backward = left = right = false;
        }
    }
}
