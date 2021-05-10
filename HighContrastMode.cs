// MIT License

// Copyright (c) 2021 NedMakesGames

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class HighContrastMode : ScriptableRendererFeature {

    [System.Serializable]
    public class Overrides {
        [HideInInspector]
        public Material overrideMaterial;
        public FilterSettings filterSettings = new FilterSettings();
        public LayerMask LayerMask;
        public Color color;
        [Range(0f,0.002f)]
        public float OutlineThickness;
        [Range(0f,15f)]
        public float OutlineBrightness;
        [Range(0f,1f)]
        public float ShaderTransparency;
        // public List<string> PassNames = new List<string>();
    }

    [System.Serializable]
    public class FilterSettings
    {
        public RenderQueueType RenderQueueType;
        public LayerMask LayerMask;
        public List<string> PassNames = new List<string>();

        public FilterSettings()
        {
            RenderQueueType = RenderQueueType.Opaque;
            LayerMask = 0;
        }
    }

    public RenderObjects.RenderObjectsSettings settings = new RenderObjects.RenderObjectsSettings();

    private RenderObjectsPass renderObjectsPass;
    private RenderObjectsPass renderObjectsPass2;
    private List<RenderObjectsPass> renderObjectsPasses = new List<RenderObjectsPass>();
    private Material myMaterial;

    public List<Overrides> overrides = new List<Overrides>();
    public int overrideMaterialPassIndex2 = 0;
    public LayerMask layerMask1;

    private DepthNormalsPass depthNormalsPass;

    public override void Create() {

        RenderObjects.FilterSettings filter = settings.filterSettings;

        // Render Objects pass doesn't support events before rendering prepasses.
        // The camera is not setup before this point and all rendering is monoscopic.
        // Events before BeforeRenderingPrepasses should be used for input texture passes (shadow map, LUT, etc) that doesn't depend on the camera.
        // These events are filtering in the UI, but we still should prevent users from changing it from code or
        // by changing the serialized data.
        if (settings.Event < RenderPassEvent.BeforeRenderingPrepasses)
            settings.Event = RenderPassEvent.BeforeRenderingPrepasses;

        renderObjectsPasses.Clear();
        foreach (var item in overrides) {
            item.overrideMaterial = new Material(Shader.Find("Shader Graphs/HighContrastShader"));
            item.overrideMaterial.SetColor("_Color", item.color);
            item.overrideMaterial.SetFloat("_OutlineThickness", item.OutlineThickness);
            item.overrideMaterial.SetFloat("_OutlineBrightness", item.OutlineBrightness);
            item.overrideMaterial.SetFloat("_ShaderTransparency", item.ShaderTransparency);

            RenderObjectsPass roPass = new RenderObjectsPass(settings.passTag, settings.Event, filter.PassNames, item.filterSettings.RenderQueueType, item.filterSettings.LayerMask, settings.cameraSettings);
            roPass.overrideMaterialPassIndex = settings.overrideMaterialPassIndex;
            roPass.overrideMaterial = item.overrideMaterial;

            if (settings.overrideDepthState)
                roPass.SetDetphState(settings.enableWrite, settings.depthCompareFunction);
            if (settings.stencilSettings.overrideStencilState)
                roPass.SetStencilState(settings.stencilSettings.stencilReference,
                    settings.stencilSettings.stencilCompareFunction, settings.stencilSettings.passOperation,
                    settings.stencilSettings.failOperation, settings.stencilSettings.zFailOperation);

            renderObjectsPasses.Add(roPass);
        }


        // ***********
        // NedMakesGames code snippets
        // We will use the built-in renderer's depth normals texture shader
        Material material = CoreUtils.CreateEngineMaterial("Hidden/Internal-DepthNormalsTexture");
        this.depthNormalsPass = new DepthNormalsPass(material);
        // Render after shadow caster, depth, etc. passes
        depthNormalsPass.renderPassEvent = RenderPassEvent.AfterRenderingPrePasses;
        // NedMakesGames snippets ends
        // ***********
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {

        foreach (var pass in renderObjectsPasses) {
            renderer.EnqueuePass(pass);
        }



        renderer.EnqueuePass(depthNormalsPass);

    }
}