using UnityEngine;
[CreateAssetMenu(fileName = "ImportantThings", menuName = "ImportantThings", order = 0)]
    public class ImportantThings : ScriptableObject
    {
        public ContactFilter2D whatIsEnemy;
        public LayerMask WhatIsEnemy;
        public enum DamageType
        {
            Value,
            pHP,
            pMHP
        }
    }