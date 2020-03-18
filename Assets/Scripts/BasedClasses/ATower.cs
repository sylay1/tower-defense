using UnityEngine;

public abstract class ATower : MonoBehaviour
{
    public float radious = 6;
    public float radiousC = 0.5f;
    public ABullet BulletPrefab;
    public float Damage =10;
    public float level = 0;
    public float dps = 0.5f;
    public AEnemy CurrentEnemy;
    public float _dpsTimer;
    public bool StopShooting = false;
    void Update()
    {
        if (_dpsTimer > dps)
        {
            Shoot();
        }
        if(!StopShooting)_dpsTimer += Time.deltaTime;
    }

    public virtual void Shoot()
    {
        if (CurrentEnemy)
        {
            ABullet clone = Instantiate(BulletPrefab, transform.position, transform.rotation);
            clone.Target = CurrentEnemy.gameObject;
            clone.damage = Damage;
            clone.targetPos = CurrentEnemy.transform.position;
            _dpsTimer = 0;
        }
    }

}