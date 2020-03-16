using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class ImportantThings : ScriptableObject
    {
        public ContactFilter2D whatIsEnemy;
        public LayerMask WhatIsEnemy;
    }
}