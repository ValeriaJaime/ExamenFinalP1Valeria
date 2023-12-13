using UnityEngine;

namespace LuisOrtiz
{
    public class Collectible : MonoBehaviour, IRecollectable
    {

        public Item itemReference;

        public Item Pick()
        {

            return itemReference;

        }

    }

}


