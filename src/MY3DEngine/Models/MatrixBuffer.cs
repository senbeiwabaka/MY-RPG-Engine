// <copyright file="MatrixBuffer.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.Models
{
    using System.Numerics;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MatrixBuffer
    {
        public MatrixBuffer(Matrix4x4 world, Matrix4x4 view, Matrix4x4 projection)
        {
            this.World = world;
            this.View = view;
            this.Projection = projection;
        }

        /// <summary>
        /// Gets the World from the matrix
        /// </summary>
        public Matrix4x4 World { get; }

        /// <summary>
        /// Gets View from the matrix
        /// </summary>
        public Matrix4x4 View { get; }

        /// <summary>
        /// Gets Projection from the matrix
        /// </summary>
        public Matrix4x4 Projection { get; }

        public static bool operator ==(MatrixBuffer left, MatrixBuffer right) => left.Equals(right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(MatrixBuffer left, MatrixBuffer right) => !(left == right);

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
