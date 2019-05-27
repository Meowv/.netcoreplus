using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Plus.Collections
{
    /// <summary>
    /// A shortcut for <see cref="TypeList{TBaseType}"/> to use object as base type
    /// </summary>
    public class TypeList : TypeList<object>, ITypeList
    {
    }

    /// <summary>
    /// Extends <see cref="List{Type}"/> to add restriction a specific base type.
    /// </summary>
    /// <typeparam name="TBaseType"></typeparam>
    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count { get { return _typeList.Count; } }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Type this[int index]
        {
            get { return _typeList[index]; }
            set
            {
                CheckType(value);
                _typeList[index] = value;
            }
        }

        private readonly List<Type> _typeList;

        /// <summary>
        /// Creates a new <see cref="TypeList{T}"/> object.
        /// </summary>
        public TypeList()
        {
            _typeList = new List<Type>();
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Add<T>() where T : TBaseType
        {
            _typeList.Add(typeof(T));
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="item"></param>
        public void Add(Type item)
        {
            CheckType(item);
            _typeList.Add(item);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, Type item)
        {
            _typeList.Insert(index, item);
        }

        /// <summary>
        /// IndexOf
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(Type item)
        {
            return _typeList.IndexOf(item);
        }

        /// <summary>
        /// Contains
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Contains<T>() where T : TBaseType
        {
            return Contains(typeof(T));
        }

        /// <summary>
        /// Contains
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Type item)
        {
            return _typeList.Contains(item);
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>() where T : TBaseType
        {
            _typeList.Remove(typeof(T));
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Type item)
        {
            return _typeList.Remove(item);
        }

        /// <summary>
        /// RemoveAt
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _typeList.RemoveAt(index);
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _typeList.Clear();
        }

        /// <summary>
        /// CopyTo
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(Type[] array, int arrayIndex)
        {
            _typeList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Type> GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).GetTypeInfo().IsAssignableFrom(item))
            {
                throw new ArgumentException("Given item is not type of " + typeof(TBaseType).AssemblyQualifiedName, "item");
            }
        }
    }
}