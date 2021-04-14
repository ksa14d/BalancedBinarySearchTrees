using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTrees
{
    public class AVLBalancedTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        public new AVLNode<T> Root
        {
            get
            {
                return base.Root as AVLNode<T>;
            }

            set
            {
                base.Root = value;
            }
        }       
        public AVLBalancedTree() : base()
        {            
        }
        public AVLBalancedTree(List<T> values) : base(values)
        {
        }
        public AVLBalancedTree(IComparer<T> comparer) : base(comparer)
        {
        }
        public override void Add(T key)
        {
            Add(Root, new AVLNode<T>(key));
        }
        public override void Remove(T key)
        {
            Remove(Root, new AVLNode<T>(key));
        }
               
        private void Print(AVLNode<T> node , string indent, bool last)
        {
            sb.Append(indent);
            if (last)
            {
                sb.Append("\\---");
                indent += "   ";
            }
            else
            {
                sb.Append("|---");
                indent += "|   ";
            }
            sb.Append(node.Key +"\n");
           
            if (node.Right != null)
                Print(node.Right, indent, (node.Right.Left == null && node.Right.Right == null));
            if (node.Left != null)
                Print(node.Left, indent, (node.Left.Left == null && node.Left.Right == null));
        }
        private StringBuilder sb = new StringBuilder();
        public string PrintSemanticTree()
        {
            sb.Clear();
            Print(Root,"   ", true);
            return sb.ToString();
        }
    }
}
