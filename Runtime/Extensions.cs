using UnityEngine;

namespace LouieLib
{
    public static class Extensions
    {
        public static Vector2 WorldToScreenPointProjected(this Camera camera, Vector3 worldPosition)
        {
            var cameraTransform = camera.transform;
            var cameraNormal = cameraTransform.forward;
            var direction = worldPosition - cameraTransform.position;
            
            var camNormDot = Vector3.Dot(cameraNormal, direction);
            if (camNormDot > 0)
            {
                return RectTransformUtility.WorldToScreenPoint(camera, worldPosition);
            }

            var proj = cameraNormal * camNormDot * 1.01f;
            worldPosition = camera.transform.position + (direction - proj);
            return RectTransformUtility.WorldToScreenPoint(camera, worldPosition);
        }

        public static T[] Concat<T>(this T[] array1, T[] array2)
        {
            var result = new T[array1.Length + array2.Length];
            array1.CopyTo(result, 0);
            array2.CopyTo(result, array1.Length);
            return result;
        }
    }
}