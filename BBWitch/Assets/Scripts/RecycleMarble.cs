using UnityEngine;

public class RecycleMarble : MonoBehaviour
{
    // �^���u�]�ƶq
    public static int recycleMarbles;

    public GameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("�u�]"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = new Vector3(100, 0, 0);


            // �^���u�]�ƶq�W�[
            recycleMarbles++;
            // �p�G�^���ƶq ���� �Ҧ��u�]�ƶq ������ �Ĥ�^�X
            if (recycleMarbles != ControlSystem.shootMarbles) { return; }
            
            gm.SwitchTurn(false);

            if (gm.monsterParent.childCount == 0) { gm.SwitchTurn(true); }
        }
    }

}
