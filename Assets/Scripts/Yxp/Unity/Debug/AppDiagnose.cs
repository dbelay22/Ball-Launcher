using System;
using System.Collections;
using UnityEngine;


namespace Yxp.Debug
{
    public class AppDiagnose : MonoBehaviour
    {
        public static AppDiagnose Instance { get; private set; }

        [Header("Settings")]
        [SerializeField] private bool _diagnoseMemory;
        [SerializeField] private bool _diagnoseRendering;

        Coroutine _diagnoseHeapCoroutine;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            ApplySettings();
        }

        void ApplySettings()
        {
            DiagnoseMemory(_diagnoseMemory);
            DiagnoseRendering(_diagnoseRendering);
        }

        void DiagnoseRendering(bool enabled)
        {
            if (enabled)
            {
                YLogger.Verbose($"AppDiagnose] Quality level: {QualitySettings.GetQualityLevel()}");
                YLogger.Verbose($"AppDiagnose] vSyncCount: {QualitySettings.vSyncCount}");
                YLogger.Verbose($"AppDiagnose] particleRaycastBudget: {QualitySettings.particleRaycastBudget}");
                YLogger.Verbose($"AppDiagnose] pixelLightCount: {QualitySettings.pixelLightCount}");
            }
        }

        void DiagnoseMemory(bool enabled)
        {
            if (enabled)
            {
                Application.lowMemory += OnLowMemory;

                _diagnoseHeapCoroutine = StartCoroutine(DiagnoseHeapSize());
            }
            else
            {
                Application.lowMemory -= OnLowMemory;

                StopCoroutine(_diagnoseHeapCoroutine);
            }
        }

        void OnLowMemory()
        {
            if (!_diagnoseMemory)
            {
                return;
            }

            YLogger.Warning($"AppDiagnose] OnLowMemory) !!! heap size: {GetHeapSize()}");
        }
        
        IEnumerator DiagnoseHeapSize()
        {
            while (_diagnoseMemory)
            {
                YLogger.Verbose($"AppDiagnose] DiagnoseMemory) heap size: {GetHeapSize()}");

                yield return new WaitForSeconds(3f);
            }
        }

        private string GetHeapSize()
        {
            long heapSize = GC.GetTotalMemory(false) / 1000000L;
            
            return String.Format("{0}Mb", heapSize);
        }

        void OnDestroy()
        {
            _diagnoseMemory = false;
            
            _diagnoseRendering = false;
            
            ApplySettings();
        }
    }
}
