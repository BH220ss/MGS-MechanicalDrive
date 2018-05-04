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
    public class Belt : BaseMechanism
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
        /// <param name="speed">Line speed.</param>
        public override void Drive(float speed)
        {
            beltRenderer.material.mainTextureOffset += new Vector2(speed * Mathf.Deg2Rad * Time.deltaTime, 0);
        }
        #endregion
    }
}