using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControlSystem : MonoBehaviour
{
    #region ���
    [Header("�b�Y")]
    public GameObject goArrow;
    [Header("�ͦ��u�]��m")]
    public Transform tranSpawPoint;
    [Header("�u�]���m��")]
    public GameObject goMarbles;
    [Header("�o�g�t��"),Range(0,5000)]
    public float speedShoot = 750;
    [Header("�g�u�n�I�����ϼh")]
    public LayerMask layerToHit;
    [Header("���շƹ���m")]
    public Transform traTestMousePosition;
    [Header("�Ҧ��u�]")]
    public List<GameObject> listMarbles = new List<GameObject>();
    [Header("�o�g���j"), Range(0, 5)]
    public float fireInterval = 0.5f;
    // �Ҧ��u�]�ƶq
    public static int allMarbles;
    // �i�H�o�g���̤j�u�]�ƶq
    public static int maxMarbles = 2;
    // �O�_��o�g
    public bool canShoot = true;
    // �C���o�g�X�h���u�]�ƶq
    public static int shootMarbles;

    public Text scoreText;

    public GameObject endUI;

    [SerializeField] private int score;
    //���������
    public Transform marbleParent;

    #endregion
    #region �ƥ�
    private void Start()
    {
        score = 0;
        for (int i = 0; i < 50; i++)  SpawnMarble();
        
    }

    private void Update()
    {
        MouseControl();
    }
    #endregion
    #region ��k
    /// <summary>
    /// �ƹ�����
    /// </summary>
    private void SpawnMarble()
    {
        allMarbles++;
        //�Ҧ��u�]�M��.�K�[(�ͦ��u�])
        GameObject marble = Instantiate(goMarbles, new Vector3(100, 0, 0), Quaternion.identity, marbleParent);
        marble.name = "�u�]" + allMarbles;

        listMarbles.Add(marble);
    }

    /// <summary>
    /// �ƹ�����
    /// </summary>
    private void MouseControl()
    {
        if (!canShoot) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            goArrow.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 v3Mouse = Input.mousePosition;

            //print("�ƹ��y��" + v3Mouse);

            // �g�u = �D�n��v��.�ù��y����g�u(�ƹ��y��)
            Ray rayMouse = Camera.main.ScreenPointToRay(v3Mouse);
            // �g�u�I����T
            RaycastHit hit;

            // �p�G �g�u���쪫��N�B�z
            // ���z �g�u�I��(�g�Z�A���u)
            if (Physics.Raycast(rayMouse, out hit, 100, layerToHit))
            {
                print("�ƹ��g�u���쪫��~" + hit.collider.name);

                Vector3 hitPosition = hit.point;               //���o�I����T���y��
                hitPosition.y = 0.5f;                          //�վ㰪�׶b�V
                traTestMousePosition.position = hitPosition;   //��s���ժ���y��

                //���⪺z�b = ���ժ��󪺮y�� - ���⪺�y��(�V�q)
                transform.forward = traTestMousePosition.position - transform.position;
            }       
            

        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StartCoroutine(FireMarble());
            canShoot = false;
        }
 

    }

    /// <summary>
    /// �o�g�u�]
    /// </summary>
    private IEnumerator FireMarble()
    {
        shootMarbles = 0;

        for (int i = 0; i < maxMarbles; i++)
        {
            shootMarbles++;
            GameObject temp = listMarbles[i];    //�ͦ��u�]
            temp.transform.position = tranSpawPoint.position;
            temp.transform.rotation = tranSpawPoint.rotation;
            temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            temp.GetComponent<Rigidbody>().AddForce(tranSpawPoint.forward * speedShoot);                 //�o�g�u�]
            yield return new WaitForSeconds(fireInterval);
        }
        goArrow.SetActive(false);

    }

    public void AddScore() 
    { 
        score++;
        scoreText.text = $"SCORE : { score }";
        if (score >= 10)
        {
            GameOver(true);
        }
    }

    public void GameOver(bool isWin) 
    {
        endUI.SetActive(true);
        Text text = endUI.GetComponentInChildren<Text>();

        if (isWin) text.text = $"You\n Win";
        else text.text = $"Game\n Over";

        canShoot = false;
        //StopAllCoroutines();
    }
    
    #endregion

}
