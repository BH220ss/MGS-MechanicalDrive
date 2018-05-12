/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DynamicRollerChain.cs
 *  Description  :  Define DynamicRollerChain component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/27/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/DynamicRollerChain")]
    public class DynamicRollerChain : RollerChain
    {
        #region Public Method
        /// <summary>
        /// Drive chain.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void LinearDrive(float velocity)
        {
            CreateCurve();

            var maxTime = curve[curve.Length - 1].time;
            if (Mathf.Abs(timer) >= maxTime)
                timer -= maxTime;

            base.LinearDrive(velocity);
        }
        #endregion
    }
}