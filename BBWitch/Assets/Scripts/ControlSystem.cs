using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControlSystem : MonoBehaviour
{
    #region 欄位
    [Header("箭頭")]
    public GameObject goArrow;
    [Header("生成彈珠位置")]
    public Transform tranSpawPoint;
    [Header("彈珠欲置物")]
    public GameObject goMarbles;
    [Header("發射速度"),Range(0,5000)]
    public float speedShoot = 750;
    [Header("射線要碰撞的圖層")]
    public LayerMask layerToHit;
    [Header("測試滑鼠位置")]
    public Transform traTestMousePosition;
    [Header("所有彈珠")]
    public List<GameObject> listMarbles = new List<GameObject>();
    [Header("發射間隔"), Range(0, 5)]
    public float fireInterval = 0.5f;
    // 所有彈珠數量
    public static int allMarbles;
    // 可以發射的最大彈珠數量
    public static int maxMarbles = 2;
    // 是否能發射
    public bool canShoot = true;
    // 每次發射出去的彈珠數量
    public static int shootMarbles;

    public Text scoreText;

    public GameObject endUI;

    [SerializeField] private int score;
    //父物件欄位
    public Transform marbleParent;

    #endregion
    #region 事件
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
    #region 方法
    /// <summary>
    /// 滑鼠控制
    /// </summary>
    private void SpawnMarble()
    {
        allMarbles++;
        //所有彈珠清單.添加(生成彈珠)
        GameObject marble = Instantiate(goMarbles, new Vector3(100, 0, 0), Quaternion.identity, marbleParent);
        marble.name = "彈珠" + allMarbles;

        listMarbles.Add(marble);
    }

    /// <summary>
    /// 滑鼠控制
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

            //print("滑鼠座標" + v3Mouse);

            // 射線 = 主要攝影機.螢幕座標轉射線(滑鼠座標)
            Ray rayMouse = Camera.main.ScreenPointToRay(v3Mouse);
            // 射線碰撞資訊
            RaycastHit hit;

            // 如果 射線打到物件就處理
            // 物理 射線碰撞(射距，離線)
            if (Physics.Raycast(rayMouse, out hit, 100, layerToHit))
            {
                print("滑鼠射線打到物件~" + hit.collider.name);

                Vector3 hitPosition = hit.point;               //取得碰撞資訊的座標
                hitPosition.y = 0.5f;                          //調整高度軸向
                traTestMousePosition.position = hitPosition;   //更新測試物件座標

                //角色的z軸 = 測試物件的座標 - 角色的座標(向量)
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
    /// 發射彈珠
    /// </summary>
    private IEnumerator FireMarble()
    {
        shootMarbles = 0;

        for (int i = 0; i < maxMarbles; i++)
        {
            shootMarbles++;
            GameObject temp = listMarbles[i];    //生成彈珠
            temp.transform.position = tranSpawPoint.position;
            temp.transform.rotation = tranSpawPoint.rotation;
            temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            temp.GetComponent<Rigidbody>().AddForce(tranSpawPoint.forward * speedShoot);                 //發射彈珠
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
