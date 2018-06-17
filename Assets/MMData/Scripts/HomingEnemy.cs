using UnityEngine;
using System.Collections;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class HomingEnemy : MonoBehaviour
    {
        public float speed = 5;
        public float rotatingSpeed = 200;
        public float Gap = 2;
        public GameObject target;

        Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        // Use this for initialization
        void OnEnable()
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                target = LevelManager.Instance.PlayableCharacters[0].gameObject;
                rotatingSpeed = 0;
            }
            else
            {
                rotatingSpeed = 200;
                target = GameObject.FindGameObjectWithTag("Player");
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(target == null)
            {
                this.gameObject.SetActive(false);
                return;
            }
            Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;

            point2Target.Normalize();

            float value = Vector3.Cross(point2Target, transform.right).z;

            if (Vector2.Distance(transform.position, target.transform.position) < Gap)
            {
                //Debug.Log(Vector2.Distance(transform.position, target.transform.position));
                rotatingSpeed = 0;
            }

            //if (value > 0) {

            //        rb.angularVelocity = rotatingSpeed;
            //} else if (value < 0)
            //        rb.angularVelocity = -rotatingSpeed;
            //else
            //rotatingSpeed = 0;

            rb.angularVelocity = rotatingSpeed * value;


            rb.velocity = transform.right * speed;


        }
    }
}
