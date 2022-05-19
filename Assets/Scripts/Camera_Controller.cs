using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    //variable
    public GameObject player;
    public float camera_depth = -10;
    public float smooth_position = 0.5f;
    public float smooth_rotation = 0.5f;
    public float rotation_cutoff = 0.02f;
    public float rotation_speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    //Fixed updates
    private void FixedUpdate()
    {
        //setting vectors
        Vector3 playerDepth = new Vector3(player.transform.position.x, player.transform.position.y, camera_depth);
        Vector3 camerPos = new Vector3(this.transform.position.x, this.transform.position.y, camera_depth);
        //setting position
        this.transform.position = Vector3.Slerp(playerDepth, camerPos, smooth_position * Time.fixedDeltaTime);
        //setting quaternion
        Quaternion playerRot = player.transform.rotation;
        Quaternion cameraRot = this.transform.rotation;
        Quaternion rotDif = playerRot * Quaternion.Inverse(cameraRot);
        Debug.Log("player: " + playerRot);
        Debug.Log("camera: " + cameraRot);
        Debug.Log("Dif: " + Mathf.Abs(rotDif.z));
        //setting roations
        //need to limit the rotation speed because it's fast as hell
        if (rotDif.z <= -rotation_cutoff)
        {
            Quaternion leftturn = new Quaternion(cameraRot.x, cameraRot.y, cameraRot.z - rotation_speed * Time.fixedDeltaTime, cameraRot.w);
            this.transform.rotation = Quaternion.Slerp(playerRot, leftturn, smooth_rotation * Time.fixedDeltaTime);
        }
        if (rotDif.z >= rotation_cutoff)
        {
            Quaternion rightturn = new Quaternion(cameraRot.x, cameraRot.y, cameraRot.z + rotation_speed * Time.fixedDeltaTime, cameraRot.w);
            this.transform.rotation = Quaternion.Slerp(playerRot, rightturn, smooth_rotation * Time.fixedDeltaTime);
        }
        /*
        if (rotDif.z <= -rotation_cutoff || rotDif.z! >= rotation_cutoff)
        {
            this.transform.rotation = Quaternion.Slerp(playerRot, cameraRot, smooth_rotation * Time.fixedDeltaTime);
        }
        */
    }
}
