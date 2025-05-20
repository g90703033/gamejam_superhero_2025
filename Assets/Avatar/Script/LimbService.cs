using System;
using UnityEngine;
using static Jason.Avatar.AvatarManager;

namespace Jason.Avatar
{
    public class LimbService : MonoBehaviour
    {
        public Action<PlayerAttribute> OnCatchLimb;
        public Collider CatChLimbCollider;
        public Hero hero;
        public AvatarManager AvatarManager;

        public GameObject limbVFX;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Broken")
            {
                HeroArm.ArmType armType = other.gameObject.name.Contains("LeftArm") ? HeroArm.ArmType.Left : HeroArm.ArmType.Right;

                if (armType is HeroArm.ArmType.Left && !hero.heroArmL.gameObject.activeSelf) 
                {
                    PlayerAttribute attribute = other.gameObject.GetComponent<LimbHeader>().Attribute;
                    AvatarManager.RecoverLimb(armType, attribute);

                    Destroy(other.gameObject);
                }

                if (armType is HeroArm.ArmType.Right && !hero.heroArmR.gameObject.activeSelf)
                {
                    PlayerAttribute attribute = other.gameObject.GetComponent<LimbHeader>().Attribute;
                    AvatarManager.RecoverLimb(armType, attribute);

                    Destroy(other.gameObject);
                }
            }
        }

        /// <summary>
        /// Unity Event
        /// </summary>
        public void OnLimbBrokenAction(HeroArm limb)
        {
            CatChLimbCollider.enabled = false;
            Invoke("EnableCatch", 2);
            GameObject limbObject = limb.gameObject;
            limbObject.SetActive(false);
            GameObject brokenObject = Instantiate(limbObject, limbObject.transform.position, limbObject.transform.rotation);
            brokenObject.transform.localScale = limb.hero.transform.localScale;

            SetTag(brokenObject);
            ClearCmponent(brokenObject);

            GameObject vfx = Instantiate(limbVFX, limbObject.transform.position, limbObject.transform.rotation, brokenObject.transform);
            //vfx.transform.parent = brokenObject.transform;

            brokenObject.SetActive(true);

            Vector3 explodeForce = UnityEngine.Random.insideUnitSphere * 30f;
            if (explodeForce.y < 0f) explodeForce.y = -explodeForce.y;
            brokenObject.GetComponent<Rigidbody>().AddForce(explodeForce, ForceMode.VelocityChange);
        }
        public void CatchLamb(GameObject gameObject)
        {
            PlayerAttribute attribute = gameObject.GetComponent<LimbHeader>().Attribute;
            OnCatchLimb?.Invoke(attribute);

            Destroy(gameObject);
        }
        private void EnableCatch()
        {
            CatChLimbCollider.enabled = true;
        }
        private void SetTag(GameObject brokenObject)
        {
            brokenObject.tag = "Broken";
            for (int index = 0; index < brokenObject.transform.childCount; index++)
            {
                GameObject gameObject = brokenObject.transform.GetChild(index).gameObject;
                gameObject.tag = "Broken";
                //SetTag(gameObject);
            }
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
