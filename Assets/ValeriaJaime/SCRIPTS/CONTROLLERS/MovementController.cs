using UnityEngine;

namespace ValeriaJaime
{

    //Este personaje, va a caminar, correr, saltar y agacharse
    public class MovementController : MonoBehaviour
    {
        // Variables de una sola palabra inician en minuscula
        // Variables con 2 o mas palabras, la 1ra inicia en minuscula y desde la segunda palabra en mayuscula
        public float walkingSpeed;
        public float runningSpeed;

        public float jumpingForce;

        public float crouchHeight;
        public float crouchingSpeed;

        public Transform rayOrigin;
        public float rayRange;
        public LayerMask rayLayers;

        private Rigidbody rb;

        private float normalHeight = 1.75f;

        private Vector3 crouchScale = new Vector3(0.6f, 1, 0.2f);
        private Vector3 playerScale = new Vector3(0.6f, 1.75f, 0.2f);
    
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            Debug.Log(Suma());
        }

        private void Update()
        {
            Move();
            Jump();
            Crouch();

        }

        #region Movement

        // En los metodos, la primera letra de cada palabra en Mayuscula
        private void Move()
        {
            rb.velocity += new Vector3(HorizontalMove(), 0, VerticalMove()) * ActualSpeed() * Time.deltaTime;
        }

       // Variables cuyo valor puedes modificar dentro de las mismas
        public int Suma()
        {
            int num1 = 6;
            int num2 = 10;

            return num1 + num2;
        }

        private float HorizontalMove()
        {
            return Input.GetAxis("Horizontal"); // Si estoy presionando A va a regresar -1. Si presiono D va a regresar 1
        }

        private float VerticalMove()
        {
            return Input.GetAxis("Vertical"); // Si estoy presionando S va a regresar -1. Si presiono W va a regresar 1
        }

        private float ActualSpeed()
        {
            // Si estoy presionando el input de correr, va a regresar runningSpeed, si no va a regresar walkingSpeed
            return RunInputPressed() ? runningSpeed : walkingSpeed;
        }

        private bool RunInputPressed()
        {
            return Input.GetKey(KeyCode.LeftShift); // Si estoy presionando shift izquierdo va a regresar True, y si no Falso
        }

        #endregion

        #region Jump

        private void Jump()
        {
            if(JumpInput() && IsTouchingGround())
            {
                rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
            }
        }

        private bool JumpInput()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        /// <summary>
        /// Hay un raycast o rayo, que nos sirve para saber si el jugador esta tocando o no el suelo
        /// 
        /// El raycast, prinicpalmente necesita de
        /// 
        /// Origen: Es de donde sale el rayo
        /// 
        /// Direccion: Es hacia donde apunta el rayo
        /// 
        /// Rango: Es el alcance o longitud del rayo
        /// 
        /// Layers: Que layers puede o debe detectar el rayo
        /// 
        /// -------------------------------------------------------
        /// 
        /// Hit: Te devuelve el objeto exacto que esta tocando el rayo
        /// 
        /// </summary>
        private bool IsTouchingGround()
        {
            return Physics.Raycast(rayOrigin.position, Vector3.down, rayRange, rayLayers);
        }

        /// <summary>
        /// On Draw Gizmos unicamente dibuja lineas o figuras en el editor
        /// Estas lineas o figuras son meramente representativas, y no tienen ningun efecto sobre el juego
        /// 
        /// NO NOS SIRVE PARA DETECTAR EL SUELO
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(rayOrigin.position,Vector3.down * rayRange);
        }

        #endregion

        #region Crouch
    
        private void Crouch()
        {
            CheckIfCrouchig();
            CheckIfStillCrouching();
        }

        private void CheckIfCrouchig()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.localScale = crouchScale;
                transform.position = transform.position = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
            }
        }

        private void CheckIfStillCrouching()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                transform.localScale = playerScale;
                transform.position = transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
            }
        }
        #endregion
    }
}


