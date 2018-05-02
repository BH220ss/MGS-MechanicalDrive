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

namespace Mogoson.MechanicalDrive
{
    [AddComponentMenu("Mogoson/MechanicalDrive/Belt")]
    [RequireComponent(typeof(Renderer))]
    public class Belt : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Renderer of belt.
        /// </summary>
        protected Renderer beltRenderer;
        #endregion

        #region Private Method
        protected virtual void Awake()
        {
            beltRenderer = GetComponent<Renderer>();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive belt.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            beltRenderer.material.mainTextureOffset += new Vector2(velocity * Mathf.Deg2Rad * Time.deltaTime, 0);
        }
        #endregion
    }
}