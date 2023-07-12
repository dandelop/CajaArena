using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField]
    private float walkSpeed = 0.1f;

    [SerializeField]
    private Vector2 defaultPositionRange = new Vector2(-4, 4);

    [SerializeField]
    private NetworkVariable<float> forwardBackPosition = new();

    [SerializeField]
    private NetworkVariable<float> leftRightPosition = new();

    public float overlayHeight;

    private Rigidbody rigBody;

    private TextMesh overlay;

    private void Start()
    {
        transform.position = new Vector3(Random.Range(defaultPositionRange.x, defaultPositionRange.y), 0.5f, 
            Random.Range(defaultPositionRange.x, defaultPositionRange.y));
        
        rigBody = GetComponentInParent<Rigidbody>();
        overlay = GetComponentInChildren<TextMesh>();
    }

    private void Update()
    {          
        if (IsServer)
        {
            UpdateServer();
        } 

        if(IsClient && IsOwner)
        {
            UpdateClient();
        }

        overlay.transform.rotation = Quaternion.identity;
        overlay.transform.position = (this.transform.localPosition + (Vector3.up * overlayHeight));  
    }

    private void UpdateServer()
    {
        transform.position = new Vector3(transform.position.x+leftRightPosition.Value, transform.position.y,
            transform.position.z + forwardBackPosition.Value);   
    }

    private void UpdateClient()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        
        Vector3 force = new Vector3(x, 0, z);

        //Update the server
        UpdateClientPositionServerRpc(force, walkSpeed);   
    }

    [ServerRpc]
    private void UpdateClientPositionServerRpc(Vector3 force, float walkSpeed)
    {
        rigBody.AddForce(force * walkSpeed);
    }
}

