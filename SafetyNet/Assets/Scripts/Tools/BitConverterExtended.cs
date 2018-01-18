using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class BitConverterExtended
{
    #region float

    public static byte[] GetBytes(float[] floatArray)
    {
        List<byte> bytes = new List<byte>();
        for(int i = 0; i < floatArray.Length; i++)
        {
            bytes.AddRange(BitConverter.GetBytes(floatArray[i]));
        }
        return bytes.ToArray();
    }

    public static float[] ToSingleArray(byte[] bytes)
    {
        return ToFloatArray(bytes);
    }

    public static float[] ToFloatArray(byte[] bytes)
    {
        List<float> floats = new List<float>();
        int offset = 0;
        int size = sizeof(float);
        int count = bytes.Length / size;
        for(int i = 0; i < count; i++)
        {
            floats.Add(BitConverter.ToSingle(bytes, offset));
            offset += size;
        }
        return floats.ToArray();
    }
    
    #endregion

    #region Vector3

    public static byte[] GetBytes(Vector3[] vectors)
    {
        byte[] bytes = new byte[12 * vectors.Length];
        for(int i = 0; i < vectors.Length; i++)
        {
            byte[] vector = GetBytes(vectors[i]);
            vector.CopyTo(bytes, i * 12);
        }
        return bytes;
    }

    public static Vector3[] ToVector3Array(byte[] bytes)
    {
        int vectorCount = bytes.Length / 12;
        Vector3[] vectors = new Vector3[vectorCount];
        for(int i = 0; i < vectorCount; i++)
        {
            int index = i * 12;
            byte[] vectorBytes = new byte[12];
            Array.Copy(bytes, index, vectorBytes, 0, 12);

            vectors[i] = ToVector3(vectorBytes);
        }

        return vectors;
    }

    public static byte[] GetBytes(Vector3 vector3)
    {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(BitConverter.GetBytes(vector3.x));
        bytes.AddRange(BitConverter.GetBytes(vector3.y));
        bytes.AddRange(BitConverter.GetBytes(vector3.z));
        return bytes.ToArray();
    }

    public static Vector3 ToVector3(byte[] bytes)
    {
        return new Vector3
        (
            BitConverter.ToSingle(bytes, 0),
            BitConverter.ToSingle(bytes, 4),
            BitConverter.ToSingle(bytes, 8)
        );
    }

    #endregion

    #region Vector4

    public static byte[] GetBytes(Vector4[] vectors)
    {
        byte[] bytes = new byte[16 * vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            byte[] vectorBytes = GetBytes(vectors[i]);
            vectorBytes.CopyTo(bytes, i * 16);
        }
        return bytes;
    }

    public static Vector4[] ToVector4Array(byte[] bytes)
    {
        int vectorCount = bytes.Length / 16;
        Vector4[] vectors = new Vector4[vectorCount];
        for (int i = 0; i < vectorCount; i++)
        {
            int index = i * 16;
            byte[] vectorBytes = new byte[16];
            Array.Copy(bytes, index, vectorBytes, 0, 16);

            vectors[i] = ToVector4(vectorBytes);
        }

        return vectors;
    }

    public static byte[] GetBytes(Vector4 vector4)
    {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(BitConverter.GetBytes(vector4.x));
        bytes.AddRange(BitConverter.GetBytes(vector4.y));
        bytes.AddRange(BitConverter.GetBytes(vector4.z));
        bytes.AddRange(BitConverter.GetBytes(vector4.w));
        return bytes.ToArray();
    }

    public static Vector4 ToVector4(byte[] bytes)
    {
        return new Vector4
        (
            BitConverter.ToSingle(bytes, 0),
            BitConverter.ToSingle(bytes, 4),
            BitConverter.ToSingle(bytes, 8),
            BitConverter.ToSingle(bytes, 12)
        );
    }

    #endregion

    #region Quaternion

    public static byte[] GetBytes(Quaternion[] quaternions)
    {
        byte[] bytes = new byte[16 * quaternions.Length];
        for (int i = 0; i < quaternions.Length; i++)
        {
            byte[] quaternionBytes = GetBytes(quaternions[i]);
            quaternionBytes.CopyTo(bytes, i * 16);
        }
        return bytes;
    }

    public static Quaternion[] ToQuaternionArray(byte[] bytes)
    {
        int count = bytes.Length / 16;
        Quaternion[] quaternions = new Quaternion[count];
        for (int i = 0; i < count; i++)
        {
            int index = i * 16;
            byte[] quaternionBytes = new byte[16];
            Array.Copy(bytes, index, quaternionBytes, 0, 16);

            quaternions[i] = ToQuaternion(quaternionBytes);
        }

        return quaternions;
    }

    public static byte[] GetBytes(Quaternion quaternion)
    {
        List<byte> bytes = new List<byte>();
        bytes.AddRange(BitConverter.GetBytes(quaternion.x));
        bytes.AddRange(BitConverter.GetBytes(quaternion.y));
        bytes.AddRange(BitConverter.GetBytes(quaternion.z));
        bytes.AddRange(BitConverter.GetBytes(quaternion.w));
        return bytes.ToArray();
    }

    public static Quaternion ToQuaternion(byte[] bytes)
    {
        return new Quaternion
        (
            BitConverter.ToSingle(bytes, 0),
            BitConverter.ToSingle(bytes, 4),
            BitConverter.ToSingle(bytes, 8),
            BitConverter.ToSingle(bytes, 12)
        );
    }

    #endregion
}
