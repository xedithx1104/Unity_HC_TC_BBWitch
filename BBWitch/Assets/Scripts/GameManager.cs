using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 回合資料
    /// </summary>
    public Turn turn = Turn.My;

    [Header("敵方回合事件")]
    public UnityEvent onEnemyTurn;
    [Header("怪物陣列")]
    public GameObject[] goEnemy;
    [Header("彈珠")]
    public GameObject goMarble;
    [Header("棋盤群組")]
    public Transform traCheckboard;
    [Header("生成數量最小最大值")]
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
        //棋盤陣列 = 棋盤群組.取得子物件的元件<變形元件>()
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
    /// 切換回合
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
