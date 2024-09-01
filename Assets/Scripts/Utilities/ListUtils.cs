using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ListUtils
    {
        public static T RandomElement<T>(this List<T> list)
        {
            if (list.Count < 1)
                return default(T);

            return list[Random.Range(0, list.Count)];
        }
    }
}