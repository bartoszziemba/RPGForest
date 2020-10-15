using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Misc
{
    [System.Serializable]
    public struct SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(float rX, float rY, float rZ)
        {
            x = rX;
            y = rY;
            z = rZ;
        }

        public SerializableVector3(string repr)
        {
            try
            {
                repr = repr.Trim('[');
                repr = repr.Trim(']');
                string[] vals = repr.Split(';');
                foreach (var v in vals)
                {
                    v.Trim();
                }
                x = float.Parse(vals[0]);
                y = float.Parse(vals[1]);
                z = float.Parse(vals[2]);
            }
            catch (System.Exception)
            {
                throw new System.InvalidCastException("Passed string was not a correct Vector3 representation");
            }
        }

        public override string ToString()
        {
            return $"[{x}; {y}; {z}]";
        }

        public static implicit operator Vector3(SerializableVector3 rValue)
        {
            return new Vector3(rValue.x, rValue.y, rValue.z);
        }

        public static implicit operator SerializableVector3(Vector3 rValue)
        {
            return new SerializableVector3(rValue.x, rValue.y, rValue.z);
        }
    }
}