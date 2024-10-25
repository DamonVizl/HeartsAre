using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEffectsManager : MonoBehaviour
{

    public ParticleSystem playerDamageParticle;

    public void PlayPlayerDamageParticleEffect()
    {
        if (playerDamageParticle != null)
        {
            playerDamageParticle.Play();
        }
    }
}
