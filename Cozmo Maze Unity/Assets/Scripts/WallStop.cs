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
        net = GetComponent<NetworkConnection>();
        movWO = GameObject.FindWithTag("Player").GetComponent<Move_WO_server>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mov.CheckMove(0);
            //movWO.CheckMove(0);
            //coz.CheckMove(0);
            net.Send("stop");
            Debug.Log("i sent stop");
        }
    }
    
}