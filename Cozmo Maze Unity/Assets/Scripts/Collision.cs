using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    NetworkConnection net;
    Movement mov;
    // Start is called before the first frame update
    void Start()
    {
        mov = GameObject.FindWithTag("Player").GetComponent<Movement>();
        net = GameObject.FindWithTag("NetScript").GetComponent<NetworkConnection>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            //movement = 0;
            net.Send("stop");
            Debug.Log("Movement script says: Wall hit i stop to server");
        }
    }
}
