//referenced from: https://answers.unity.com/questions/1077171/how-to-make-camera-follow-player-position-and-rota.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform player;
	public Vector3 offsetPosition;

	public Space offsetSpace = Space.Self;

	public bool lookAt = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

	void Update()
    {
		CamMove();
    }

	public void CamMove()
    {
		//compute position 
		if(offsetSpace == Space.Self)
        {
			transform.position = player.TransformPoint(offsetPosition);
        }
        else
        {
			transform.position = player.position + offsetPosition;
        }
        //compute rotation
        if (lookAt)
        {
			transform.LookAt(player);
        }
        else
        {
            transform.rotation = player.rotation;
        }

    }
}