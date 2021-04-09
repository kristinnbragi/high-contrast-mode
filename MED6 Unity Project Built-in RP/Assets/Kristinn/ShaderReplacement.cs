using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Kristinn {
    [ExecuteInEditMode]
    public class ShaderReplacement : MonoBehaviour {

        public Shader ReplacementShader;

        private void OnEnable() {
            if (ReplacementShader != null) {
                GetComponent<Camera>().SetReplacementShader(ReplacementShader, "RenderType");
            }
        }

        private void OnDisable() {
            GetComponent<Camera>().ResetReplacementShader();
        }

        // // Start is called before the first frame update
        // void Start()
        // {
        //     if (ReplacementShader != null) {
        //         print("here");
        //         GetComponent<Camera>().SetReplacementShader(ReplacementShader, "RenderType");
        //     }
        // }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}