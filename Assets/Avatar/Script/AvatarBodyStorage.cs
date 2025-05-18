using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jason.Avatar
{
    public enum LevelType
    {
        Default,
        Level_1,
        Level_2
    }
    public enum ModifyType
    {
        Idle,
        demo,
        Scope
    }
    public enum LimbType
    {
        body,
        LeftArm,
        LeftLeg,
        RightArm,
        RightLeg
    }
    public class AvatarBodyStorage : MonoBehaviour
    {
        public List<AvatarLimb> AvatarLimbList;
        public AvatarLimbModifyLevel GetLimb(LimbType limb, ModifyType modify, LevelType level)
        {
            AvatarLimbModifyLevel result = AvatarLimbList[(int)limb].modifies[(int)ModifyType.Idle].modifyLevels[(int)LevelType.Default];
            foreach (var storelimb in AvatarLimbList)
            {
                if (storelimb.type == limb) 
                {
                    result = GetModify(storelimb, modify, level);
                    return result;
                } 
            }
            return result;
        }
        private AvatarLimbModifyLevel GetModify(AvatarLimb limb, ModifyType modify, LevelType level)
        {
            AvatarLimbModifyLevel result = limb.modifies[(int)ModifyType.Idle].modifyLevels[(int)LevelType.Default]; ;
            foreach (var storeModify in limb.modifies)
            {
                if (storeModify.type == modify)
                {
                    result = GetLevel(storeModify, level);
                    return result;
                }
            }
            return result;
        }
        private AvatarLimbModifyLevel GetLevel(AvatarLimbModify modify, LevelType level)
        {
            AvatarLimbModifyLevel result = modify.modifyLevels[(int)LevelType.Default];
            foreach (var storeLevel in modify.modifyLevels)
            {
                if (storeLevel.type == level)
                {
                    result = storeLevel;
                    return result;
                }
            }
            return result;
        }
    }
    [Serializable]
    public class AvatarLimb
    {
        public LimbType type;
        public List<AvatarLimbModify> modifies;
    }
    [Serializable]
    public class AvatarLimbModify
    {
        public ModifyType type;
        public List<AvatarLimbModifyLevel> modifyLevels;
    }
    [Serializable]
    public class AvatarLimbModifyLevel
    {
        public LevelType type;
        public Mesh mesh;
    }
}

