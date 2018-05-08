/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Engine.cs
 *  Description  :  Define Engine component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/Engine")]
    public class Engine : Synchronizer
    {
        #region Field and Property
        /// <summary>
        /// Engine output power.
        /// </summary>
        public float power = 100;

        /// <summary>
        /// Damper of engine power.
        /// </summary>
        protected Damper damper;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            damper = GetComponent<Damper>();
        }

        protected virtual void Update()
        {
            Drive(power);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Turn on engine.
        /// </summary>
        public virtual void TurnOn()
        {
            if (damper)
                damper.BeginAccelerate();
            enabled = true;
        }

        /// <summary>
        /// Turn off engine.
        /// </summary>
        public virtual void TurnOff()
        {
            if (damper)
                damper.BeginDecelerate();
            else
                enabled = false;
        }
        #endregion
    }
}