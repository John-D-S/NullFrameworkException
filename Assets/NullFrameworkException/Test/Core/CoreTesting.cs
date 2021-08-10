using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.Test.Core
{
    public class CoreTesting : MonoBehaviour
    {
        public Transform cube;

        private RunnableTest runnableTest;
        
        // Start is called before the first frame update
        void Start()
        {
            RunnableUtils.Setup(ref runnableTest, gameObject, cube, 1f, Vector3.up);
        }

        // Update is called once per frame
        void Update()
        {
            RunnableUtils.Run(ref runnableTest, gameObject, Input.GetKey(KeyCode.Space));
        }
    }
}
