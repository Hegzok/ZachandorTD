using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraOffset;

    [SerializeField] [Range(0f, 3f)] private float smoothFactor;
    [SerializeField] private bool LookAtPlayer = false;

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 newPos = player.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        if (LookAtPlayer)
        {
            transform.LookAt(player);
        }
    }
}
