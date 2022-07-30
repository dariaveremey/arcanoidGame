using UnityEngine;

public abstract class PickUpBase : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        PlayMusic();
        PlayParticle();
        ApplyEffect(col);
        Destroy(gameObject);
    }

    private void PlayParticle()
    {
       //TODO: Add Particle
    }

    private void PlayMusic()
    {
        //ToDO: Add Music
    }

    protected abstract void ApplyEffect(Collision2D col);
}