using UnityEngine;

public class Eenmy : MonoBehaviour, IDamageable
{
    public void TakeDamage(float amount)
    {
        Debug.Log("take damage");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(10);
        }

    }


    // void OnTriggerEnter(Collider other)
    // {
    //     other
    // }


}
