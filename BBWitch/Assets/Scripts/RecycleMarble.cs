using UnityEngine;

public class RecycleMarble : MonoBehaviour
{
    public static int recycleMarbles;

    public GameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("¼u¯]"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = new Vector3(0, 0, 100);

            recycleMarbles++;
            if (recycleMarbles == ControlSystem.allMarbles) gm.SwitchTurn(false);
            {

            }
        }
    }

}
