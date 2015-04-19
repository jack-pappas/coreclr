﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

/*============================================================
**
**
**
** Purpose: Convenient wrapper for a string, an offset, and
**          a count.  Ideally used in streams, collections,
**          and parsers.
**
**
===========================================================*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public struct Substring : IEquatable<Substring>, IComparable<Substring>
        // IEquatable<string>, IComparable<string>, IStructuralComparable
        // : IEnumerable, IEnumerable<char>, IReadOnlyCollection<char>, IReadOnlyList<char>
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly string m_string;
        
        /// <summary>
        /// 
        /// </summary>
        private readonly int m_offset;

        /// <summary>
        /// 
        /// </summary>
        private readonly int m_count;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        public Substring(string str)
        {
            // Contracts
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            Contract.EndContractBlock();

            // Assign field values.
            m_string = str;
            m_offset = 0;
            m_count = str.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public Substring(string str, int offset, int count)
        {
            // Contracts
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            else if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            else if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            else if (str.Length - offset < count)
            {
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLenStr"));
            }
            Contract.EndContractBlock();

            // Assign field values.
            m_string = str;
            m_offset = offset;
            m_count = count;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char this[int index]
        {
            get
            {
                if (m_string == null)
                {
                    throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
                }
                else if (index < 0 || index >= m_count)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                Contract.EndContractBlock();

                return m_string[m_offset + index];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                // Since copying value types is not atomic & callers cannot atomically 
                // read all three fields, we cannot guarantee that Count is within 
                // the bounds of String.  That's our intent, but let's not specify 
                // it as a postcondition - force callers to re-verify this themselves
                // after reading each field out of a Substring into their stack.
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.EndContractBlock();

                Contract.Assert((null == m_string && 0 == m_offset && 0 == m_count)
                                 || (null != m_string && m_offset >= 0 && m_count >= 0 && m_offset + m_count <= m_string.Length),
                                "Substring is invalid");

                return m_count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty
        {
            get { return this.Count == 0; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Offset
        {
            get
            {
                // Since copying value types is not atomic & callers cannot atomically 
                // read all three fields, we cannot guarantee that Offset is within 
                // the bounds of String.  That is our intent, but let's not specify 
                // it as a postcondition - force callers to re-verify this themselves
                // after reading each field out of a Substring into their stack.
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.EndContractBlock();

                Contract.Assert((null == m_string && 0 == m_offset && 0 == m_count)
                                 || (null != m_string && m_offset >= 0 && m_count >= 0 && m_offset + m_count <= m_string.Length),
                                "Substring is invalid");

                return m_offset;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string String
        {
            get
            {
                Contract.Assert((null == m_string && 0 == m_offset && 0 == m_count)
                                 || (null != m_string && m_offset >= 0 && m_count >= 0 && m_offset + m_count <= m_string.Length),
                                "Substring is invalid");

                return m_string;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int Compare(Substring other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Alias for <see cref="Compare(Substring)"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Substring other)
        {
            return Compare(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public int Compare(Substring other, StringComparer comparer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public static int CompareOrdinal(Substring other)
        {
            throw new NotImplementedException();
        }

        ////
        //[Pure]
        //public bool Contains(string value)
        //{
        //    return IndexOf(value, StringComparison.Ordinal) >= 0;
        //}

        public override bool Equals(object obj)
        {
            return (obj is Substring) ? Equals((Substring)obj) : false;
        }

        public bool Equals(Substring other)
        {
            // TODO OPT : When comparing the strings, only compare the sections spanned by the substrings.
            return other.m_offset == m_offset && other.m_count == m_count && other.m_string == m_string;
        }

        public bool Equals(Substring other, StringComparer comparer)
        {
            // TODO OPT : When comparing the strings, only compare the sections spanned by the substrings.
            return other.m_offset == m_offset && other.m_count == m_count && comparer.Equals(other.m_string, m_string);
        }

        public override int GetHashCode()
        {
            // TODO OPT: Only hash the substring (instead of the whole underlying string).
            return m_string == null ? 0 : (m_string.GetHashCode() ^ m_offset ^ m_count);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified Unicode character in this substring.
        /// </summary>
        /// <param name="value">A Unicode character to seek.</param>
        /// <returns>The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not.</returns>
        [Pure]
        public int IndexOf(char value)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            Contract.EndContractBlock();

            return IndexOf(value, 0);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified Unicode character in this substring.
        /// The search starts at a specified character position.
        /// </summary>
        /// <param name="value">A Unicode character to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <returns>The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is negative</exception>
        /// <exception cref="ArgumentOutOfRangeException">The substring is empty and <paramref name="startIndex"/> is greater than 0</exception>
        /// <exception cref="ArgumentOutOfRangeException">The substring is non-empty and <paramref name="startIndex"/> is greater than <see cref="Count"/></exception>
        [Pure]
        public int IndexOf(char value, int startIndex)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            else if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            else if (!this.IsEmpty && startIndex >= this.Count)
            {
                throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Argument_InvalidOffLenStr"));
            }
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < this.Count);
            Contract.EndContractBlock();

            // OPT : If this is an empty substring, return the result immediately.
            if (this.IsEmpty) { return -1; }
            else
            {
                var CharIndex = this.String.IndexOf(value, this.Offset + startIndex, this.Count - startIndex);
                
                // If the character was found, convert the index in the underlying string to an index into this substring before returning.
                return CharIndex == -1 ? -1 : CharIndex - this.Offset;
            }
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance.
        /// </summary>
        /// <param name="value">The Unicode character to seek.</param>
        /// <returns>
        /// The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not found
        /// or if the current instance is an empty substring.
        /// </returns>
        [Pure]
        public int LastIndexOf(char value)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < this.Count);
            Contract.EndContractBlock();

            // OPT : If this is an empty substring, return the result immediately.
            return this.IsEmpty ? -1 : LastIndexOf(value, this.Count - 1);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance.
        /// The search starts at a specified character position and proceeds backward toward the beginning of the substring.
        /// </summary>
        /// <param name="value">The Unicode character to seek.</param>
        /// <param name="startIndex">
        /// The starting position of the search. The search proceeds from <paramref name="startIndex"/> toward the beginning of this instance.
        /// </param>
        /// <returns>The zero-based index position of <paramref name="value"/> if that character is found, or -1 if it is not.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is negative</exception>
        /// <exception cref="ArgumentOutOfRangeException">The substring is empty and <paramref name="startIndex"/> is greater than 0</exception>
        /// <exception cref="ArgumentOutOfRangeException">The substring is non-empty and <paramref name="startIndex"/> is greater than <see cref="Count"/></exception>
        [Pure]
        public int LastIndexOf(char value, int startIndex)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            else if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            else if (!this.IsEmpty && startIndex >= this.Count)
            {
                throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Argument_InvalidOffLenStr"));
            }
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < this.Count);
            Contract.EndContractBlock();

            // OPT : If this is an empty substring, return the result immediately.
            if (this.IsEmpty) { return -1; }
            else
            {
                var CharIndex = this.String.LastIndexOf(value, this.Offset + startIndex, this.Count - startIndex);

                // If the character was found, convert the index in the underlying string to an index into this substring before returning.
                return CharIndex == -1 ? -1 : CharIndex - this.Offset;
            }
        }

        /// <summary>
        /// Determines whether the beginning of this substring value matches the specified string.
        /// </summary>
        /// <param name="value">The string to compare.</param>
        /// <returns>
        /// <c>true</c> if this instance begins with <paramref name="value"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
        [Pure]
        public bool StartsWith(string value)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            else if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            Contract.EndContractBlock();

            return StartsWith(value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Pure]
        public bool StartsWith(Substring value)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            Contract.EndContractBlock();

            //
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the beginning of this substring value matches the specified string
        /// when compared using the specified comparison option.
        /// </summary>
        /// <param name="value">The string to compare.</param>
        /// <param name="comparisonType">
        /// One of the enumeration values that determines how this substring and <paramref name="value"/> are compared.
        /// </param>
        /// <returns>
        /// <c>true</c> if this substring begins with <paramref name="value"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparisonType"/> is not a valid <see cref="StringComparison"/> value.
        /// </exception>
        [Pure]
        public bool StartsWith(string value, StringComparison comparisonType)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            } 
            else if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            else if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
            {
                throw new ArgumentException("The comparison type is not supported.", "comparisonType");
            }
            Contract.EndContractBlock();

            var ValueLength = value.Length;

            // If the value string is larger than this substring, the substring cannot start with the value.
            if (ValueLength > this.Count) { return false; }
            else
            {
                // Compare the string to beginning of this substring using the specified comparer.
                return String.Compare(
                    this.String, this.Offset,
                    value, 0,
                    Math.Min(this.Count, ValueLength),
                    comparisonType) == 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        [Pure]
        public bool StartsWith(Substring value, StringComparison comparisonType)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            else if (value.String == null)
            {
                throw new ArgumentException("The substring is backed by a null string.", "value");
            }
            else if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
            {
                throw new ArgumentException("The comparison type is not supported.", "comparisonType");
            }
            Contract.EndContractBlock();

            // If the value string is larger than this substring, the substring cannot start with the value.
            if (value.Count > this.Count) { return false; }
            else
            {
                // Compare the string to beginning of this substring using the specified comparer.
                return String.Compare(
                    this.String, this.Offset,
                    value.String, value.Offset,
                    Math.Min(this.Count, value.Count),
                    comparisonType) == 0;
            }
        }

        /// <summary>
        /// Determines whether the beginning of this substring value matches the specified string
        /// when compared using the specified culture.
        /// </summary>
        /// <param name="value">The string to compare.</param>
        /// <param name="ignoreCase">
        /// <c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.
        /// </param>
        /// <param name="culture">
        /// Cultural information that determines how this string and <paramref name="value"/> are compared.
        /// If <paramref name="culture"/> is null, the current culture is used.
        /// </param>
        /// <returns>
        /// <c>true</c> if this substring begins with <paramref name="value"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
        [Pure]
        public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            else if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            Contract.EndContractBlock();

            var ValueLength = value.Length;

            // If the value string is larger than this substring, the substring cannot start with the value.
            if (ValueLength > this.Count) { return false; }
            else
            {
                // Compare the string to beginning of this substring using the comparison for the specified culture.
                return String.Compare(
                    this.String, this.Offset,
                    value, 0,
                    Math.Min(this.Count, ValueLength),
                    ignoreCase,
                    culture == null ? CultureInfo.CurrentCulture : culture) == 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        [Pure]
        public bool StartsWith(Substring value, bool ignoreCase, CultureInfo culture)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            } 
            else if (value.String == null)
            {
                throw new ArgumentException("The substring is backed by a null string.", "value");
            }
            Contract.EndContractBlock();

            // If the value string is larger than this substring, the substring cannot start with the value.
            if (value.Count > this.Count) { return false; }
            else
            {
                // Compare the string to beginning of this substring using the comparison for the specified culture.
                return String.Compare(
                    this.String, this.Offset,
                    value.String, value.Offset,
                    Math.Min(this.Count, value.Count),
                    ignoreCase,
                    culture == null ? CultureInfo.CurrentCulture : culture) == 0;
            }
        }

        /// <summary>
        /// Copies the characters in this substring into a Unicode character array.
        /// </summary>
        /// <returns></returns>
        public char[] ToCharArray()
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            Contract.EndContractBlock();

            // OPT : If this substring is empty, return an empty array.
            if (this.IsEmpty) { return new char[] { }; }

#if FX_ATLEAST_PORTABLE
            // Manually implement String.ToCharArray(int,int) for portable libraries.
            var chars = new char[this.Count];
            for (var i = 0; i < this.Count; i++)
            {
                chars[i] = m_string[m_offset + i];
            }
            return chars;
#else
            return this.String.ToCharArray (this.Offset, this.Count);
#endif
        }

        public override string ToString()
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            Contract.EndContractBlock();

            // OPT : If the substring is empty, immediately return an empty string.
            if (this.IsEmpty) { return String.Empty; }
 
            // OPT : If the substring spans the entire underlying string, just return the string itself.
            if (this.Offset == 0 && this.Count == this.String.Length) { return this.String; }
            else { return this.String.Substring(this.Offset, this.Count); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="remaining"></param>
        /// <returns></returns>
        public bool TryRead(out char value, out Substring remaining)
        {
            if (m_string == null)
            {
                throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullString"));
            }
            Contract.EndContractBlock();

            // If the string is empty, assign default/empty values to the out parameters and return false.
            if (this.IsEmpty)
            {
                value = default(char);
                remaining = this;
                return false;
            }
            else
            {
                // Remove the first character from the substring; return it along with the remaining substring.
                value = m_string[this.Offset];
                remaining = new Substring(m_string, m_offset + 1, m_count);
                return true;
            }
        }

        #endregion

        #region Operators

        public static bool operator ==(Substring a, Substring b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Substring a, Substring b)
        {
            return !a.Equals(b);
        }

        #endregion
    }
}
