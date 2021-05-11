using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighContrastMode.Example {
    public class PlayerController : MonoBehaviour {

        private void FixedUpdate() {
            var moveX = Input.GetAxis("Horizontal");
            var moveZ = Input.GetAxis("Vertical");

            var pos = transform.position;
// print(pos, moveX);
            transform.position.Set(pos.x + moveX, pos.y, pos.z + moveZ);
        }
    }
}