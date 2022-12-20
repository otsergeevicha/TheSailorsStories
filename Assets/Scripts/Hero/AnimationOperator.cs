using UnityEngine;

namespace Hero
{
    public class AnimationOperator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Run()
        {
            _animator.SetBool("Run", true);
        }

        public void Idle()
        {
            _animator.SetBool("Run", false);
        }
    }
}