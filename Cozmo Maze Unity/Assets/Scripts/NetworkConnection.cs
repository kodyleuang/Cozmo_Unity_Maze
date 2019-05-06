using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

public class NetworkConnection : MonoBehaviour
{
    TcpClient s = new TcpClient();
    private void Awake()
    {
        s.Connect("10.0.1.10", 5000);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Send(string x)
    {
        
        Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ipAdd = System.Net.IPAddress.Parse("10.0.1.10");
        IPEndPoint remoteEP = new IPEndPoint(ipAdd, 5000);

        soc.Connect(remoteEP);
        byte[] byData = System.Text.Encoding.ASCII.GetBytes(x);
        soc.Send(byData);
        Debug.Log(x + " Has been sent to python from network Connect");
    }
}
