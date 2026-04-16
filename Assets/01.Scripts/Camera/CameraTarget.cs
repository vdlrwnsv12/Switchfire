using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
   public Transform player;

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position;
    }
}
