using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;
public class ControlBoton : MonoBehaviour
{
	const string LANG_CODE = "es-ES";

	[SerializeField]
	Text uiText;
	[SerializeField]
	Text uiText1;

	PieceMovement piecemovScript;

	public int turno;
	public float th = 0.008f;

	void Start()
	{
		Setup(LANG_CODE);

		SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;


		SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
		piecemovScript = GameObject.FindGameObjectWithTag("pieceMovement").GetComponent<PieceMovement>();
		CheckPermission();
	}
	void CheckPermission()
	{

		if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
		{
			Permission.RequestUserPermission(Permission.Microphone);
		}

	}

	#region Speech to text
	public void StartListening()
	{
		SpeechToText.instance.StartRecording();
	}
	public void StopListening()
	{
		SpeechToText.instance.StopRecording();
	}
	void OnFinalSpeechResult(string result)
	{
		instance(result);
		uiText1.text = result;

	}
	void OnPartialSpeechResult(string result)
	{
		instance(result);
		uiText1.text = result;
	}
	#endregion
	void Setup(string code)
	{
	//	TextToSpeech.instance.Setting(code, 1, 1);
		SpeechToText.instance.Setting(code);
	}
	public  void IsOccupied(GameObject sq)
	{
		//bool occupied = false;

		turno = piecemovScript.playerTurn;
		foreach (Piece pc in piecemovScript.allPieces)
		{
			int color = pc.GetComponent<Piece>().color;
			if ((pc.transform.position - sq.transform.position).magnitude < th && color == turno )
            {
				pc.GetComponent<Piece>().onPieceClick(color);
			}	
		}

		//return occupied;
	}
	void instance(string name_square)
    {
		string name_square1 = name_square.ToLower();
		GameObject choosed_square= null;
		string tmp = "Comando no permitido";

		
		foreach (GameObject sq in piecemovScript.squares)
        {
			string my_name = sq.GetComponent<ChessSquare>().name_square;
			if (my_name.Equals(name_square1))
            {
				tmp = "...";
				choosed_square = sq;
			}
        }
		uiText.text = tmp;
		if (choosed_square != null)
        {
			if(choosed_square.GetComponent<ChessSquare>().onClickSquare() == false)
            {
				IsOccupied(choosed_square);
			}	
		}

	}
}
