using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    enum RotateType { Parent, Local};
    [SerializeField]
    RotateType rotationType;
    [SerializeField]
    Vector3 rotationAxis;
    [SerializeField]
    float speed;

    // Update is called once per frame
    void Update()
    {
        float angle = speed * Time.deltaTime;

        if (rotationType == RotateType.Parent)
        {
            if (transform.parent != null)
                transform.RotateAround(transform.parent.position, new Vector3(rotationAxis.x * transform.parent.position.x, rotationAxis.y * transform.parent.position.y, rotationAxis.z * transform.parent.position.z), angle);
        }
        else
        {

            //transform.RotateAround(new Vector3(rotationAxis.x * transform.parent.position.x, rotationAxis.y * transform.parent.position.y, rotationAxis.z * transform.parent.position.z), angle);
        }
    }
}
