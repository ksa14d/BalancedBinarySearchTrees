using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTrees
{
    public class Node<T> :  ITreeNode<T, Node<T>> where T : IComparable<T>  
    {
        public  T Key { get; set; }
        public  Node<T> Left { get; set; }
        public  Node<T> Right { get; set; }        
        public Node(T Key)
        {
            this.Key = Key;
            Left = null;
            Right = null;
        }

        public static bool operator <(Node<T> a, Node<T> b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            return (a.Key.CompareTo(b.Key) < 0);
        }
        public static bool operator >(Node<T> a, Node<T> b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            return (a.Key.CompareTo(b.Key) > 0);
        }
        public static bool operator ==(Node<T> a, Node<T> b)
        {
            if (ReferenceEquals(a, b)) return true;
            else if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return (a.Key.CompareTo(b.Key) == 0);
        }
        public static bool operator !=(Node<T> a, Node<T> b)
        {
            if (!(a == b)) return true;
            return false;
        }

        public virtual Node<T> Balance()
        {
            return this; // not much to balance in a regular tree
        }
        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (ReferenceEquals(this, obj))
            {
                isEqual = true;
            }

            if (ReferenceEquals(obj, null))
            {
                isEqual = false;
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }
    }
    
}
