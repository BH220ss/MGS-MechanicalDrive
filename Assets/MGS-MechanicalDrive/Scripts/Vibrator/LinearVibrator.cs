/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LinearVibrator.cs
 *  Description  :  Define LinearVibrator component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Linear vibrator.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/LinearVibrator")]
    public class LinearVibrator : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Amplitude radius of vibrator.
        /// </summary>
        public float amplitudeRadius = 0.1f;

        /// <summary>
        /// Start loacal position.
        /// </summary>
        public Vector3 StartPosition { protected set; get; }

        /// <summary>
        /// Vibrate local axis.
        /// </summary>
        protected Vector3 LocalAxis
        {
            get
            {
                if (transform.parent)
                    return transform.parent.InverseTransformDirection(transform.forward);
                else
                    return transform.forward;
            }
        }

        /// <summary>
        /// Current offset base on start position.
        /// </summary>
        protected float currentOffset;

        /// <summary>
        /// Vibrate direction.
        /// </summary>
        protected int direction = 1;
        #endregion

        #region Protected Method
        protected override void Awake()
        {
            base.Awake();
            StartPosition = transform.localPosition;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive vibrator by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public override void Drive(float velocity)
        {
            currentOffset += velocity * Mathf.Deg2Rad * direction * Time.deltaTime;
            if (currentOffset < -amplitudeRadius || currentOffset > amplitudeRadius)
            {
                direction *= -1;
                currentOffset = Mathf.Clamp(currentOffset, -amplitudeRadius, amplitudeRadius);
            }
            transform.localPosition = StartPosition + LocalAxis * currentOffset;
        }
        #endregion
    }
}