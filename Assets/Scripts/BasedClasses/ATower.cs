using UnityEngine;

public abstract class ATower : MonoBehaviour
{
    public float radious = 6;
    public GameObject BulletPrefab;
    public float Damage =10;
    public float level = 0;
    public float dps = 0.5f;
    public ContactFilter2D whatIsEnemy;
    public Enemy CurrentEnemy;
    public float _dpsTimer;

}