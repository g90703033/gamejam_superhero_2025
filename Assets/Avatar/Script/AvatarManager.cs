using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jason.Avatar
{
    public class AvatarManager : MonoBehaviour
    {
        public ModifyType DefaultType;
        public LimbService limbService;
        public AvatarBodyStorage AvatarBodyStorage;
        public List<PlayerAttribute> AvatarLimb;
        
        void Start()
        {
            SetCatchLimb();
            SetAllBody(DefaultType, LevelType.Default);

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetLimb(LimbType.LeftArm, ModifyType.Scope, LevelType.Level_1);
                SetLimb(LimbType.RightArm, ModifyType.Scope, LevelType.Level_2);
            }
        }
        private void SetCatchLimb()
        {
            limbService.OnCatchLimb += (limbAttribute) =>
            {
                LimbHeader header = limbAttribute.limbHeader;
                SetLimb(limbAttribute.type, header.limbTag.modifyType, header.limbTag.levelType);
                Destroy(limbAttribute.PlayerLimb);
            };
        }
        public void SetLimb(LimbType limb, ModifyType modify, LevelType level)
        {
            foreach (var storelimb in AvatarLimb)
            {
                AvatarLimbModifyLevel attribute = AvatarBodyStorage.GetLimb(limb, modify, level);
                if (storelimb.type == limb) 
                {
                    if (storelimb.PlayerLimb.activeSelf is false)
                    {
                        storelimb.PlayerLimb.tag = "HeroArm";
                        storelimb.PlayerLimb.transform.localRotation = storelimb.OriginQuaternion;
                        storelimb.PlayerLimb.SetActive(true);
                        storelimb.PlayerLimb.GetComponent<MeshFilter>().mesh = attribute.mesh;
                    }
                    return;
                }
            }
        }
        public void SetAllBody(ModifyType modify, LevelType level)
        {
            foreach (var limb in AvatarLimb)
            {
                AvatarLimbModifyLevel attribute = AvatarBodyStorage.GetLimb(limb.type, modify, level);
                limb.PlayerLimb.GetComponent<MeshFilter>().mesh = attribute.mesh;
                SetLimbTag(limb, modify, level);
            }
        }
        public void SetLimbTag(PlayerAttribute limb, ModifyType modify, LevelType level)
        {
            limb.limbHeader = limb.PlayerLimb.GetComponent<LimbHeader>();
            limb.limbHeader.Attribute = limb;
            limb.limbHeader.limbTag.limbType = limb.type;
            limb.limbHeader.limbTag.modifyType = modify;
            limb.limbHeader.limbTag.levelType = level;
            limb.OriginQuaternion = limb.PlayerLimb.transform.localRotation;
        }
        [Serializable]
        public class PlayerAttribute
        {
            public LimbType type;
            public GameObject PlayerLimb;
            public LimbHeader limbHeader;
            public Quaternion OriginQuaternion;
        }
    }
}

