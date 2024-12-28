using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Editor
{
    public class CodeFormat
    {
        public static string PoolingTypeFormat =
        @"namespace BGD.ObjectPooling
        {{
            public enum PoolingType
            {{
                {0}
            }}
        }}";
    }
}
