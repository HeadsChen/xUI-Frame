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
    /// <summary>
    /// 类栈多叉树
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class UITree<T>
	{
		//根引用
		private Node<T> root;

		//根引用属性
		public Node<T> Root {
			get { return root; }
		}

		//末梢引用
		private Node<T> end;

		//末梢引用属性
		public Node<T> End {
			get{ return end; }
		}


        private List<Node<T>> nodeList;

        private int lastIndex;

		#region 构造器

		/// <summary>
		/// 创建新节点，并作为新树根节点及叶节点
		/// </summary>
		/// <param name="val">Value.</param>
		public UITree (string id, T val)
		{
			Node<T> p = new Node<T> (id, val);
			root = p;
			end = p;

            nodeList = new List<Node<T>>();
            nodeList.Add(p);
            lastIndex = 0;
		}
        
		#endregion

		/// <summary>
		/// 判断是否只有根节点
		/// </summary>
		/// <returns></returns>
		public bool NotOnlyRoot ()
		{
            //return root.Id != end.Id;
            return lastIndex != 0;
		}

		#region 树结构逻辑

		/// <summary>
		/// Pushs the end.
		/// </summary>
		/// <param name="val">Value.</param>/
		public void PushEnd (string id,T val)
		{
			Node<T> node = new Node<T> (id, val, end);
			end = node;

            nodeList.Add(node);
            lastIndex++;
		}
        
		/// <summary>
		/// Pops the end.
		/// </summary>
		/// <returns>The end.</returns>//
		public T[] PopEnd ()
		{
			if (end.HaveChildren ()) {
				T[] tArr = GetNodeArr ();
				end.ClearChildren ();
				end = end.Parent;

                nodeList.RemoveAt(lastIndex--);
				return tArr;
			}

			T t = end.Data;
			end = end.Parent;

            nodeList.RemoveAt(lastIndex--);
            return new T[1] { t };
		}


        //public T[] PullTo(string id)
        //{
        //    int index = FindWithId(id);
        //    if (index != -1)
        //    {
        //        T[] arr = new T[] { };
        //        for (int i = nodeList.Count; i > index+1; i--)
        //        {
        //            arr+=PopEnd()
        //        }
        //    }
        //}

		/// <summary>
		/// Peeks the end.
		/// </summary>
		/// <returns>The end.</returns>/
		public T[] PeekEnd ()
		{
            if (end.HaveChildren())
            {
				return GetNodeArr ();
            }
            return new T[1] { end.Data };
		}
        
		/// <summary>
		/// Adds the leaf.
		/// </summary>
		/// <param name="id">Id.</param>
		/// <param name="leaf">Leaf.</param>
		public void AddLeaf (string id, T leaf)
		{
			if (end.ContainsChild (id))
				return;
			end.AddChild (id, leaf);
		}

		/// <summary>
		/// Removes the leaf.
		/// </summary>
		/// <returns>The leaf.</returns>
		/// <param name="leaf">Leaf.</param>
		public T RemoveLeaf(string id){
			return end.RemoveChild (id);
		}

        #endregion

        /// <summary>
        /// 查找指定id的节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private int FindWithId(string id)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }


		private T[] GetNodeArr(){
			int count = end.Children.Count;
			T[] tArr = new T[count + 1];
			for (int i = 0; i < count; i++) {
				tArr [i] = end.Children [i].Data;
			}
			tArr [count] = end.Data;
			return tArr;
		}


        #if UNITY_EDITOR

        #region 测试用！测试用！测试用！

        /// <summary>
        /// 遍历树结构
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string Traverse(Node<T> node)
        {
			string idStr = node.Id + GetChildrenId (node);
            if (node.Parent != null)
            {
                idStr = idStr + "-" + Traverse(node.Parent);
            }
            
            return idStr;
        }

		private string GetChildrenId(Node<T> node){
			if (node.HaveChildren ()) {
				string str = "(";
				for (int i = 0; i < node.Children.Count; i++) {
					str = str + node.Children [i].Id + "-";
				}
				int lastIndex = -1;
				lastIndex = str.LastIndexOf ('-');
				if (lastIndex != -1) {
                    str = str.Remove(lastIndex);
				}
				str += ")";
				return str;
			}
			return "";
		}
        #endregion

        #endif
    }

	public class Node<T>
	{
		//节点ID
		private string id;
		//数据域
		private T data;
		//父节点
		private Node<T> parent;
		//孩子集
		private List<Node<T>> children;
        
		#region 构造器

		public Node (string id, T val, Node<T> p)
		{
			this.id = id;
			data = val;
			parent = p;
		}

		public Node (string id, T val)
		{
			this.id = id;
			data = val;
			parent = null;
		}

		public Node (Node<T> p)
		{
			data = default(T);
			parent = p;
		}

		#endregion

		#region 属性

		//数据ID
		public string Id
        {
            get { return id; }
        }

		//数据属性
		public T Data
        {
            get { return data; }
        }

		//父节点属性
		public Node<T> Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public List<Node<T>> Children
        {
            get { return children; }
        }

		#endregion

		/// <summary>
		/// Adds the child.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="val">Value.</param>
		public void AddChild (string id, T val)
		{
			if (children == null) {
				children = new List<Node<T>> ();
			}
			Node<T> node = new Node<T> (id, val, this);
			children.Add (node);
		}

		/// <summary>
		/// 查找叶节点
		/// </summary>
		/// <param name="node">叶节点名</param>
		/// <returns></returns>
		public bool TryGetChild (string id,out Node<T> child)
		{
			if (HaveChildren()) {
				for (int i = 0; i < children.Count; i++) {
					if (children [i].Id == id) {
						child = children [i];
						return true;
					}
				}
			}
            child = null;
			return false;
		}

		public bool ContainsChild(string id){
			if (HaveChildren ()) {
				for (int i = 0; i < children.Count; i++) {
					if (children [i].Id == id) {
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 删除叶节点
		/// </summary>
		/// <param name="node">叶节点名</param>
		/// <returns></returns>
		public T RemoveChild (string id)
		{
			if (children != null && children.Count != 0) {
				for (int i = 0; i < children.Count; i++) {
					if (children [i].Id == id) {
						T t = children [i].Data;
						children.RemoveAt (i);
						return t;
					}
				}
			}
			return default(T);
		}

		/// <summary>
		/// 清空子节点
		/// </summary>
		public void ClearChildren ()
		{
			if (children != null) {
				children.Clear ();
			}
		}

		public bool HaveChildren(){
			return children != null && children.Count != 0;
		}
	}
}
