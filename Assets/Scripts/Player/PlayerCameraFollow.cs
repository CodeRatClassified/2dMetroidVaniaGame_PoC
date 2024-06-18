using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    public float trackingSpeed = 30f;
    public float yOffset = 1f;
    public Transform target;


    void Update()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + yOffset, -10f);

        transform.position = Vector3.Lerp(transform.position, targetPosition, trackingSpeed * Time.deltaTime);
    }
}

