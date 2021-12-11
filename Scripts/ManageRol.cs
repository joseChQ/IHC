using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class ManageRol : MonoBehaviour
{
    // Start is called before the first frame update
    public void addrol(string tmp)
    {
        PhotonNetwork.NickName += tmp;
    }
}
