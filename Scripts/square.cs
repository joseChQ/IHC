using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class square : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public int ix;
    public int jx;
    public bool selected = false;
    public int color;
    public string name_square;
    public float th = 0.008f;

    public Renderer rend;
    //private PieceType tmpPieceType;

    controlGame controlScript;
    void Start()
    {
        controlScript = GameObject.FindGameObjectWithTag("pieceMovement").GetComponent<controlGame>();
        rend = GetComponent<Renderer>();
        if(name_square.Equals("under"))
        {
            rend.enabled = false;
        }
        else
        {
            rend.enabled = true;
        }
        if ((ix + jx)%2 == 1)
        {
            color = 1;
        }
        else
        {
            color = 0;
        }
    }

    public void rend_enabled(bool x)
    {
        rend = GetComponent<Renderer>();
        rend.enabled = x;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    GameObject check_square(int ixx,int jxx)
    {
        GameObject currentSquare = controlScript.squares[ixx, jxx];
        foreach(GameObject piece in controlScript.pieces)
        {
            if (piece != null && (currentSquare.transform.position - piece.transform.position).magnitude < th)
            {
                //tmpPieceType = piece.GetComponent<piece>().pieceTp;
                //return true;
                return piece;
            }
        }
        return null;
    }
    bool check_square1(int ixx, int jxx)
    {
        GameObject currentSquare = controlScript.squares[ixx, jxx];
        foreach (GameObject piece in controlScript.pieces)
        {
            if (piece != null && (currentSquare.transform.position - piece.transform.position).magnitude < th)
            {
                //tmpPieceType = piece.GetComponent<piece>().pieceTp;
                return true;
            }
        }
        return false;
    }
    bool valid(int ixx, int jxx)
    {
        return ixx >= 0 && jxx >= 0 && ixx < 8 && jxx < 8;
    }
    void possible_movement(GameObject tmp_piece, int i_tmp, int j_tmp, bool r)
    {
        GameObject possible_enemy;
        if (valid(i_tmp, j_tmp))
        {
            possible_enemy = check_square(i_tmp, j_tmp);
            if ((possible_enemy == null && r) || possible_enemy != null && possible_enemy.GetComponent<piece>().color != tmp_piece.GetComponent<piece>().color)
            {
                square tmp = controlScript.squares[i_tmp, j_tmp].GetComponent<square>();
                tmp.selected = true;
                tmp.rend_enabled(false);
                square tmp1 = controlScript.under_squares[i_tmp, j_tmp].GetComponent<square>();
                tmp1.rend_enabled(true);
                
            }
        }
    }

    void pawn_possible_movement(GameObject tmp_piece, int i_tmp, int j_tmp)
    {
        GameObject possible_enemy;
        if (valid(i_tmp, j_tmp))
        {
            possible_enemy = check_square(i_tmp, j_tmp);
            if (possible_enemy == null)
            {
                square tmp = controlScript.squares[i_tmp, j_tmp].GetComponent<square>();
                tmp.selected = true;
                tmp.rend_enabled(false);
                square tmp1 = controlScript.under_squares[i_tmp, j_tmp].GetComponent<square>();
                tmp1.rend_enabled(true);

            }
        }
    }
    void diagonal_move(GameObject tmp_piece, int tmp_ix, int tmp_jx, int i_v, int j_v)
    {
        bool ok = true;
        while(valid(tmp_ix, tmp_jx) && ok)
        {
            GameObject possible_piece = null;
            possible_movement(tmp_piece, tmp_ix, tmp_jx, true);
            possible_piece = check_square(tmp_ix, tmp_jx);
            if (possible_piece != null)
            {
                ok = false;
            }
            tmp_ix += i_v;
            tmp_jx += j_v;
        }
    }
    void selected_square()
    {
        GameObject tmp_piece = check_square(ix,jx);
        if (tmp_piece != null)
        {
            //       #                  PAWN                  #
            if(tmp_piece.GetComponent<piece>().pieceTp == PieceType.pawn)
            {
                if(tmp_piece.GetComponent<piece>().color == 0)
                {
                    possible_movement(tmp_piece, ix + 1, jx - 1, false);
                    possible_movement(tmp_piece, ix + 1, jx + 1, false);
                    pawn_possible_movement(tmp_piece, ix + 1, jx);
                    if(tmp_piece.GetComponent<piece>().first && valid(ix+2,jx) && check_square1(ix+1,jx) == false)
                    {
                    //    tmp_piece.GetComponent<piece>().first = false;
                        pawn_possible_movement(tmp_piece, ix + 2, jx);
                    }
                }
                else ///#color negro
                {
                    possible_movement(tmp_piece, ix - 1, jx - 1, false);
                    possible_movement(tmp_piece, ix - 1, jx + 1, false);
                    pawn_possible_movement(tmp_piece, ix - 1, jx);
                    if (tmp_piece.GetComponent<piece>().first && valid(ix - 2, jx) && !check_square1(ix - 1, jx))
                    {
                    //    tmp_piece.GetComponent<piece>().first = false;
                        pawn_possible_movement(tmp_piece, ix - 2, jx);
                    }
                }
            } //         #          CABALLO        #
            else if(tmp_piece.GetComponent<piece>().pieceTp == PieceType.knight)
            {
                possible_movement(tmp_piece, ix + 2, jx - 1, true);
                possible_movement(tmp_piece, ix + 2, jx + 1, true);
                possible_movement(tmp_piece, ix - 2, jx - 1, true);
                possible_movement(tmp_piece, ix - 2, jx + 1, true);

                possible_movement(tmp_piece, ix - 1, jx + 2, true);
                possible_movement(tmp_piece, ix + 1, jx + 2, true);
                possible_movement(tmp_piece, ix - 1, jx - 2, true);
                possible_movement(tmp_piece, ix + 1, jx - 2, true);
            } //  #             TORRE               #
            else if(tmp_piece.GetComponent<piece>().pieceTp == PieceType.rock)    
            {
                diagonal_move(tmp_piece, ix, jx - 1, 0, -1);
                diagonal_move(tmp_piece, ix, jx + 1, 0, 1);
                diagonal_move(tmp_piece, ix - 1, jx, -1, 0);
                diagonal_move(tmp_piece, ix + 1, jx, 1, 0);
            }
            else if(tmp_piece.GetComponent<piece>().pieceTp == PieceType.bishop)
            {
                diagonal_move(tmp_piece, ix - 1, jx - 1, -1, -1);
                diagonal_move(tmp_piece, ix + 1, jx + 1, 1, 1);
                diagonal_move(tmp_piece, ix - 1, jx + 1, -1, 1);
                diagonal_move(tmp_piece, ix + 1, jx - 1, 1, -1);

            }
            else if(tmp_piece.GetComponent<piece>().pieceTp == PieceType.queen)
            {
                // linea recta
                diagonal_move(tmp_piece, ix, jx - 1, 0, -1);
                diagonal_move(tmp_piece, ix, jx + 1, 0, 1);
                diagonal_move(tmp_piece, ix - 1, jx, -1, 0);
                diagonal_move(tmp_piece, ix + 1, jx, 1, 0);

                //
                diagonal_move(tmp_piece, ix - 1, jx - 1, -1, -1);
                diagonal_move(tmp_piece, ix + 1, jx + 1, 1, 1);
                diagonal_move(tmp_piece, ix - 1, jx + 1, -1, 1);
                diagonal_move(tmp_piece, ix + 1, jx - 1, 1, -1);
            }
            else if(tmp_piece.GetComponent<piece>().pieceTp == PieceType.king)
            {
                possible_movement(tmp_piece, ix + 1, jx - 1, true);
                possible_movement(tmp_piece, ix + 1, jx + 1, true);
                possible_movement(tmp_piece, ix - 1, jx - 1, true);
                possible_movement(tmp_piece, ix - 1, jx + 1, true);

                possible_movement(tmp_piece, ix, jx - 1, true);
                possible_movement(tmp_piece, ix, jx + 1, true);
                possible_movement(tmp_piece, ix - 1, jx, true);
                possible_movement(tmp_piece, ix + 1, jx, true);
            }

            controlScript.last_ix = ix;
            controlScript.last_jx = jx;
        }
        else
        {
            controlScript.last_ix = -1;
            controlScript.last_jx = -1;
        }

    }
    bool check_selected_state(int ixx, int jxx)
    {
        square currentSquare = controlScript.squares[ixx, jxx].GetComponent<square>();
        return currentSquare.selected;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (check_selected_state(ix,jx))
        {
            controlScript.current_ix = ix;
            controlScript.current_jx = jx;
            RaiseAction RaiseActionScript = GameObject.FindGameObjectWithTag("pieceMovement").GetComponent<RaiseAction>();
            RaiseActionScript.SendMovePos(controlScript.last_ix, controlScript.last_jx, controlScript.current_ix, controlScript.current_jx);
            controlScript.current_ix = -1;
            controlScript.current_jx = -1;
            controlScript.last_ix = -1;
            controlScript.last_jx = -1;
            controlScript.rstart_ssquare();
        }
        else
        {
            controlScript.rstart_ssquare();
            selected_square();
        }
    }

    public void voice_fun()
    {
        if (check_selected_state(ix, jx))
        {
            controlScript.current_ix = ix;
            controlScript.current_jx = jx;
            RaiseAction RaiseActionScript = GameObject.FindGameObjectWithTag("pieceMovement").GetComponent<RaiseAction>();
            RaiseActionScript.SendMovePos(controlScript.last_ix, controlScript.last_jx, controlScript.current_ix, controlScript.current_jx);
            controlScript.current_ix = -1;
            controlScript.current_jx = -1;
            controlScript.last_ix = -1;
            controlScript.last_jx = -1;
            controlScript.rstart_ssquare();
        }
        else
        {
            controlScript.rstart_ssquare();
            selected_square();
        }
    }

}
