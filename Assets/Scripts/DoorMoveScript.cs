using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMoveScript : MonoBehaviour
{

    private Vector3 LeftDoorOpenPosition; //new Vector3(-1.90f,-0.0759999976f,8.66300011f);
    private Vector3 LeftDoorClosePosition; // new Vector3(-0.25f,-0.0759999976f,8.66300011f);

    private Vector3 RightDoorOpenPosition;
    private Vector3 RightDoorClosePosition;

    public float DoorRange = 1.0f;

    public float DoorSpeed = 0.5f;

    public GameObject DoorLeft;
    public GameObject DoorRight;

    public bool Opening = true;

    // Start is called before the first frame update
    void Start()
    {
        //init left
        LeftDoorClosePosition = DoorLeft.transform.position;
        LeftDoorOpenPosition = DoorLeft.transform.position;
        LeftDoorOpenPosition.x = LeftDoorOpenPosition.x - DoorRange;

        //init right
        RightDoorClosePosition = DoorRight.transform.position;
        RightDoorOpenPosition = DoorRight.transform.position;
        RightDoorOpenPosition.x = RightDoorOpenPosition.x + DoorRange;
    }

    // Update is called once per frame
    void Update()
    {
        if(DoorLeft == null) {
            return;
        }
        MoveDoors();
    }

    void MoveDoors() {
        var speed = DoorSpeed * Time.deltaTime;
        if(Opening) {
            OpenLeftDoor(speed);
            OpenRightDoor(speed);
        } else {
            CloseLeftDoor(speed);
            CloseRightDoor(speed);
        }
    }

    private void OpenLeftDoor(float speed) {
        //Debug.Log(DoorLeft.transform.position.x);
        DoorLeft.transform.position = Vector3.MoveTowards(DoorLeft.transform.position, LeftDoorOpenPosition, speed);
    }

     private void OpenRightDoor(float speed) {
        //Debug.Log(DoorRight.transform.position.x);
        DoorRight.transform.position = Vector3.MoveTowards(DoorRight.transform.position, RightDoorOpenPosition, speed);
    }

    private void CloseLeftDoor(float speed) {
        //Debug.Log(DoorLeft.transform.position.x);
        DoorLeft.transform.position = Vector3.MoveTowards(DoorLeft.transform.position, LeftDoorClosePosition, speed);
    }

    private void CloseRightDoor(float speed) {
        DoorRight.transform.position = Vector3.MoveTowards(DoorRight.transform.position, RightDoorClosePosition, speed);
    }

    public void OpenTheDoors() {
        Opening = true;
    }

    public void CloseTheDoors() {
        Opening = false;
    }
}
