using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTrees
{
    public interface ITree<T>
    {
        public void Add(T key);
        public void Remove(T key);
        public T Max();
        public T Min();
        public bool Contains(T key);

        public IEnumerable<T> PreOrder();

        public IEnumerable<T> PostOrder();

        public IEnumerable<T> InOrder();

        public IEnumerable<T> LevelOrder();

        public int Height();

        public int Count { get; set; }

        //private Node

    }
}
