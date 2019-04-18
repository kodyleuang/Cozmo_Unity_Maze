using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_WO_server : MonoBehaviour
{
    public float speed;
    public float turnTime;
    public CharacterController controller;

    float waitTime;
    float movement;
    bool isTurnable = true;

    Vector3 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        movement = 0;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Movement options for WASD and Arrow Keys, Sending input to the connected Server
        Sends the key pressed, the action it should take (Forward or Backwards) and the 
        distance Cozmo should move.
         */

        //Mathf.Approximately(PlayerRB.velocity.y, 0 <- use to detect if player is moving to not have double inputs
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")))
        {
            movement = 1;
        }
        if ((Input.GetKeyDown("s") || Input.GetKeyDown("down")))
        {
            movement = -1;
        }
        if (Input.GetKeyUp("w") || Input.GetKeyUp("s") || Input.GetKeyUp("down") || Input.GetKeyUp("up"))
        {
            movement = 0;
        }

        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")))
        {
            if (isTurnable)
            {
                transform.Rotate(Vector3.up, -90);
                StartCoroutine(TurnWait());
            }
        }
        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")))
        {
            if (isTurnable)
            {
                transform.Rotate(Vector3.up, 90);
                StartCoroutine(TurnWait());
            }
        }
        CheckMove(2);
        moveDirection = (transform.forward * movement );
        moveDirection = moveDirection.normalized * speed;

        controller.Move(moveDirection * Time.deltaTime);
    }
    public void CheckMove(int moveNum)
    {
        if (moveNum == 2) return;
        else
        {
            movement = moveNum;
            Debug.Log("i changed it");
        }
    }
    private IEnumerator TurnWait()
    {
        isTurnable = false;
        yield return new WaitForSeconds(turnTime);
        isTurnable = true;
    }
}
