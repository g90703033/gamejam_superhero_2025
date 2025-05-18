using Unity.VisualScripting;
using UnityEngine;
using WildWind.ObjectPool;

public class Victim : HoldableObject
{    
    public Rigidbody[] myRbs;
    public ConfigurableJoint[] myJoints;
    //public VictimBody attachedBody;
    public float activeJointSpring = 2000f;

    public PooledObject pooledObject;

    public bool isDead;
    public float deadRecycleDelay = 3f;
    private float deadMoment;

    public GameObject deadVFX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (transform.position.y < -5f)
            {
                Recycle();
            }
            else if (isDead && Time.time - deadMoment > deadRecycleDelay)
            {
                Recycle();
             }
        }
    }

    public void SetDead(bool val)
    {
        isDead = val;
        deadMoment = Time.time;

        SetJointForce(val ? activeJointSpring : 0f);

        EndFirstComing();

        if (val)
        {
            deadVFX.transform.position = transform.position;
            deadVFX.transform.rotation = Quaternion.identity;
            deadVFX.transform.parent = null;
            deadVFX.SetActive(true);
         }
     }

    public void Recycle()
    {
        pooledObject.BackToPool();

        if (attachedArm != null)
        {
            attachedArm.RemoveHoldableObject(this);
            attachedArm = null;
        }

        if (attachedJoint != null)
        {
            Destroy(attachedJoint);
        }

        for (int i = 0; i < myRbs.Length; i++)
        {
            myRbs[i].linearVelocity = Vector3.zero;
        }

        SetGravity(true);

        isDead = false;
        SetJointForce(0f);

        deadVFX.SetActive(false);
    }

    public override void ThrowObject(Vector3 force)
    {
        Destroy(attachedJoint);
        attachedArm = null;

        for (int i = 0; i < myRbs.Length; i++)
        {
            myRbs[i].AddForce(force, ForceMode.Impulse);
        }

        SetGravity(true);
        SetJointForce(0f);
     }

    public override void ReleaseObject()
    {
        Destroy(attachedJoint);
        attachedArm = null;

        SetGravity(true);
     }

    public void SetGravity(bool val)
    {
        for (int i=0; i<myRbs.Length; i++)
        {
            myRbs[i].useGravity = val;
        }
    }

    public void SetJointForce(float val)
    {
        JointDrive jointDrive = myJoints[0].slerpDrive;
        jointDrive.positionSpring = val;

        for (int i = 0; i < myJoints.Length; i++)
        {
            myJoints[i].slerpDrive = jointDrive;
        }
     }


    public void UpdateTag()
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        myRbs = rbs;

        foreach (Rigidbody rb in rbs)
        {
            rb.transform.tag = tag;
        }

        myJoints = GetComponentsInChildren<ConfigurableJoint>();
    }
}
