// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace System
{
    /// <summary>
    /// Represents a type with a single value.
    /// This type is often used to denote the successful completion of a void-returning method (C#) or a Sub procedure (Visual Basic).
    /// </summary>
#if !NO_SERIALIZABLE
    [Serializable]
#endif
    public struct Unit : IComparable, IComparable<Unit>, IEquatable<Unit>
    {
        /// <summary>
        /// Compares this instance to a specified object and returns an integer that indicates their relationship to one another.
        /// </summary>
        /// <param name="obj">An object to compare to this instance, or <c>null</c>.</param>
        /// <returns>A signed integer that indicates the relative order of this instance and <paramref name="obj"/>.</returns>
        /// <exception cref="ArgumentException"><paramref name="obj"> is not a Unit.</exception>
        /// <remarks>
        /// <paramref name="obj"/> must be <c>null</c> or an instance of Unit; otherwise, an exception is thrown.
        /// </remarks>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            
            if (!(obj is Unit))
            {
                throw new ArgumentException(Environment.GetResourceString("Arg_MustBeUnit"));
            }
            
            return 0;
        }
    
        /// <summary>
        /// Compares this instance to a specified Unit object and returns an integer that indicates their relationship to one another.
        /// </summary>
        /// <param name="other">An object to compare to the current Unit value.</param>
        /// <returns>
        /// A signed integer that indicates the relative order of this instance and <paramref name="other"/>.
        /// Unit has only one value, so the returned integer is always zero.
        /// </returns>
        public int CompareTo(Unit other)
        {
            return 0;
        }
    
        /// <summary>
        /// Determines whether the specified Unit values is equal to the current Unit.
        /// </summary>
        /// <param name="other">An object to compare to the current Unit value.</param>
        /// <returns>Because Unit has a single value, this always returns <c>true</c>.</returns>
        public bool Equals(Unit other)
        {
            return true;
        }

        /// <summary>
        /// Determines whether the specified System.Object is equal to the current Unit.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current Unit.</param>
        /// <returns><c>true</c> if the specified System.Object is a Unit value; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return obj is Unit;
        }

        /// <summary>
        /// Returns the hash code for the current Unit value.
        /// </summary>
        /// <returns>A hash code for the current Unit value.</returns>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// Returns a string representation of the current Unit value.
        /// </summary>
        /// <returns>String representation of the current Unit value.</returns>
        public override string ToString()
        {
            return "()";
        }

        /// <summary>
        /// Determines whether the two specified Unit values are equal.
        /// Because Unit has a single value, this always returns <c>true</c>.
        /// </summary>
        /// <param name="first">The first Unit value to compare.</param>
        /// <param name="second">The second Unit value to compare.</param>
        /// <returns>Because Unit has a single value, this always returns <c>true</c>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters",
            MessageId = "first", Justification = "Parameter required for operator overloading.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters",
            MessageId = "second", Justification = "Parameter required for operator overloading.")]
        public static bool operator ==(Unit first, Unit second)
        {
            return true;
        }

        /// <summary>
        /// Determines whether the two specified Unit values are not equal.
        /// Because Unit has a single value, this always returns <c>false</c>.
        /// </summary>
        /// <param name="first">The first Unit value to compare.</param>
        /// <param name="second">The second Unit value to compare.</param>
        /// <returns>Because Unit has a single value, this always returns <c>false</c>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters",
            MessageId = "first", Justification = "Parameter required for operator overloading.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters",
            MessageId = "second", Justification = "Parameter required for operator overloading.")]
        public static bool operator !=(Unit first, Unit second)
        {
            return false;
        }
    }
}
