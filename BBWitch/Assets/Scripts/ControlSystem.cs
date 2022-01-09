using UnityEngine;
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
    public static int allMarbles;
    #endregion
    #region �ƥ�
    private void Start()
    {
        for (int i = 0; i < 2; i++)  SpawnMarble();
        
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
        listMarbles.Add(Instantiate(goMarbles, new Vector3(0, 0, 100), Quaternion.identity));
    }
    #endregion
    private void MouseControl()
    {
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
            // �]���I����T
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
            

        }else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StartCoroutine(FireMarble());
        }
 

    }

    private IEnumerator FireMarble()
    {
            for (int i = 0; i < listMarbles.Count; i++)
            {
                GameObject temp = listMarbles[i];    //�ͦ��u�]
                temp.transform.position = tranSpawPoint.position;
                temp.transform.rotation = tranSpawPoint.rotation;
                temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
                temp.GetComponent<Rigidbody>().AddForce(tranSpawPoint.forward * speedShoot);                 //�o�g�u�]
                yield return new WaitForSeconds(fireInterval);
            }
            goArrow.SetActive(false);

    }
    

}
