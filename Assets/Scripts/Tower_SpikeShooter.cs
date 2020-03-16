using UnityEngine;
public class Tower_SpikeShooter : ATower
{
    public int spikesnumber=2;//the spikes will be shoot with an equal angles,

    void Update()
    {
        if (_dpsTimer > dps)
        {
            Shoot();
        }
        _dpsTimer += Time.deltaTime;
    }

    public void Shoot()
    {
        for (int i = 0; i < spikesnumber; i++)
        {
            ABullet b = Instantiate(BulletPrefab,
                transform.position,
                Quaternion.Euler(0,0,transform.rotation.eulerAngles.z+360f*i/spikesnumber));
               // Quaternion.Euler(0,0,0));
            b.targetPos = transform.up;
            Debug.Log(b.targetPos);
            b.damage = Damage;
        }
        _dpsTimer = 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,radious);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,radiousC);
        Gizmos.color = Color.red;
        for (int i = 0; i < spikesnumber; i++)
        {
            Gizmos.DrawLine(transform.position,transform.position+(Vector3)Waypoint.rotate(transform.up,360f*i/spikesnumber));
        }
    }

}