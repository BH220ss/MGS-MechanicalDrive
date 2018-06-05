/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Gear.cs
 *  Description  :  Define Gear component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Gear rotate around axis Z.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Gear")]
    public class Gear : Axle, IEngageMechanism, IEngagedMechanism
    {
        #region Field and Property
        /// <summary>
        /// Engaged mechanisms.
        /// </summary>
        public List<Mechanism> engages;

        /// <summary>
        /// Radius of gear.
        /// </summary>
        public float radius = 0.5f;

        /// <summary>
        /// Engage power mechanism.
        /// </summary>
        protected IEngageMechanism engage;
        #endregion

        #region Protected Method
        /// <summary>
        /// Initialize engages.
        /// </summary>
        protected void InitializeEngages()
        {
            foreach (var engage in engages)
            {
                if (engage is IEngagedMechanism)
                {
                    (engage as IEngagedMechanism).EngageTo(this);
                }
            }
        }

        /// <summary>
        /// Drive engage mechanisms by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity of drive.</param>
        protected void DriveEngages(float velocity)
        {
            foreach (var engage in engages)
            {
                engage.Drive(velocity, DriveType.Linear);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize gear.
        /// </summary>
        public override void Initialize()
        {
            InitializeCoaxes();
            InitializeEngages();
        }

        /// <summary>
        /// Drive gear by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity, DriveType type)
        {
            var angular = velocity;
            var linear = velocity;

            if (type == DriveType.Linear)
                angular = velocity / radius * Mathf.Rad2Deg;
            else
                linear = velocity * Mathf.Deg2Rad * radius;

            transform.Rotate(Vector3.forward, angular * Time.deltaTime, Space.Self);
            DriveCoaxes(angular);
            DriveEngages(-linear);
        }

        /// <summary>
        /// Build engage for mechanism.
        /// </summary>
        /// <param name="engage">Engage mechanism.</param>
        public void BuildEngage(IEngagedMechanism engage)
        {
            var Mechanism = engage as Mechanism;
            if (Mechanism && !engages.Contains(Mechanism))
                engages.Add(Mechanism);
        }

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="engage">Engage mechanism.</param>
        public void BreakEngage(IEngagedMechanism engage)
        {
            var mechanism = engage as Mechanism;
            if (engages.Contains(mechanism))
                engages.Remove(mechanism);
        }

        /// <summary>
        /// Engage this mechanism to power mechanism.
        /// </summary>
        /// <param name="engage">Power mechanism.</param>
        public void EngageTo(IEngageMechanism engage)
        {
            if (engage == null || engage == this.engage)
                return;
            else
            {
                EngageBreak();

                //Engage this mechanism to new power mechanism.
                engage.BuildEngage(this);
                this.engage = engage;
            }
        }

        /// <summary>
        /// Break engage from power mechanism.
        /// </summary>
        public void EngageBreak()
        {
            if (engage != null)
            {
                engage.BreakEngage(this);
                engage = null;
            }
        }
        #endregion
    }
}