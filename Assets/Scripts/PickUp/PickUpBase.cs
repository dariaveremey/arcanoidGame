using UnityEngine;

public abstract class PickUpBase : MonoBehaviour
{
 #region Private methods

 private void OnCollisionEnter2D(Collision2D col)
 {
  if (!col.gameObject.CompareTag(Tags.Pad))
   return;
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

 #endregion
    
}