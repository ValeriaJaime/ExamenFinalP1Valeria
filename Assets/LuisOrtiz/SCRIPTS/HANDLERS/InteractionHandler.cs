using UnityEngine;

namespace LuisOrtiz
{
        public class InteractionHandler : MonoBehaviour
    {
        [SerializeField] private Transform rayOrigin;
        [SerializeField] private float interactionRange;
        [SerializeField] private LayerMask interactionMask;
        private Inventory inventory;
        Ray ray;
        RaycastHit hit;

        private void Awake()
        {
            inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            InteractionRay();
        }

        private void InteractionRay()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, interactionRange, interactionMask))
            {

                if (Input.GetMouseButtonDown(0))
                {

                    switch (hit.collider.tag)
                    {

                        case "Interactable":
                            {
                                hit.collider.GetComponent<IInteractable>().Interact();
                                break;
                            }

                        case "Recollectable":
                            {
                                inventory.AddItem(hit.collider.GetComponent<IRecollectable>().Pick());
                                Destroy(hit.collider.gameObject);
                                break;
                            }
                    }

                }

            }
        }

        private void OnDrawGizmos()
        {

            Physics.Raycast(ray.origin, ray.direction, out hit, interactionRange, interactionMask);

        }


    }

}


