using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class BoyController : PlayableCharacter
    {
        //跳距離
        public float JumpForce = 20f;
        //跳次數
        public int NumberOfJumpsAllowed = 1;
        //冷卻時間
        public float CooldownBetweenJumps = 0f;
        /// can the character jump only when grounded ?
        public bool JumpsAllowedWhenGroundedOnly;
        /// 落下速度
        public float JumpReleaseSpeed = 50f;
        /// 可控制落下
        public bool JumpProportionalToPress = true;
        //增益時間
        public float BuffTime = 5f;

        public int _numberOfJumpsLeft;
        protected bool _jumping = false;
        protected bool _flying = false;
        protected float _lastJumpTime;
        protected float _FlyStartTime;

        /// <summary>
        /// On Start() we initialize the last jump time
        /// </summary>
        protected override void Start()
        {
            _lastJumpTime = Time.time;
            _FlyStartTime = Time.time;
            _numberOfJumpsLeft = NumberOfJumpsAllowed;
        }

        /// <summary>
        /// On update, we update the animator and try to reset the jumper's position
        /// </summary>
        protected override void Update()
        {
            _jumping = false;

            // we determine the distance between the ground and the Jumper
            ComputeDistanceToTheGround();
            // we send our various states to the animator.      
            UpdateAnimator();
            // if we're supposed to reset the player's position, we lerp its position to its initial position
            ResetPosition();
            // we check if the player is out of the death bounds or not
            CheckDeathConditions();

            // we reset our jump variables if needed
            if (_grounded)
            {
                _jumping = false;
                if (Time.time - _lastJumpTime > 0.02f)
                {
                    _numberOfJumpsLeft = NumberOfJumpsAllowed;
                }
            }
        }

        /// <summary>
        /// Updates all mecanim animators.
        /// </summary>
        protected override void UpdateAllMecanimAnimators()
        {
            MMAnimator.UpdateAnimatorBoolIfExists(_animator, "Grounded", _grounded);
            MMAnimator.UpdateAnimatorBoolIfExists(_animator, "Jumping", _jumping);
            MMAnimator.UpdateAnimatorBoolIfExists(_animator, "Flying", _flying);
        }

        public void Fly()
        {
            if (_flying)
                StopCoroutine(BuffDecrease(BuffTime));
            else
                _flying = true;
            
            GameManager.Instance.TimeScale = 1.5f;
            StartCoroutine(BuffDecrease(BuffTime));
        }

        IEnumerator BuffDecrease(float _time)
        {
            yield return new WaitForSeconds(_time);
            GameManager.Instance.TimeScale = 1.0f;
            _flying = false;
        }

        /// <summary>
        /// What happens when the main action button button is pressed
        /// </summary>
        public override void MainActionStart()
        {
            Jump();
        }

        public virtual void Jump()
        {
            if (!EvaluateJumpConditions())
            {
                return;
            }

            PerformJump();
        }

        /// <summary>
        /// 跳躍行為
        /// </summary>
        protected virtual void PerformJump()
        {
            _lastJumpTime = Time.time;
            // we jump and decrease the number of jumps left
            _numberOfJumpsLeft--;

            // if the character is falling down, we reset its velocity
            if (_rigidbodyInterface.Velocity.y < 0)
            {
                _rigidbodyInterface.Velocity = Vector3.zero;
            }

            // we make our character jump
            ApplyJumpForce();
            MMEventManager.TriggerEvent(new MMGameEvent("Jump"));

            _lastJumpTime = Time.time;
            _jumping = true;
        }

        protected virtual void ApplyJumpForce()
        {
            _rigidbodyInterface.AddForce(Vector3.up * JumpForce);
        }

        /// <summary>
        /// 判斷是否可跳及次數
        /// </summary>
        /// <returns><c>true</c>, if jump conditions was evaluated, <c>false</c> otherwise.</returns>
        protected virtual bool EvaluateJumpConditions()
        {
            // if the character is not grounded and is only allowed to jump when grounded, we do not jump
            if (JumpsAllowedWhenGroundedOnly && !_grounded)
            {
                return false;
            }

            // if the character doesn't have any jump left, we do not jump
            if (_numberOfJumpsLeft == 0)
            {
                return false;
            }

            // if we're still in cooldown from the last jump AND this is not the first jump, we do not jump
            if ((Time.time - _lastJumpTime < CooldownBetweenJumps) && (_numberOfJumpsLeft != NumberOfJumpsAllowed))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// What happens when the main action button button is released
        /// </summary>
        public override void MainActionEnd()
        {
            // we initiate the descent
            if (JumpProportionalToPress)
            {
                StartCoroutine(JumpSlow());
            }
        }

        /// <summary>
        /// Slows the player's jump
        /// </summary>
        /// <returns>The slow.</returns>
        public virtual IEnumerator JumpSlow()
        {
            while (_rigidbodyInterface.Velocity.y > 0)
            {
                Vector3 newGravity = Vector3.up * (_rigidbodyInterface.Velocity.y - JumpReleaseSpeed * Time.deltaTime);
                _rigidbodyInterface.Velocity = new Vector3(_rigidbodyInterface.Velocity.x, newGravity.y, _rigidbodyInterface.Velocity.z);
                yield return 0;
            }
        }


    }
}