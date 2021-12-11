using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class ControlName : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Text uiText;
    
    void Start()
    {
        string n_name = PhotonNetwork.NickName;
        //uiText.text = n_name;
        if (n_name.Length > 0)
        {
            string tmp_name = "";
            for (int i = 0; i < n_name.Length - 2; ++i)
            {
                tmp_name += n_name[i];
            }
            if (n_name[n_name.Length - 1] == '0')
            {
                tmp_name = tmp_name + " " + "( rol : " + "capitan" + " )";
            }
            else
            {
                tmp_name = tmp_name + " " + "( rol: " + "colaborador" + " )";
            }
            uiText.text = tmp_name;
        }


    }
}
