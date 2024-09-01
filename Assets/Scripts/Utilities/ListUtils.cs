using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ListUtils
    {
        public static T RandomElement<T>(this List<T> list) => list.Count < 1 ? default(T) : list[Random.Range(0, list.Count)];
    }
}