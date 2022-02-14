using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Base
{
    public abstract class Waypoint : MonoBehaviour
    {
        [SerializeField]
        private int index;
        public int Index {
            get => index;
        }

        protected abstract void Register();
    }
}
