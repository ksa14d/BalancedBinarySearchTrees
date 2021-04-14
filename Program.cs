using System;
using System.Collections.Generic;

namespace BinarySearchTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            var ints = new List<int>() { 10, 7, 5, 8, 15, 11, 18, 3, 1, 9, 2, 20, 26, 22, 16 };
            AVLBalancedTree<int> bst = new AVLBalancedTree<int>(ints);
        }
    }
}
