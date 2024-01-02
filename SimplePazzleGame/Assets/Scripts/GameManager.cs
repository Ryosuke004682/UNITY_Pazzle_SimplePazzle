using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject>  pieces;
    [SerializeField] private int shuffleCount;
        
    List<Vector2> startPosition;
    GameObject   nowSelectPiece;

    private void Start( )
    {
        startPosition = new List<Vector2>();
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
            RaycastHit2D raycast = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (raycast)
            {
                GameObject hitPiese = raycast.collider.gameObject;

                if (nowSelectPiece == null) 
                { 
                    nowSelectPiece = hitPiese;
                }
                else
                {
                    //�񖇖ڂ͈ʒu�����ւ��āA�I����Ԃ�����
                    (hitPiese.transform.position, nowSelectPiece.transform.position)
                        = (nowSelectPiece.transform.position, hitPiese.transform.position);

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

            (pieces[indexA].transform.position, pieces[indexB].transform.position)
                = (pieces[indexB].transform.position, pieces[indexA].transform.position);
        }
    }


    /// <summary>
    /// �N���A���菈��
    /// </summary>
    private bool IsClear()
    {
        for(int i = 0; i < pieces.Count; i++)
        {
            Vector2 position = pieces[i].transform.position;

            if (startPosition[i] != position)
            {
                return false;
            }
        }

        return true;
    }



}
