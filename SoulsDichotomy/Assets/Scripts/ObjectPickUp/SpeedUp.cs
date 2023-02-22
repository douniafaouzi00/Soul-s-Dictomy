using UnityEngine;

namespace ObjectPickUp
{
    public class SpeedUp : PickUp
    {
        [Header("Speed up attributes")]
        //the new  effective value need to be calibrated  

        [SerializeField] private float newVelocity = 12;
        
        
        private float restoreVelocity;
        public override void ApplyPlayer()
        {
            restoreVelocity = player.GetComponent<PlayerVelocity>().MoveSpeed;
            player.GetComponent<PlayerVelocity>().MoveSpeed = newVelocity; 
        }

        public override void ApplySoul()
        {
            throw new System.NotImplementedException();
        }

        public override void RemovePlayer()
        {
            if (PlayerExist())
            {
                PlayerVelocity pv = player.GetComponent<PlayerVelocity>();
                if (pv != null)
                {
                    pv.MoveSpeed = restoreVelocity;
                }
            }   
        }

        public override void RemoveSoul()
        {
            throw new System.NotImplementedException();
        }
    }
}
