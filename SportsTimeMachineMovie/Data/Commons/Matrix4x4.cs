using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsTimeMachine.Data.Commons
{
    /// <summary>
    /// 行列を表すクラス.
    /// </summary>
    public class Matrix4x4 : IEquatable<Matrix4x4>
    {
        public float[,] m;

        /// <summary>
        /// ゼロ行列を生成する.
        /// </summary>
        public Matrix4x4()
            :this(0.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 0.0f
                )
        {
        }

        /// <summary>
        /// 行列を生成する.
        /// </summary>
        public Matrix4x4(
			float m00, float m01, float m02, float m03,
			float m10, float m11, float m12, float m13,
			float m20, float m21, float m22, float m23,
			float m30, float m31, float m32, float m33
        )
        {
            m = new float[4, 4];
            m[0, 0] = m00;
            m[0, 1] = m01;
            m[0, 2] = m02;
            m[0, 3] = m03;
            m[1, 0] = m10;
            m[1, 1] = m11;
            m[1, 2] = m12;
            m[1, 3] = m13;
            m[2, 0] = m20;
            m[2, 1] = m21;
            m[2, 2] = m22;
            m[2, 3] = m23;
            m[3, 0] = m30;
            m[3, 1] = m31;
            m[3, 2] = m32;
            m[3, 3] = m33;
        }

        /// <summary>
        /// すべての行列の要素が等しければ等しい.
        /// </summary>
        public override bool Equals(Object obj)
        {
            Matrix4x4 mat = obj as Matrix4x4;
            if (mat == null) return false;
            return Equals((Matrix4x4)mat);
        }

        /// <summary>
        /// すべての行列の要素が等しければ等しい.
        /// </summary>
        public bool Equals(Matrix4x4 other)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (m[i, j] != other.m[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 掛け算する.
        /// </summary>
        public static Matrix4x4 operator* (Matrix4x4 left, Matrix4x4 right)
        {
            Matrix4x4 mat = new Matrix4x4();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        mat.m[i, j] += left.m[i, k] * right.m[k, j];
                    }
                }
            }

            return mat;
        }

        /// <summary>
        /// 掛け算する.
        /// 4*4行列×4*1行列は本来計算できないが、
        /// 4*1行列×4*4行列に
        /// 入れ替えて計算する.
        /// </summary>
        public static Vector4 operator *(Matrix4x4 left, Vector4 right)
        {
            return right * left;
        }

        /// <summary>
        /// 掛け算する.
        /// </summary>
        public static Vector4 operator *(Vector4 left, Matrix4x4 right)
        {
            Vector4 vec = new Vector4();

            vec.x = left.x * right.m[0, 0] + left.y * right.m[0, 1] + 
                left.z * right.m[0, 2] + left.w * right.m[0, 3];

            vec.y = left.x * right.m[1, 0] + left.y * right.m[1, 1] +
                left.z * right.m[1, 2] + left.w * right.m[1, 3];

            vec.z = left.x * right.m[2, 0] + left.y * right.m[2, 1] +
                left.z * right.m[2, 2] + left.w * right.m[2, 3];

            vec.w = left.x * right.m[3, 0] + left.y * right.m[3, 1] +
                left.z * right.m[3, 2] + left.w * right.m[3, 3];

            return vec;
        }
    }
}
