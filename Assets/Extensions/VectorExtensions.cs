using UnityEngine;

namespace srs.Vector
{
    public static class VectorExtensions
    {
        /// <summary>
        ///     Returns the vector with the y component set to 0.
        /// </summary>
        public static Vector3 Flattened(this Vector3 vector)
        {
            return new Vector3(vector.x, 0, vector.z);
        }

        /// <summary>
        ///     Sets the y component of the vector to 0.
        /// </summary>
        public static void Flatten(this Vector3 vector)
        {
            vector.y = 0;
        }
    }
}