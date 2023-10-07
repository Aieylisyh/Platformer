using System.Collections;
using UnityEngine;

namespace Assets.Game.Script.SceneObject
{
    public class Killzone : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            var sp = GetComponent<SpriteRenderer>();
            if (sp)
                sp.enabled = false;
        }
    }
}