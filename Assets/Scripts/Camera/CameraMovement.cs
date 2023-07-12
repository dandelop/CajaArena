using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed;
    public GameObject target;

    private Transform rigTransform;

    // Start is called before the first frame update
    void Start()
    {
        rigTransform = this.transform;
        

    }

    // Update is called once per frame
    void Update()
    {
       

        if (target != null)
        {
            rigTransform.position = Vector3.Lerp(rigTransform.position, target.transform.position + new Vector3(0, 10, 0),
                                                moveSpeed);
        }

        

    }
}
