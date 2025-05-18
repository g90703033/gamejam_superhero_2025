using System;
using UnityEngine;
using static Jason.Avatar.AvatarManager;

namespace Jason.Avatar
{
    public class LimbService : MonoBehaviour
    {
        public Action<PlayerAttribute> OnCatchLimb;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "broken")
            { CatchLamb(collision.gameObject); }
        }

        /// <summary>
        /// Unity Event
        /// </summary>
        public void OnLimbBrokenAction(GameObject limbObject)
        {
            limbObject.SetActive(false);
            GameObject brokenObject = Instantiate(limbObject);
            brokenObject.tag = "broken";
            brokenObject.SetActive(true);
            brokenObject.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.insideUnitSphere, ForceMode.Impulse);
            ClearCmponent(brokenObject);
        }
        public void ChangeWeightColor(float value)
        { 
        }
        public void CatchLamb(GameObject gameObject)
        {
            Debug.Log("Get");
            PlayerAttribute attribute = gameObject.GetComponent<LimbHeader>().Attribute;
            OnCatchLimb?.Invoke(attribute);
        }
        private void ClearCmponent(GameObject brokenObject)
        {
            ConfigurableJoint joint = brokenObject.GetComponent<ConfigurableJoint>();
            Destroy(joint);
            //TODO Remove
        }
    }
}
