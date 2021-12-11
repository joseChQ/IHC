using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class RaiseAction : MonoBehaviour, IOnEventCallback
{
    // If you have multiple custom events, it is recommended to define them in the used class
    public const byte evento1 = 1;
    public const byte evento2 = 0;
    controlGame controlScript;

    int color_ = 1;
    void Start()
    {
        controlScript = GetComponent<controlGame>();
        string n_name = PhotonNetwork.NickName;

        if (n_name.Length > 0)
        {
            if (n_name[n_name.Length - 2] == '2')
            {
                color_ = 2;
            }
        }
    }

    public void SendMovePos(int i1, int j1, int i2, int j2)
    {
        byte evento_tmp = 0;
        if(controlScript.capitan)
        {
            evento_tmp = 1;
        }
        object[] content = new object[] { i1, j1, i2, j2, color_ }; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(evento_tmp, content, raiseEventOptions, SendOptions.SendReliable);
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == evento1)
        {
            object[] data = (object[])photonEvent.CustomData;
            int i1 = (int)data[0];
            int j1 = (int)data[1];
            int i2 = (int)data[2];
            int j2 = (int)data[3];
            controlScript.action(i1, j1, i2, j2);
        }
        else if(eventCode == evento2)
        {
            object[] data = (object[])photonEvent.CustomData;
            int i1 = (int)data[0];
            int j1 = (int)data[1];
            int i2 = (int)data[2];
            int j2 = (int)data[3];
            int color_tmp = (int)data[4];
            if(color_tmp == color_ )
            {
                controlScript.action_v(i1, j1, i2, j2);
            }
        }
        
    }
}