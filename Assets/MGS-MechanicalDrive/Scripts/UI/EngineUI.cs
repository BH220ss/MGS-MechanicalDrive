/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EngineUI.cs
 *  Description  :  Draw scene UI to control Engine.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [RequireComponent(typeof(Engine))]
    public class EngineUI : MonoBehaviour
    {
        #region Field and Property
        public float top = 10;
        public float left = 10;

        protected Engine engine;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            engine = GetComponent<Engine>();
        }

        protected virtual void OnGUI()
        {
            GUILayout.Space(top);
            GUILayout.BeginHorizontal();
            GUILayout.Space(left);
            if (GUILayout.Button("Turn On Engine"))
                engine.TurnOn();
            if (GUILayout.Button("Turn Off Engine"))
                engine.TurnOff();
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}