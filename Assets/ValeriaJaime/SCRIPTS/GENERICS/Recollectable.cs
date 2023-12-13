using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValeriaJaime
{

    public class Recollectable : MonoBehaviour, IRecollectable
    {

        [SerializeField] private Item objeto;

        public Item Pick()
        {
            return objeto;
        }
    }

}