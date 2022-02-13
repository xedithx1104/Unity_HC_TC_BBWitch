using UnityEngine;

public class Marble : MonoBehaviour
{
    public float attack = 100f;
    public Rigidbody marbleRigibody;
    
    private void Awake()
    {
        marbleRigibody.GetComponent<Rigidbody>();
        // ���z.������h�I��(A�ϼh,B�ϼh)���� A B �ϼh�I��
        Physics.IgnoreLayerCollision(6, 6);
    }

    private void Update()
    {
        Vector3 velocity = marbleRigibody.velocity;

        //Debug.Log(transform.name + " " + velocity);
        if(velocity.x < 0.8) { marbleRigibody.AddForce(Vector3.right*3); }
    }
}
