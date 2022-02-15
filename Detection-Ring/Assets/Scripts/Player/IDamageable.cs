using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitDirection);
}