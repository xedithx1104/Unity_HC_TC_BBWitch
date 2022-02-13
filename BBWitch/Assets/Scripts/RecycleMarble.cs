using UnityEngine;

public class RecycleMarble : MonoBehaviour
{
    // Μ紆痌计秖
    public static int recycleMarbles;

    public GameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("紆痌"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = new Vector3(100, 0, 0);


            // Μ紆痌计秖糤
            recycleMarbles++;
            // 狦Μ计秖 单 ┮Τ紆痌计秖 ち传 寄よ
            if (recycleMarbles != ControlSystem.shootMarbles) { return; }
            
            gm.SwitchTurn(false);

            if (gm.monsterParent.childCount == 0) { gm.SwitchTurn(true); }
        }
    }

}
