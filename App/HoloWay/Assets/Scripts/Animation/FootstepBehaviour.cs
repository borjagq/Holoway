using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FootstepBehaviour : StateMachineBehaviour
{
    //public AudioSource source;
    public AudioClip clip;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        /*animator.GetComponent<FootstepBehaviourAnimator>().PlayFootstepSound();*/
        AudioSource source = Camera.main.GetComponent<AudioSource>();
        source.PlayOneShot(clip);

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        /*animator.GetComponent<FootstepBehaviourAnimator>().PlayFootstepSound();*/
        AudioSource source = Camera.main.GetComponent<AudioSource>();
        source.Stop();

    }
}
