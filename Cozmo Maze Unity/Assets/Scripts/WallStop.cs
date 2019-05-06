using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStop : MonoBehaviour
{
    Movement mov;
    Move_WO_server movWO;
    NetworkConnection net;
    cozmo_move_WO_serv coz;
    // Start is called before the first frame update
    void Start()
    {
        mov = GameObject.FindWithTag("Player").GetComponent<Movement>();
        coz = GameObject.FindWithTag("Player").GetComponent<cozmo_move_WO_serv>();
        net = GameObject.FindWithTag("NetScript").GetComponent<NetworkConnection>();
        movWO = GameObject.FindWithTag("Player").GetComponent<Move_WO_server>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Front")
        {
            mov.CheckMove(0);
            mov.CheckMove(3);
            //movWO.CheckMove(0);
            coz.CheckMove(0);
            coz.CheckMove(3);
            net.Send("stop");
            Debug.Log("WallStop says: i sent stop in front");
        }
        else if (other.tag == "Player_Back")
        {
            mov.CheckMove(0);
            mov.CheckMove(3);
            net.Send("stop");
            Debug.Log("WallStop says: i sent stop for back");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player_Front")
        {
            mov.CheckMove(0);
            if (Input.GetKeyDown("s"))
            {
                Debug.Log("your moving back");
                mov.CheckMove(4);
                mov.CheckMove(-1);
                coz.CheckMove(4);
                coz.CheckMove(-1);
            }
            if (Input.GetKeyDown("w"))
            {
                Debug.Log("forward movement not allowed");
                mov.CheckMove(3);
                coz.CheckMove(3);
            }
        }
        if (other.tag == "Player_Back")
        {
            mov.CheckMove(0);
            if (Input.GetKeyDown("w"))
            {
                Debug.Log("your moving forward");
                mov.CheckMove(4);
                mov.CheckMove(1);
                coz.CheckMove(4);
                coz.CheckMove(1);
            }
            if (Input.GetKeyDown("s"))
            {
                Debug.Log("backward movement not allowed");
                mov.CheckMove(3);
                coz.CheckMove(3);
            }
        }
    }

}