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
    /// �s�[�X�����ւ��鏈��
    /// </summary>
    private void ReplacePanel( )
    {
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Vector2.zero : �w�肳�ꂽ�i���񂾂�����N���b�N���ꂽ�ꏊ�j�̓_��Ԃ�
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
                    //�񖇖ڂ͈ʒu�����ւ��āA�I����Ԃ�����
                    SwapPiecePosition(hitPiese.transform , nowSelectPiece.transform);

                    nowSelectPiece = null;

                    if(IsClear())
                    {
                        print("�N���A�I");
                    }
                }
            }
        }
    }


    /// <summary>
    /// ����ւ�����
    /// </summary>
    /// <param name="pieceA"></param>
    /// <param name="pieceB"></param>
    private void SwapPiecePosition(Transform pieceA , Transform pieceB)
    {
        (pieceA.transform.position, pieceB.transform.position)
            = (pieceB.transform.position, pieceA.transform.position);
    }


    /// <summary>
    /// �p�l�����V���b�t�����Ă鏊�B
    /// </summary>
    private void ShufflePanel()
    {
        foreach (GameObject item in pieces)
        {
            Vector2 position = item.transform.position;
            startPosition.Add(position);
        }

        //���בւ�
        for (int i = 0; i < shuffleCount; i++)
        {
            int indexA = Random.Range(0, pieces.Count);
            int indexB = Random.Range(0, pieces.Count);

            SwapPiecePosition(pieces[indexA].transform , pieces[indexB].transform);
        }
    }


    /// <summary>
    /// �N���A���菈��
    /// </summary>
    private bool IsClear()
    {
        return pieces.TrueForAll
            (piece => startPosition[pieces.IndexOf(piece)] 
             == (Vector2)piece.transform.position);
    }
}
