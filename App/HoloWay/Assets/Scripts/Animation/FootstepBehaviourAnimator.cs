using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepBehaviourAnimator : MonoBehaviour
{
    public AudioClip clip;
    public void PlayFootstepSound()
    {
        StartCoroutine(this.PlayFootstep());
    }
    // Start is called before the first frame update
    public IEnumerator PlayFootstep()
    {

        AudioSource source = Camera.main.GetComponent<AudioSource>();
        source.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(1);
        source.PlayOneShot(clip);
    }
}
