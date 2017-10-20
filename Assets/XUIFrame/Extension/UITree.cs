/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  UI Tree
 *    Description: 
 *        UI panel tree structure.
 *                  
 *    Date: 2017/10/20
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace XUIF
{
    public class UITree<T>
    {
        private Stack<Node<T>> treeTrunk;
        
        private Node<T> root;                           //头引用

        //头引用属性
        public Node<T> Root
        {
            get { return root; }
            set { root = value; }
        }

        #region 构造器

        public UITree()
        {
            root = null;
            treeTrunk = new Stack<Node<T>>();
        }
        
        public UITree(T val)
        {
            Node<T> p = new Node<T>(val);
            root = p;
            treeTrunk = new Stack<Node<T>>();
        }

        public UITree(Node<T> parent)
        {
            Node<T> p = new Node<T>(parent);
            root = p;
            treeTrunk = new Stack<Node<T>>();
        }

        public UITree(T val,Node<T> parent)
        {
            Node<T> p = new Node<T>(val, parent);
            root = p;
            treeTrunk = new Stack<Node<T>>();
        }

        #endregion 

        /// <summary>
        /// 判断是否空树
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return root == null;
        }
        

        /// <summary>
        /// 获取叶子节点
        /// </summary>
        /// <param name="nodeName">子节点名</param>
        /// <returns></returns>
        public Node<T> GetChild(string nodeName)
        {
            return null;
        }

        /// <summary>
        /// 新节点入栈
        /// </summary>
        /// <param name="node"></param>
        public void PushLeaf(Node<T> node)
        {
            treeTrunk.Push(node);
        }

        /// <summary>
        /// 叶子节点出栈
        /// </summary>
        /// <returns></returns>
        public Node<T> PopLeaf()
        {
            return treeTrunk.PopItem();
        }

        /// <summary>
        /// 获取叶子节点，不出栈
        /// </summary>
        /// <returns></returns>
        public Node<T> PeekLeaf()
        {
            return treeTrunk.PeekItem();
        }

    }

    public class Node<T>
    {
        private T data;                                   //数据域
        private Node<T> parent;                           //父节点
        private Dictionary<string, Node<T>> children;     //孩子集

        #region 构造器

        public Node(T val, Node<T> p)
        {
            data = val;
            parent = p;
            children = new Dictionary<string, Node<T>>();
        }

        public Node(T val)
        {
            data = val;
            parent = null;
            children = new Dictionary<string, Node<T>>();
        }

        public Node(Node<T> p)
        {
            data = default(T);
            parent = p;
            children = new Dictionary<string, Node<T>>();
        }

        #endregion

        //数据属性
        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        //父节点属性
        public Node<T> Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// 查找子节点
        /// </summary>
        /// <param name="node">子节点名</param>
        /// <returns></returns>
        public Node<T> GetChild(string node)
        {
            return children.GetValue(node);
        }

        /// <summary>
        /// 删除子节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool RemoveChild(string node)
        {
            return children.Remove(node);
        }

        /// <summary>
        /// 清空子节点
        /// </summary>
        public void ClearChildren()
        {
            children.Clear();
        }
    }
}
