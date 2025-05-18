using Unity.VisualScripting;
using UnityEngine;

public class VictimBody : MonoBehaviour
{
    private ConfigurableJoint joint;
    public Victim victim;

    void OnCollisionEnter(Collision collision)
    {
        if (victim.isDead) return;

        if (victim.IsAttached) return;

        if (collision.transform.CompareTag("Ground"))
        {
            victim.SetDead(true);
            return;
         }

        if (!victim.IsAttached && collision.rigidbody != null)
        {
            if (collision.transform.root != victim.transform)
            {
                //Check target is hero or hero-connected victim

                if (collision.transform.CompareTag("HeroArm"))
                {
                    HeroArm heroArm = collision.transform.GetComponent<HeroArm>();

                    if (heroArm != null)
                    {
                        victim.attachedArm = heroArm;
                        victim.attachedArm.AddHoldableObject(victim);

                        AddJointToAttached(collision.rigidbody);

                        victim.SetGravity(false);
                        victim.SetJointForce(victim.activeJointSpring);
                    }
                }
                else if (collision.transform.CompareTag("Victim"))
                {
                    VictimBody collisionVictimBody = collision.gameObject.GetComponent<VictimBody>();
                    if (collisionVictimBody != null && collisionVictimBody.victim.attachedHero != null)
                    {
                        victim.attachedArm = collisionVictimBody.victim.attachedArm;
                        victim.attachedArm.AddHoldableObject(victim);

                        AddJointToAttached(collision.rigidbody);

                        victim.SetGravity(false);
                        victim.SetJointForce(victim.activeJointSpring);
                    }
                }
            }
        }
    }

    private void AddJointToAttached(Rigidbody targetRb)
    {
        joint = transform.AddComponent<ConfigurableJoint>();

        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;

        joint.rotationDriveMode = RotationDriveMode.Slerp;

        JointDrive slerpDrive = joint.slerpDrive;
        slerpDrive.positionSpring = 100000f;
        joint.slerpDrive = slerpDrive;

        joint.connectedBody = targetRb;

        victim.attachedJoint = joint;
     }
}
