using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Complete
{
    [System.Serializable]
    public class PerfectOverride
    {
        public int referenceOrthographicSize;
        public float referencePixelsPerUnit;
    }

    [ExecuteInEditMode]
    public class PerfectPixel : MonoBehaviour
    {
        [Tooltip("the reference resolution to which your game is made for (e.g. 768px)")]
        public int ReferenceOrthographicSize;
        [Tooltip("Reference main PPU (e.g. 32, 64 etc...")]
        public float ReferencePixelsPerUnit;
        public List<PerfectOverride> Overrides;

        private int m_lastSize = 0;

        // Use this for initialization
        void Start()
        {
            UpdateOrthoSize();
        }

        PerfectOverride FindOverride(int size)
        {
            return Overrides.FirstOrDefault(x => x.referenceOrthographicSize == size);
        }

        void UpdateOrthoSize()
        {
            m_lastSize = Screen.height;

            // first find the reference orthoSize
            float refOrthoSize = (ReferenceOrthographicSize / ReferencePixelsPerUnit) * 0.5f;

            // then find the current orthoSize
            var overRide = FindOverride(m_lastSize);
            float ppu = overRide != null ? overRide.referencePixelsPerUnit : ReferencePixelsPerUnit;
            float orthoSize = (m_lastSize / ppu) * 0.5f;

            // the multiplier is to make sure the orthoSize is as close to the reference as possible
            float multiplier = Mathf.Max(1, Mathf.Round(orthoSize / refOrthoSize));

            // then we rescale the orthoSize by the multipler
            orthoSize /= multiplier;

            // set it
            this.GetComponent<Camera>().orthographicSize = orthoSize;
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            if (m_lastSize != Screen.height)
                UpdateOrthoSize();
#endif
        }
    }
}
