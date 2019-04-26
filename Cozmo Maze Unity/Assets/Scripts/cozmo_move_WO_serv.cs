using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cozmo_move_WO_serv : MonoBehaviour
{
    public float speed;
    public float turnTime;
    public float waitTime;

    float movement;
    bool isMovable = true;
    bool noAction = true;

    public CharacterController controller;
    Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*Movement options for WASD and Arrow Keys, Sending input to the connected Server
        Sends the key pressed, the action it should take (Forward or Backwards) and the 
        distance Cozmo should move.
         */

        //make is moveable bool for W and S keys
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && noAction)
        {
            movement = 1;
            noAction = false;
        }
        if ((Input.GetKeyDown("s") || Input.GetKeyDown("down")) && noAction)
        {
            movement = -1;
            noAction = false;
        }
        if (Input.GetKeyUp("w") || Input.GetKeyUp("up"))
        {
            if (isMovable)
            {
                StartCoroutine(MoveWait());
                movement = 0;
                StartCoroutine(Wait());
            }
        }
        if (Input.GetKeyUp("s") || Input.GetKeyUp("down"))
        {
            if (isMovable)
            {
                StartCoroutine(MoveWait());
                movement = 0;
                StartCoroutine(Wait());
            }
        }

        //Movement options for Left and Right Turning
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && noAction)
        {
            if (isMovable)
            {
                transform.Rotate(Vector3.up, -90);
                Debug.Log("turned left");
                noAction = false;
                StartCoroutine(MoveWait());
                StartCoroutine(Wait());
            }
        }
        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && noAction)
        {
            if (isMovable)
            {
                transform.Rotate(Vector3.up, 90);
                Debug.Log("turned right");
                noAction = false;
                StartCoroutine(MoveWait());
                StartCoroutine(Wait());
            }
        }
        //CheckMove(2);
        moveDirection = (transform.forward * movement);
        moveDirection = moveDirection.normalized * speed;

        controller.Move(moveDirection * Time.deltaTime);
    }
    /* public void CheckMove(int moveNum)
     {
         if (moveNum == 2) return;
         else movement = moveNum;
     }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            movement = 0;
            Debug.Log("Coz no serv says: Wall hit i stop");
        }
    }

    private IEnumerator MoveWait()
    {
        isMovable = false;
        yield return new WaitForSeconds(turnTime);
        Debug.Log("you can move now");
        isMovable = true;
    }
    private IEnumerator Wait()
    {
        noAction = false;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("you have waited " + waitTime+ " seconds no movement detected");
        noAction = true;
    }
}
