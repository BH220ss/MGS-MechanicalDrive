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

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Belt with UV animation.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Belt")]
    [RequireComponent(typeof(Renderer))]
    public class Belt : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Renderer of belt.
        /// </summary>
        protected Renderer beltRenderer;
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize belt.
        /// </summary>
        public override void Initialize()
        {
            beltRenderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// Drive belt by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            beltRenderer.material.mainTextureOffset += new Vector2(velocity * Time.deltaTime, 0);
        }
        #endregion
    }
}