using UnityEngine;
using static Jason.Avatar.AvatarManager;

namespace Jason.Avatar
{
    public class LimbHeader : MonoBehaviour
    {
        [HideInInspector] public string BuffID;
        [HideInInspector] public LimbTag limbTag = new();
        [HideInInspector] public PlayerAttribute Attribute;
        public class LimbTag
        {
            public LimbType limbType;
            public ModifyType modifyType;
            public LevelType levelType;
        }
    }
}
