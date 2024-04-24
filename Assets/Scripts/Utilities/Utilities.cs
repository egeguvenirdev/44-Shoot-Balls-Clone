using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VigilanteGamesUtilies
{
    struct ShootableObjInfo
    {
        public Transform ballTargetPos;
        public UpgradeType upgradeType;
        public float value;
        public float followDuration;

        public ShootableObjInfo(Transform ballTargetPos, UpgradeType upgradeType, float value, float followDuration)
        {
            this.ballTargetPos = ballTargetPos;
            this.upgradeType = upgradeType;
            this.value = value;
            this.followDuration = followDuration;
        }
    }

    public static class Utilities
    {

    }
}
