using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTrees
{
    public class AVLNode<T> : Node<T> where T : IComparable<T>
    {
        public int Height { get; set; }

        public new AVLNode<T> Left 
        { 
            get 
            {
                return base.Left as AVLNode<T>;
            }
            set
            {
                base.Left = value;
            }
        }
        public new AVLNode<T> Right
        {
            get
            {
                return base.Right as AVLNode<T>;
            }
            set
            {
                base.Right = value;
            }
        }
        public AVLNode(T Key) : base(Key)
        {
            this.Height = 1;
        }        
      
        public int BalanceFactor
        {
            get { return GetHeight(Left) - GetHeight(Right); }
        }

        private int GetHeight(AVLNode<T> node) => (node == null) ? 0 : node.Height;

        private void CalculateHeight() => this.Height = Math.Max(GetHeight(Left), GetHeight(Right)) + 1;

        private AVLNode<T> RotateRight()
        {
            AVLNode<T> x = this.Left;
            AVLNode<T> T2 = x.Right;

            // Perform rotation
            x.Right = this;
            this.Left = T2;

            // Update heights
            this.CalculateHeight();
            x.CalculateHeight();

            // Return new root
            return x;
        }

        private AVLNode<T> RotateLeft()
        {
            AVLNode<T> y = this.Right;
            AVLNode<T> T2 = y.Left;

            // Perform rotation
            y.Left = this;
            this.Right = T2;

            // Update heights
            this.CalculateHeight();
            y.CalculateHeight();

            // Return new root
            return y;
        }

        public override AVLNode<T> Balance()
        {
            AVLNode<T> balancedNode;

            this.CalculateHeight();

            if (this.BalanceFactor > 1 && this.Left.BalanceFactor >= 0) // LL
                balancedNode = this.RotateRight(); 

            else if (this.BalanceFactor > 1 && this.Left.BalanceFactor < 0) //LR
            {
                this.Left = this.Left.RotateLeft();
                balancedNode = this.RotateRight();
            }

            else if (this.BalanceFactor < -1 && this.Right.BalanceFactor <= 0) // RR
                balancedNode = this.RotateLeft();

            else if (this.BalanceFactor < -1 && this.Right.BalanceFactor > 0) //RL
            {
                this.Right = this.Right.RotateRight();
                balancedNode = this.RotateLeft();
            }
            else balancedNode = this; // already balanced

            return balancedNode;
        }
    }

}
