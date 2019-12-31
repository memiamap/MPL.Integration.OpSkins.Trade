using System;
using System.Collections;
using System.Collections.Generic;

namespace MPL.Integration.OpSkins.Trade
{
    /// <summary>
    /// A class that defines a thread-safe synchronized generic list.
    /// </summary>
    /// <typeparam name="T">A T that is the type contained by the list.</typeparam>
    internal class SynchronizedList<T> : IList<T>
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        internal SynchronizedList()
        {
            _SynchronizedList = ArrayList.Synchronized(new List<T>());
        }

        #endregion

        #region Declarations
        #region _Members_
        private IList _SynchronizedList;
        #endregion
        #endregion

        #region Methods
        #region _Internal_

        #endregion
        #endregion

        #region Interfaces
        #region _IList<T>_
        #region __Methods__
        void ICollection<T>.Add(T item)
        {
            _SynchronizedList.Add(item);
        }

        void ICollection<T>.Clear()
        {
            _SynchronizedList.Clear();
        }

        bool ICollection<T>.Contains(T item)
        {
            return _SynchronizedList.Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            _SynchronizedList.CopyTo(array, arrayIndex);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new SynchronizedListEnumerator<T>(_SynchronizedList.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _SynchronizedList.GetEnumerator();
        }

        int IList<T>.IndexOf(T item)
        {
            return _SynchronizedList.IndexOf(item);
        }

        void IList<T>.Insert(int index, T item)
        {
            _SynchronizedList.Insert(index, item);
        }

        bool ICollection<T>.Remove(T item)
        {
            bool ReturnValue = false;

            if (_SynchronizedList.Contains(item))
            {
                _SynchronizedList.Remove(item);
                ReturnValue = true;
            }

            return ReturnValue;
        }

        void IList<T>.RemoveAt(int index)
        {
            _SynchronizedList.RemoveAt(index);
        }

        #endregion
        #region __Properties__
        T IList<T>.this[int index]
        {
            get { return (T)_SynchronizedList[index]; }
            set { _SynchronizedList[index] = value; }
        }

        int ICollection<T>.Count
        {
            get { return _SynchronizedList.Count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return _SynchronizedList.IsReadOnly; }
        }

        #endregion
        #endregion
        #endregion

        #region Classes
        #region _SynchronizedListEnumerator<Y>_
        private class SynchronizedListEnumerator<Y> : IEnumerator<Y>
        {
            #region Constructors
            internal SynchronizedListEnumerator(IEnumerator enumerator)
            {
                _Enumerator = enumerator;
            }

            #endregion

            #region Declarations
            #region _Members_
            private IEnumerator _Enumerator;

            #endregion
            #endregion

            #region Interfaces
            #region _IEnumerator<Y>_
            #region __Methods__
            void IDisposable.Dispose()
            { }

            bool IEnumerator.MoveNext()
            {
                return _Enumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                _Enumerator.Reset();
            }

            #endregion
            #region __Properties__
            Y IEnumerator<Y>.Current
            {
                get { return (Y)_Enumerator.Current; }
            }

            object IEnumerator.Current
            {
                get { return (Y)_Enumerator.Current; }
            }

            #endregion
            #endregion
            #endregion
        }

        #endregion
        #endregion
    }
}