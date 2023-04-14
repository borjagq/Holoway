using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmoticonHandler : MonoBehaviour
{
    public Animator CharacterAnim;
    public void WaveOnClick()
    {
        CharacterAnim.SetBool("IsWaving", true);
        
    }
    public void ShakeHandOnClick()
    {
        CharacterAnim.SetBool("IsShakingHands", true);

    }
    public void SitDownOnClick()
    {
        if (!CharacterAnim.GetBool("IsSitting"))
        {
            CharacterAnim.SetBool("IsStartSitting", true);
            CharacterAnim.SetBool("IsSitting", false);
        }
    }
    public void StandUpOnClick()
    {
        if(CharacterAnim.GetBool("IsSitting"))
        {
            CharacterAnim.SetBool("IsStartStanding", true);
            CharacterAnim.SetBool("IsSitting", false);
        }
    }

    public bool IsSitting()
    {
        return CharacterAnim.GetBool("IsSitting");
    }

    public bool IsStanding()
    {
        return !(CharacterAnim.GetBool("IsStartStanding") && CharacterAnim.GetBool("IsSitting"));
    }

    public bool IsShakingHands()
    {
        return CharacterAnim.GetBool("IsShakingHands");
    }

    public bool IsWaving()
    {
        return CharacterAnim.GetBool("IsWaving");
    }

    public string GetAnimatorCurrentAnimation()
    {
        return CharacterAnim.GetCurrentAnimatorClipInfo(0)[0].ToString();
    }
}
