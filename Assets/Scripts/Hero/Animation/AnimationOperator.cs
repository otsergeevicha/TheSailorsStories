using UnityEngine;

namespace Hero
{
    public class AnimationOperator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Run()
        {
            _animator.SetBool(HashAnimationHero.Run, true);
        }

        public void Idle()
        {
            _animator.SetBool(HashAnimationHero.Run, false);
        }
    }
}