using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float turnTime;
    public float waitTime;

    float movement;
    bool isMovable = true;
    bool noAction = true;
    bool noWall = true;

    public CharacterController controller;
    Vector3 moveDirection;
    NetworkConnection net;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        net = GameObject.FindWithTag("NetScript").GetComponent<NetworkConnection>();
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
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && noAction && noWall)
        {
            movement = 1;
            net.Send("w;F");
            noAction = false;

        }
        if ((Input.GetKeyDown("s") || Input.GetKeyDown("down")) && noAction && noWall)
        {
            movement = -1;
            net.Send("s;B");
            noAction = false;
        }
        if (Input.GetKeyUp("w") || Input.GetKeyUp("up"))
        {
            if (isMovable)
            {
                StartCoroutine(TurnWait());
                movement = 0;
                net.Send("stop");
                StartCoroutine(Wait());
            }
        }
        if (Input.GetKeyUp("s") || Input.GetKeyUp("down"))
        {
            if (isMovable)
            {
                StartCoroutine(TurnWait());
                movement = 0;
                net.Send("stop");
                StartCoroutine(Wait());
            }
        }

        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && noAction)
        {
            if (isMovable)
            {
                transform.Rotate(Vector3.up, -90);
                noAction = false;
                Debug.Log("turned left");
                net.Send("a;");
                StartCoroutine(TurnWait());
                StartCoroutine(Wait());
            }
        }
        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && noAction)
        {
            if (isMovable)
            {
                transform.Rotate(Vector3.up, 90);
                noAction = false;
                Debug.Log("turned right");
                net.Send("d;");
                StartCoroutine(TurnWait());
                StartCoroutine(Wait());
            }
        }
        // Movement of player done Here, as well as collision
        CheckMove(2);
        moveDirection = (transform.forward * movement);
        moveDirection = moveDirection.normalized * speed;

        controller.Move(moveDirection * Time.deltaTime);
    }
    public void CheckMove(int moveNum)
    {
        if (moveNum == 2) return;
        if (moveNum == 3) noWall = false;
        if (moveNum == 4) Wait();
        else movement = moveNum;

    }
    private IEnumerator TurnWait()
    {
        isMovable = false;
        yield return new WaitForSeconds(turnTime);
        Debug.Log("you can turn now");
        isMovable = true;
    }
    private IEnumerator Wait()
    {
        noAction = false;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("you have waited " + waitTime + " seconds no movement detected");
        noAction = true;
        noWall = true;
    }
}
