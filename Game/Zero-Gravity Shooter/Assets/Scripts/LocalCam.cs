using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LocalCam : NetworkBehaviour
{
    Camera cam;
    	
	void Start ()
    {
        cam = GetComponent<Camera>();
        if (isLocalPlayer)  // if this is not my player, remove the camera
        {
            cam.enabled = true;
        }
        else
        {
            cam.enabled = false;
        }
    }
}
