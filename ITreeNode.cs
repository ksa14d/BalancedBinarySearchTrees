using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTrees
{
    public interface ITreeNode<T,U> where U : ITreeNode<T, U>
    {
        public T Key { get; set; }
        public U Left { get; set; }
        public U Right { get; set; }
        public U Balance();        

    }

   

}
