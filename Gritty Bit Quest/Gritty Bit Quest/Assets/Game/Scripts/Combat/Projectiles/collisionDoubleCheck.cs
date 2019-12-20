using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class collisionDoubleCheck : MonoBehaviour {

    [SerializeField]
    LayerMask layerMask;
    public List<Transform> m_raycastPoints = new List<Transform>();
    [HideInInspector]
    public bool m_hitSomething = false;
    [HideInInspector]
    public GameObject m_hitObject;
    [HideInInspector]
    public RaycastHit m_rayHit;
    [SerializeField]
    float rayRange;

    public float skinWidth = 0.1f; //probably doesn't need to be changed

    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private Vector3 previousPosition;
    private Rigidbody myRigidbody;
    private Collider myCollider;

    void Start()
    {
        myCollider = GetComponent<Collider>();
        previousPosition = transform.position;
        minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }

    // Update is called once per frame
    void FixedUpdate () 
	{
		//{
		//	Ray ray = new Ray(m_raycastPoints[i].position, m_raycastPoints[i].forward);
		//	RaycastHit hit;
		//	if(Physics.Raycast(ray, out hit, rayRange, layerMask)) // ignoring layermask, did we hit something
		//	{
		//		m_hitObject = hit.collider.gameObject;
        //        m_rayHit = hit;
		//		m_hitSomething = true;
		//	}
		//}

        for (int i = 0; i < m_raycastPoints.Count; i++)
        { 
            //have we moved more than our minimum extent?
            Vector3 movementThisStep = transform.position - previousPosition;
             float movementSqrMagnitude = movementThisStep.sqrMagnitude;

            if (movementSqrMagnitude > sqrMinimumExtent)
            {
                float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
                RaycastHit hitInfo;

                //check for obstructions we might have missed
                if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
                {
                    if (!hitInfo.collider)
                        return;

                    m_hitObject = hitInfo.collider.gameObject;
                    m_hitSomething = true;

                }
            }
        }
        previousPosition = transform.position;


    }
}
