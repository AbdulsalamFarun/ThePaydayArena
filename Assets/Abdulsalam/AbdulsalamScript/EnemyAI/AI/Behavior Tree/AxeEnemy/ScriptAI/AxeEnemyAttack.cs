using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int quickAttackDamage = 10;
    public int heavyAttackDamage = 25;
    public float attackRange = 1.5f;
    public LayerMask playerLayer;

    public Transform attackPoint;

    // Call this for quick attack
    public void QuickAttack()
    {
        DealDamage(quickAttackDamage);
    }

    // Call this for heavy attack
    public void HeavyAttack()
    {
        DealDamage(heavyAttackDamage);
    }

    private void DealDamage(int damage)
    {
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hitPlayers)
        {
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
