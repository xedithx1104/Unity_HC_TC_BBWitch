using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �^�X���
    /// </summary>
    public Turn turn = Turn.My;

    [Header("�Ĥ�^�X�ƥ�")]
    public UnityEvent onEnemyTurn;
    [Header("�Ǫ��}�C")]
    public GameObject[] goEnemy;
    [Header("�u�]")]
    public GameObject goMarble;
    [Header("�ѽL�s��")]
    public Transform traCheckboard;
    [Header("�ͦ��ƶq�̤p�̤j��")]
    public Vector2Int v2RandomEnemyCount = new Vector2Int(1, 4);
    [SerializeField]
    private Transform[] traCheckboards;
    [SerializeField]
    private Transform[] traColmnFirst;
    private int countRow = 4;
    [SerializeField]
    private List<int> indexColumnSecond = new List<int>();
    private ControlSystem controlSystem;

    //���������
    public Transform monsterParent;
   
    private void Awake()
    {
        //�ѽL�}�C = �ѽL�s��.���o�l���󪺤���<�ܧΤ���>()
        traCheckboards = traCheckboard.GetComponentsInChildren<Transform>();
        traColmnFirst = new Transform[countRow];
        for (int i = 0; i < countRow; i++)
        {
            traColmnFirst[i] = traCheckboards[i + 1];
        }

        controlSystem = FindObjectOfType<ControlSystem>();

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        int countEnemy = Random.Range(v2RandomEnemyCount.x, v2RandomEnemyCount.y);

        indexColumnSecond.Clear();

        for (int i = 0; i < 4; i++) indexColumnSecond.Add(i);
       
        for (int i = 0; i < countEnemy; i++)
        {
            int randomEnemy = Random.Range(0, goEnemy.Length);
            
            int randomColumnSecond = Random.Range(0, indexColumnSecond.Count);
            
            Instantiate(goEnemy[randomEnemy], traColmnFirst[indexColumnSecond[randomColumnSecond]].position, Quaternion.identity, monsterParent);

            indexColumnSecond.RemoveAt(randomColumnSecond);
        }

        int randomMarble = Random.Range(0, indexColumnSecond.Count);
        Instantiate(
            goMarble,
            traColmnFirst[indexColumnSecond[randomMarble]].position + Vector3.up,
            Quaternion.identity,
            monsterParent);
            
    }

    private bool canSpawn = true;

    /// <summary>
    /// �����^�X
    /// </summary>
    public void SwitchTurn(bool isMyTurn)
    {
        if (isMyTurn) 
        { 
            RecycleMarble.recycleMarbles = 0;
            turn = Turn.My;
            controlSystem.canShoot = true;
            if (canSpawn)
            {
                canSpawn = false;
                Invoke("SpawnEnemy", 0.8f);
            }
        }
        else
        {
            canSpawn = true;
            turn = Turn.Enemy;
            onEnemyTurn.Invoke();
        }
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("�}�Y");
    }
}

public enum Turn
{
    My,Enemy
}
