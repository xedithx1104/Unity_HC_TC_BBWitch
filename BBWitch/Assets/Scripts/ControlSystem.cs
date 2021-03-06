using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControlSystem : MonoBehaviour
{
    #region 逆
    [Header("絙繷")]
    public GameObject goArrow;
    [Header("ネΘ紆痌竚")]
    public Transform tranSpawPoint;
    [Header("紆痌饼竚")]
    public GameObject goMarbles;
    [Header("祇甮硉"),Range(0,5000)]
    public float speedShoot = 750;
    [Header("甮絬璶窱疾瓜糷")]
    public LayerMask layerToHit;
    [Header("代刚菲公竚")]
    public Transform traTestMousePosition;
    [Header("┮Τ紆痌")]
    public List<GameObject> listMarbles = new List<GameObject>();
    [Header("祇甮丁筳"), Range(0, 5)]
    public float fireInterval = 0.5f;
    // ┮Τ紆痌计秖
    public static int allMarbles;
    // 祇甮程紆痌计秖
    public static int maxMarbles = 2;
    // 琌祇甮
    public bool canShoot = true;
    // –Ω祇甮紆痌计秖
    public static int shootMarbles;

    public Text scoreText;

    public GameObject endUI;

    [SerializeField] private int score;
    //ン逆
    public Transform marbleParent;

    #endregion
    #region ㄆン
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
    #region よ猭
    /// <summary>
    /// 菲公北
    /// </summary>
    private void SpawnMarble()
    {
        allMarbles++;
        //┮Τ紆痌睲虫.睰(ネΘ紆痌)
        GameObject marble = Instantiate(goMarbles, new Vector3(100, 0, 0), Quaternion.identity, marbleParent);
        marble.name = "紆痌" + allMarbles;

        listMarbles.Add(marble);
    }

    /// <summary>
    /// 菲公北
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

            //print("菲公畒夹" + v3Mouse);

            // 甮絬 = 璶尼紇诀.棵辊畒夹锣甮絬(菲公畒夹)
            Ray rayMouse = Camera.main.ScreenPointToRay(v3Mouse);
            // 甮絬窱疾戈癟
            RaycastHit hit;

            // 狦 甮絬ゴン碞矪瞶
            // 瞶 甮絬窱疾(甮禯瞒絬)
            if (Physics.Raycast(rayMouse, out hit, 100, layerToHit))
            {
                print("菲公甮絬ゴン~" + hit.collider.name);

                Vector3 hitPosition = hit.point;               //眔窱疾戈癟畒夹
                hitPosition.y = 0.5f;                          //秸俱蔼禸
                traTestMousePosition.position = hitPosition;   //穝代刚ン畒夹

                //à︹z禸 = 代刚ン畒夹 - à︹畒夹(秖)
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
    /// 祇甮紆痌
    /// </summary>
    private IEnumerator FireMarble()
    {
        shootMarbles = 0;

        for (int i = 0; i < maxMarbles; i++)
        {
            shootMarbles++;
            GameObject temp = listMarbles[i];    //ネΘ紆痌
            temp.transform.position = tranSpawPoint.position;
            temp.transform.rotation = tranSpawPoint.rotation;
            temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            temp.GetComponent<Rigidbody>().AddForce(tranSpawPoint.forward * speedShoot);                 //祇甮紆痌
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
