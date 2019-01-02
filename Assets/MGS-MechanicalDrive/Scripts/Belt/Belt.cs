/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Belt.cs
 *  Description  :  Define Belt component.
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
    /// Belt with UV animation.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Belt")]
    [RequireComponent(typeof(Renderer))]
    public class Belt : Mechanism, IEngageMechanism, IEngagedMechanism
    {
        #region Field and Property
        /// <summary>
        /// Engaged mechanisms.
        /// </summary>
        public List<Mechanism> engages;

        /// <summary>
        /// Coefficient of velocity.
        /// </summary>
        public float coefficient = 1;

        /// <summary>
        /// Engage power mechanism.
        /// </summary>
        protected IEngageMechanism engage;

        /// <summary>
        /// Renderer of belt.
        /// </summary>
        protected Renderer beltRenderer;
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
        /// Initialize belt.
        /// </summary>
        public override void Initialize()
        {
            InitializeEngages();
            beltRenderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// Drive belt by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity of drive.</param>
        /// <param name="type">Invalid parameter (Belt can only drived by linear velocity).</param>
        public override void Drive(float velocity, DriveType type = DriveType.Ignore)
        {
            beltRenderer.material.mainTextureOffset += new Vector2(velocity * coefficient * Time.deltaTime, 0);
            DriveEngages(-velocity);
        }

        /// <summary>
        /// Build engage for mechanism.
        /// </summary>
        /// <param name="engage">Engage mechanism.</param>
        public void BuildEngage(IEngagedMechanism engage)
        {
            var Mechanism = engage as Mechanism;
            if (Mechanism && !engages.Contains(Mechanism))
            {
                engages.Add(Mechanism);
            }
        }

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="engage">Engage mechanism.</param>
        public void BreakEngage(IEngagedMechanism engage)
        {
            var mechanism = engage as Mechanism;
            if (engages.Contains(mechanism))
            {
                engages.Remove(mechanism);
            }
        }

        /// <summary>
        /// Engage this mechanism to power mechanism.
        /// </summary>
        /// <param name="engage">Power mechanism.</param>
        public void EngageTo(IEngageMechanism engage)
        {
            if (engage == null || engage == this.engage)
            {
                return;
            }
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