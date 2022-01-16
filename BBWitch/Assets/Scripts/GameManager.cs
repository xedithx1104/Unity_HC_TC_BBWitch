using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

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
    public Vector2Int v2RandomEnemyCount = new Vector2Int(2, 3);
    [SerializeField]
    private Transform[] traCheckboards;
    [SerializeField]
    private Transform[] traColmnSecond;
    private int countRow = 4;
    [SerializeField]
    private List<int> indexColumnSecond = new List<int>();
    private ControlSystem controlSystem;

    private void Awake()
    {
        //�ѽL�}�C = �ѽL�s��.���o�l���󪺤���<�ܧΤ���>()
        traCheckboards = traCheckboard.GetComponentsInChildren<Transform>();
        traColmnSecond = new Transform[countRow];
        for (int i = 0; i < countRow; i++)
        {
            traColmnSecond[i] = traCheckboards[i+1];
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
            
            Instantiate(goEnemy[randomEnemy], traColmnSecond[indexColumnSecond[randomColumnSecond]].position, Quaternion.identity);

            indexColumnSecond.RemoveAt(randomColumnSecond);
        }

        int randomMarble = Random.Range(0, indexColumnSecond.Count);
        Instantiate(
            goMarble,
            traColmnSecond[indexColumnSecond[randomMarble]].position + Vector3.up,
            Quaternion.identity);
            
    }

    /// <summary>
    /// �����^�X
    /// </summary>
    public void SwitchTurn(bool isMyTurn)
    {
        if (isMyTurn) 
        { 
            turn = Turn.My;
            controlSystem.canShoot = true;
        }
        else
        {
            turn = Turn.Enemy;
            onEnemyTurn.Invoke();
        }
    }
}

public enum Turn
{
    My,Enemy
}
