using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    public Vector2Int v2RandomEnemyCount = new Vector2Int(1, 4);
    [SerializeField]
    private Transform[] traCheckboards;
    [SerializeField]
    private Transform[] traColmnFirst;
    private int countRow = 4;
    [SerializeField]
    private List<int> indexColumnSecond = new List<int>();
    private ControlSystem controlSystem;

    //父物件欄位
    public Transform monsterParent;
   
    private void Awake()
    {
        //棋盤陣列 = 棋盤群組.取得子物件的元件<變形元件>()
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
    /// 切換回合
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
        SceneManager.LoadScene("開頭");
    }
}

public enum Turn
{
    My,Enemy
}
