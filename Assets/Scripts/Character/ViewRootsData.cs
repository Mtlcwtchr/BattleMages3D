using UnityEngine;

namespace Character
{
    public class ViewRootsData
    {
        public Transform MissileCastRoot { get; private set; }
        
        public Transform Origin { get; private set; }

        public ViewRootsData(Transform origin, Transform missileCastRoot)
        {
            Origin = origin;
            MissileCastRoot = missileCastRoot;
        }
    }
}