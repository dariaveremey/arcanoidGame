 using UnityEngine;

 public class BallSpeadChangePickUp:PickUpBase
 {

     #region Variables

     [Header(nameof(BallSpeadChangePickUp))]
     [SerializeField] private float _speedMultiplier;

     #endregion


     #region Private methods

     protected override void ApplyEffect(Collision2D col)
     {
         FindObjectOfType<Ball>().ChangeSpead(_speedMultiplier);

     }

     #endregion
    }


