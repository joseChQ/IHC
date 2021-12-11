using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this enum holds the value/type of the piece
public enum PieceType
{
    rock,
    pawn,
    bishop,
    king,
    queen,
    knight
}
public class piece : MonoBehaviour
{
    // Start is called before the first frame update
    // Use this for initialization
    public PieceType pieceTp;

    public string name;
    public int color;
    public bool first;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        if (pieceTp == PieceType.pawn)
        {
            first = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void rend_enabled(bool x)
    {
        rend = GetComponent<Renderer>();
        rend.enabled = x;
    }

}
