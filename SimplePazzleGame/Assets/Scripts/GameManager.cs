using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject>  pieces;
    [SerializeField] private int shuffleCount;
        
    private List<Vector2> startPosition;
    private GameObject   nowSelectPiece;

    private void Start( )
    {
        startPosition = pieces.ConvertAll
            (piece => (Vector2)piece.transform.position);

        ShufflePanel();
    }


    private void Update( )
    {
        ReplacePanel();
    }


    /// <summary>
    /// ピースを入れ替える処理
    /// </summary>
    private void ReplacePanel( )
    {
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Vector2.zero : 指定された（今回だったらクリックされた場所）の点を返す
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit)
            {
                GameObject hitPiese = hit.collider.gameObject;

                if (nowSelectPiece == null) 
                { 
                    nowSelectPiece = hitPiese;
                }
                else
                {
                    //二枚目は位置を入れ替えて、選択状態を解除
                    SwapPiecePosition(hitPiese.transform , nowSelectPiece.transform);

                    nowSelectPiece = null;

                    if(IsClear())
                    {
                        print("クリア！");
                    }
                }
            }
        }
    }


    /// <summary>
    /// 入れ替え処理
    /// </summary>
    /// <param name="pieceA"></param>
    /// <param name="pieceB"></param>
    private void SwapPiecePosition(Transform pieceA , Transform pieceB)
    {
        (pieceA.transform.position, pieceB.transform.position)
            = (pieceB.transform.position, pieceA.transform.position);
    }


    /// <summary>
    /// パネルをシャッフルしてる所。
    /// </summary>
    private void ShufflePanel()
    {
        foreach (GameObject item in pieces)
        {
            Vector2 position = item.transform.position;
            startPosition.Add(position);
        }

        //並べ替え
        for (int i = 0; i < shuffleCount; i++)
        {
            int indexA = Random.Range(0, pieces.Count);
            int indexB = Random.Range(0, pieces.Count);

            SwapPiecePosition(pieces[indexA].transform , pieces[indexB].transform);
        }
    }


    /// <summary>
    /// クリア判定処理
    /// </summary>
    private bool IsClear()
    {
        return pieces.TrueForAll
            (piece => startPosition[pieces.IndexOf(piece)] 
             == (Vector2)piece.transform.position);
    }
}
