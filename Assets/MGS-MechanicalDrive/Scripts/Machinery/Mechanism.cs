/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract AngularMechanism and EngageGear.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  5/3/2018
 *  Description  :  Optimize AngularMechanism and define EngageGear.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Mechanism can be drived by angular velocity.
    /// </summary>
    public abstract class AngularMechanism : Mechanism
    {
        #region Public Method
        /// <summary>
        /// Drive Mechanism by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public abstract void AngularDrive(float velocity);
        #endregion
    }

    /// <summary>
    /// Gear with engage mechanisms.
    /// </summary>
    public abstract class EngageGear : AngularMechanism
    {
        #region Field and Property
        /// <summary>
        /// Radius of gear.
        /// </summary>
        public float radius = 0.5f;

        /// <summary>
        /// Engaged mechanisms.
        /// </summary>
        public List<Mechanism> engages;

        /// <summary>
        /// Power gear.
        /// </summary>
        protected EngageGear powerGear;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive Engaged mechanisms by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        protected void DriveEngages(float velocity)
        {
            foreach (var engage in engages)
            {
                engage.Drive(velocity);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive gear by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            transform.Rotate(Vector3.forward, velocity / radius * Mathf.Rad2Deg * Time.deltaTime, Space.Self);
            DriveEngages(velocity);
        }

        /// <summary>
        /// Drive gear by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity.</param>
        public override void AngularDrive(float velocity)
        {
            transform.Rotate(Vector3.forward, velocity * Time.deltaTime, Space.Self);
            DriveEngages(velocity * Mathf.Deg2Rad * radius);
        }

        /// <summary>
        /// Engage this gear to power gear.
        /// </summary>
        /// <param name="gear"></param>
        public void EngageTo(EngageGear gear)
        {
            if (gear == null || gear == powerGear)
                return;
            else
            {
                BreakEngage();

                if (!gear.engages.Contains(this))
                {
                    //Engage this gear to new power gear.
                    gear.engages.Add(this);
                }
                powerGear = gear;
            }
        }

        /// <summary>
        /// Break engage from power gear.
        /// </summary>
        public void BreakEngage()
        {
            if (powerGear != null)
            {
                powerGear.engages.Remove(this);
                powerGear = null;
            }
        }
        #endregion
    }
}