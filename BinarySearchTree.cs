using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTrees
{
    public static class StaticUtilities
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
         {
        typeof(int),  typeof(double),  typeof(decimal),
        typeof(long), typeof(short),   typeof(sbyte),
        typeof(byte), typeof(ulong),   typeof(ushort),
        typeof(uint), typeof(float)
        };
        public static bool IsNumeric(this Type myType)
        {
            return NumericTypes.Contains(Nullable.GetUnderlyingType(myType) ?? myType);
        }
    }
    public class BinarySearchTree<T> : ITree<T> where T : IComparable<T> 
    {        
        public int Count { get; set; }
        protected Node<T> Root { get; set; }

        private readonly IComparer<T> NodeComparer;
                
        public BinarySearchTree()
        {
            Root = null;
        }

        public BinarySearchTree(List<T> values) : this()
        {
            values.ForEach(v => this.Add(v)); // construct a tree from values
        }

        public BinarySearchTree(IComparer<T> NodeComparer)
        {
            this.NodeComparer = NodeComparer;
        }

        public IEnumerable<T> LevelOrder()
        {            
            var queue = new Queue<Node<T>>();
            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                Node<T> node = queue.Dequeue();
                yield return node.Key;
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }            
        }

        protected IEnumerable<T> PreOrder(Node<T> node) // N-LR
        {            
            yield return node.Key;

            if (node.Left != null)
                foreach (var key in PreOrder(node.Left))
                    yield return key;

            if (node.Right != null)
                foreach (var key in PreOrder(node.Right))
                    yield return key;
        }

      

        protected IEnumerable<T> InOrder(Node<T> node) // L N R
        {           
            if (node.Left != null)
                foreach (var key in InOrder(node.Left))
                    yield return key;

            yield return node.Key;

            if (node.Right != null)
                foreach (var key in InOrder(node.Right))
                    yield return key;
        }
       

        protected IEnumerable<T> PostOrder(Node<T> node) // N-LR
        {
            if (node.Left != null)
                foreach (var key in PostOrder(node.Left))
                    yield return key;

            if (node.Right != null)
                foreach (var key in PostOrder(node.Right))
                    yield return key;

            yield return node.Key;
        }
        private Node<T> Insert(Node<T> node, Node<T> key)
        {
            if (node == null)
            {
                node = key;
                return node;
            }

            if (key < node) node.Left = Insert(node.Left, key);
            else if (key > node) node.Right = Insert(node.Right, key);
            else return node;

            node = node.Balance();

            return node;
        }

        protected virtual void Add(Node<T> node, Node<T> key) => Root = Insert(node, key);
       

        protected virtual Node<T> Delete(Node<T> node, Node<T> key) //O(Log(n))
        {
            if (node == null)
                return node;

            if (key < node) node.Left = Delete(node.Left, key);
            else if (key > node) node.Right = Delete(node.Right, key);
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    Node<T> temp = (node.Left == null) ? node.Right : node.Left;
                    if (temp == null) // no child
                    {
                        node = null;
                    }
                    else // one child
                    {
                        node.Key = temp.Key;
                        if (node.Left == temp) node.Left = null;
                        else node.Right = null;
                    }
                }
                else  // internal node
                {
                    Node<T> inOrderSuccessor = Min(node.Right);
                    node.Key = inOrderSuccessor.Key;
                    node.Right = Delete(node.Right, inOrderSuccessor);
                }
            }

            if (node == null)
                return node;

            node = node.Balance();

            return node;

        }

        protected virtual void Remove(Node<T> node, Node<T> key) => Root = Delete(node, key);             

        public bool IsValid(Node<T> node, T low, T high) //O(n)
        {
            if (node == null)
                return true;

            T val = node.Key;

            Type nodeType = typeof(T);

            if (nodeType.IsNumeric())
            {
                if (val.CompareTo(low) <= 0)
                    return false;
                if (val.CompareTo(high) >= 0)
                    return false;
            }
            else
            {
                if (low != null && val.CompareTo(low) <= 0)
                    return false;
                if (high != null && val.CompareTo(high) >= 0)
                    return false;
            }

            if (!IsValid(node.Right, val, high))
                return false;
            if (!IsValid(node.Left, low, val))
                return false;

            return true;
        }  

        protected int Height(Node<T> node) //O(n)
        {
            if (node == null)return 0;
            return Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        protected virtual Node<T> Min(Node<T> node) //O(Log(n))
        {
            if (node == null) return node;
            var current = node;
            while (current.Left != null) current = current.Left;
            return current;
        }

        protected virtual Node<T> Max(Node<T> node) //O(Log(n))
        {
            if (node == null) return node;
            var current = node;
            while (current.Right != null) current = current.Right;
            return current;
        }

        public virtual  T Max()
        {
            var max = Max(Root);
            return (max == null) ? default(T) : max.Key;
        }
        public virtual  T Min()
        {
            var min = Min(Root);
            return (min == null) ? default(T) : min.Key;
        }
        public virtual void Add(T key)
        {
            Root = Insert(Root, new Node<T>(key));
        }
        public virtual void Remove(T key)
        {
            Root = Delete(Root, new Node<T>(key));
        }
        public virtual bool Contains(T Key)  //O(Log(n))  this class is not balanced tree might get skewed
        {
            if (Root == null) return false;
            var nodeToBeFound = Root;
            var item = new Node<T>(Key);
            while (item != null && item != nodeToBeFound)
            {
                if (item < nodeToBeFound) nodeToBeFound = nodeToBeFound.Left;
                else nodeToBeFound = nodeToBeFound.Right;
            }

            if (nodeToBeFound == null) return false;
            return true;
        }

        public IEnumerable<T> PreOrder()
        {                      
            return PreOrder(Root).ToList();           
        } // O(n) but the performance is improved by yield return 1 at a time

        public IEnumerable<T> PostOrder()
        {
            return PostOrder(Root).ToList();
        } // O(n)

        public IEnumerable<T> InOrder()
        {
            return InOrder(Root).ToList();
        } // O(n)


        public int Height()
        {
            return Height(Root);
        } // O(n)

  
        private void Mirror(Node<T> root)
        {
            if (root == null) return;
            Mirror(root.Left);
            Mirror(root.Right);
            var temp = root.Left;
            root.Left = root.Right;
            root.Right = temp;
        }
        private static bool IsSymmetricTree(Node<T> a, Node<T> b) //O(n)
        {            
            if (a == null && b == null) return true;                    
            if (a == null || b == null) return false;                        
            return a == b && IsSymmetricTree(a.Left, b.Right) && IsSymmetricTree(a.Right, b.Left);
        }

    }
}
