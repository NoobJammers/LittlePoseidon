using UnityEngine;
public class TotalImpulseApplied : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionForce = collision.GetImpactForce();
        Debug.Log("Force:" + collisionForce);
        // Debug.Log(collisionForce);
        // if (collisionForce < 100.0F)
        // {
        //     // This collision has not damaged anyone...
        // }
        // else if (collisionForce < 200.0F)
        // {
        //     // Auch! This will take some damage.
        // }
        // else
        // {
        //     // This collision killed me!
        // }
    }
}
