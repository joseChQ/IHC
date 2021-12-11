using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class controlGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[,] squares = new GameObject[8,8];
    public GameObject[,] under_squares = new GameObject[8, 8];
    public GameObject[] pieces;
    public GameObject[] pieces_v;
    public int last_ix;
    public int last_jx;
    public int current_ix;
    public int current_jx;
    public float th = 0.008f;
    public bool capitan = false;
    public float pos_v = -20;

    
    int turno = 0;

    [SerializeField]
    Text uiText;

    void Start()
    {
        last_ix = -1;
        last_jx = -1;
        GameObject[] squares_tmp= GameObject.FindGameObjectsWithTag("ssquare");
        for(int i = 0; i < squares_tmp.Length;++i)
        {
            int ix = squares_tmp[i].GetComponent<square>().ix;
            int jx = squares_tmp[i].GetComponent<square>().jx;
            squares[ix,jx] = squares_tmp[i];
        }

        GameObject[] under_squares_tmp = GameObject.FindGameObjectsWithTag("underssq");
        for (int i = 0; i < under_squares_tmp.Length; ++i)
        {
            int ix = under_squares_tmp[i].GetComponent<square>().ix;
            int jx = under_squares_tmp[i].GetComponent<square>().jx;
            under_squares[ix, jx] = under_squares_tmp[i];
            //under_squares[ix, jx].SetActive(false);
        }

        pieces = GameObject.FindGameObjectsWithTag("piece");
        pieces_v = GameObject.FindGameObjectsWithTag("piece_v");

        string n_name = PhotonNetwork.NickName;
        //uiText.text = n_name;
        if (n_name.Length > 0)
        {
            if (n_name[n_name.Length - 1] == '0')
            {
                capitan = true;
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void rstart_ssquare()
    {
        for(int i = 0; i < 8;++i)
        {
            for(int j =0; j < 8;++j)
            {
                square tmp = squares[i, j].GetComponent<square>();
                tmp.rend_enabled(true);
                tmp.selected = false;
                square tmp1 = under_squares[i, j].GetComponent<square>();
                tmp1.rend_enabled(false);
            }
        }
        Vector3 tmp11 = new Vector3(pos_v, 0.0f, pos_v);
        for(int i = 0; i < pieces_v.Length;++i)
        {
            pieces_v[i].transform.position = tmp11;
        }
        foreach(GameObject piece in pieces)
        {
            if(piece != null)
            {
                piece.GetComponent<piece>().rend_enabled(true);
            }
        }
    }
    
    public GameObject check_square(int ixx, int jxx)
    {
        GameObject currentSquare = squares[ixx, jxx];
        foreach (GameObject piece in pieces)
        {
            if (piece!=null && (currentSquare.transform.position - piece.transform.position).magnitude < th)
            {
                return piece;
            }
        }
        return null;
    }
    public void action(int i1, int j1, int i2, int j2)
    {
        GameObject current_piece = check_square(i1, j1);
        GameObject possible_enemy = check_square(i2, j2);
        if( i1 == -1 || j1 == -1 || i2 == -1 || j2  == -1)
        {
            Debug.Log("error");
        }
        current_piece.transform.position = squares[i2, j2].transform.position;
        current_piece.GetComponent<piece>().first = false;
        if (possible_enemy != null)
        {
            //possible_enemy.SetActive(false);
            Destroy(possible_enemy.gameObject);
        }
        turno ^= 1;
        if(turno == 1)
        {
            uiText.text = "Turno: Negras";
        }
        else
        {
            uiText.text = "Turno: Blancas";
        }

    }
    public void action_v(int i1, int j1, int i2, int j2)
    {
        GameObject current_piece = check_square(i1, j1);
        GameObject possible_enemy = check_square(i2, j2);
        if (i1 == -1 || j1 == -1 || i2 == -1 || j2 == -1)
        {
            Debug.Log("error");
        }
        if (possible_enemy != null)
        {
            possible_enemy.GetComponent<piece>().rend_enabled(false);
            //Destroy(possible_enemy.gameObject);
        }
        foreach (GameObject piece in pieces_v)
        {
            if(piece.GetComponent<piece>().pieceTp == current_piece.GetComponent<piece>().pieceTp)
            {
                piece.transform.position = squares[i2, j2].transform.position;
                break;
            }
        }
    }
}
