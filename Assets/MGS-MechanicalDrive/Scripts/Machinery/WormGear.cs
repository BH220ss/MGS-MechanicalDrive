/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  WormGear.cs
 *  Description  :  Define WormGear component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Worm with gear.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/WormGear")]
    public class WormGear : BaseMechanism
    {
        #region Field and Property
        /// <summary>
        /// Worm shaft.
        /// </summary>
        public Gear worm;

        /// <summary>
        /// Count of worm threads.
        /// </summary>
        public int threads = 1;

        /// <summary>
        /// Worm gear.
        /// </summary>
        public Gear gear;

        /// <summary>
        /// Count of gear teeth.
        /// </summary>
        public int teeth = 36;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive worm and gear.
        /// </summary>
        /// <param name="speed">Line speed.</param>
        public override void Drive(float speed)
        {
            var wormSpeed = speed / worm.radius * Time.deltaTime;
            worm.transform.Rotate(Vector3.forward, wormSpeed, Space.Self);
            gear.transform.Rotate(Vector3.forward, wormSpeed * threads / teeth, Space.Self);
        }
        #endregion
    }
}