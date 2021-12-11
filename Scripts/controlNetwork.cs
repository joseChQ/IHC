using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
namespace Photon.Pun.Demo.PunBasics
{
    
    public class controlNetwork : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        public Camera m_CameraOne;
        public Camera m_CameraTwo;
        public GameObject microfono1;
        public GameObject microfono2;
        //private string roll;
        void Start()
        {
            string n_name = PhotonNetwork.NickName;
            //uiText.text = n_name;
            if(n_name.Length >0)
            {
                if (n_name[n_name.Length -2] == '1')
                {
                    Destroy(m_CameraOne.gameObject);
                    m_CameraTwo.enabled = true;
                }
                else
                {
                    Destroy(m_CameraTwo.gameObject);
                    m_CameraOne.enabled = true;
                }
             
            }
            if(microfono1 != null)
            {
                if (n_name[n_name.Length - 2] == '1')
                {
                    Destroy(microfono1.gameObject);
                }
                else
                {
                    Destroy(microfono2.gameObject);
                }
            }
      
            
             
            

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        

    }

}
