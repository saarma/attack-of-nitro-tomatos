using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    GameObject cameraPosition2Target;

    [SerializeField]
    GameObject cameraPosition1Target;

    GameObject camera;

    bool isCameraInOriginalPosition;

    // Start is called before the first frame update
    void Start()
    {
        isCameraInOriginalPosition = true;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        var changeCameraPosition = false;
        if(Input.GetKeyDown(KeyCode.V)) {
            isCameraInOriginalPosition = !isCameraInOriginalPosition;
            changeCameraPosition = true;
        }

        if(changeCameraPosition) {
            if(isCameraInOriginalPosition) {
                this.camera.transform.position = cameraPosition1Target.transform.position;
                float y = 0f;
                this.camera.transform.rotation = Quaternion.AngleAxis(y, Vector3.up);
            } else {
                this.camera.transform.position = cameraPosition2Target.transform.position;
                float y = 20f;
                this.camera.transform.rotation = Quaternion.AngleAxis(y, Vector3.up);
            }
        }
    }
}
