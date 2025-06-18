using UnityEngine;

public class PlayerSwordTrigger : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(10);
        }
    }
}
