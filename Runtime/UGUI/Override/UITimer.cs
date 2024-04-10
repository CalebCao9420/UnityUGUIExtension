using System;
using UnityEngine;

namespace IG.Runtime.Extension.UGUI{
    public class UITimer : MonoBehaviour{
        /// <summary>
        /// step
        /// </summary>
        public float step = 1f;

        public  Action StepEvent;
        private float  sartNum = 0f;
        private bool   isPause;

        void FixedUpdate(){
            if (isPause){
                return;
            }

            sartNum += Time.deltaTime;
            if (sartNum >= step){
                sartNum = sartNum - step;
                if (StepEvent != null){
                    StepEvent.Invoke();
                }
            }
        }

        public void Pause(){ isPause = true; }
        public void Play() { isPause = false; }

        public void RePlay(){
            sartNum = 0;
            Play();
        }
    }
}