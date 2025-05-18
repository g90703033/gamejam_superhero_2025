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
            if (collision.gameObject.tag == "Broken")
            { CatchLamb(collision.gameObject); }
        }

        /// <summary>
        /// Unity Event
        /// </summary>
        public void OnLimbBrokenAction(HeroArm limb)
        {
            GameObject limbObject = limb.gameObject;
            limbObject.SetActive(false);
            GameObject brokenObject = Instantiate(limbObject);
            brokenObject.tag = "Broken";
            brokenObject.SetActive(true);
            brokenObject.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.insideUnitSphere, ForceMode.Impulse);
            ClearCmponent(brokenObject);
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
            HeroArm heroArm = brokenObject.GetComponent<HeroArm>();
            Destroy(joint);
            Destroy(heroArm);
            //TODO Remove
        }
    }
}
