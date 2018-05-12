/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MechDriver.cs
 *  Description  :  Define driver for test mechanism quickly.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/13/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [RequireComponent(typeof(BaseMechanism))]
    public class MechDriver : MonoBehaviour
    {
        #region Field and Property
        public float velocity = 1;
        public KeyCode positive = KeyCode.P;
        public KeyCode negative = KeyCode.N;

        protected BaseMechanism mechanism;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            mechanism = GetComponent<BaseMechanism>();
        }

        protected virtual void Update()
        {
            if (Input.GetKey(positive))
                mechanism.LinearDrive(velocity);
            else if (Input.GetKey(negative))
                mechanism.LinearDrive(-velocity);
        }
        #endregion
    }
}