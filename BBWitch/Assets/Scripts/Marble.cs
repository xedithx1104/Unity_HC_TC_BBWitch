using UnityEngine;

public class Marble : MonoBehaviour
{
    public float attack = 100f;
    public Rigidbody marbleRigibody;
    
    private void Awake()
    {
        marbleRigibody.GetComponent<Rigidbody>();
        // ª«²z.©¿²¤¶î¼h¸I¼²(A¹Ï¼h,B¹Ï¼h)©¿²¤ A B ¹Ï¼h¸I¼²
        Physics.IgnoreLayerCollision(6, 6);
    }

    private void Update()
    {
        Vector3 velocity = marbleRigibody.velocity;

        //Debug.Log(transform.name + " " + velocity);
        if(velocity.x < 0.8) { marbleRigibody.AddForce(Vector3.right*3); }
    }
}
