using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class movespere : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        float  x = 0.15f;
        transform.position = transform.position + new Vector3(x, 0, 0);
    }
}
