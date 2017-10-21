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
		//根引用
		private Node<T> root;

		//根引用属性
		public Node<T> Root {
			get { return root; }
		}

		//叶引用
		private Node<T> end;

		//叶引用属性
		public Node<T> End {
			get{ return end; }
		}

		#region 构造器

		/// <summary>
		/// 创建新节点，并作为新树根节点及叶节点
		/// </summary>
		/// <param name="val">Value.</param>
		public UITree (string key, T val)
		{
			Node<T> p = new Node<T> (key, val);
			root = p;
			end = p;
		}
        
		#endregion

		/// <summary>
		/// 判断是否只有根节点
		/// </summary>
		/// <returns></returns>
		public bool NotOnlyRoot ()
		{
            return root.Id != end.Id;
		}

		#region 树结构逻辑

		/// <summary>
		/// Pushs the end.
		/// </summary>
		/// <param name="val">Value.</param>/
		public void PushEnd (string key,T val)
		{
			Node<T> node = new Node<T> (key, val, end);
			end = node;
		}


        public void PushEnd(string key,T val,Node<T> leafNode)
        {
            Node<T> node = new Node<T>(key, val, leafNode);
            end = node;
        }

		/// <summary>
		/// Pops the end.
		/// </summary>
		/// <returns>The end.</returns>//
		public T[] PopEnd ()
		{
			if (end.HaveChildren ()) {
				List<Node<T>> list = end.ClearChildren ();
				list.Add (end);
				end = end.Parent;

				T[] tArr = new T[list.Count];
				for (int i = 0; i < list.Count; i++) {
					tArr [i] = list [i].Data;
				}
				return tArr;
			}

            T t = end.Data;
            end = end.Parent;

            return new T[1] { t };
		}

		/// <summary>
		/// Peeks the end.
		/// </summary>
		/// <returns>The end.</returns>/
		public T[] PeekEnd ()
		{
            if (end.HaveChildren())
            {
                List<Node<T>> list = end.Children;
                list.Add(end);

                T[] tArr = new T[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    tArr[i] = list[i].Data;
                }
                return tArr;
            }
            return new T[1] { end.Data };
		}

        /// <summary>
        /// 移除前末梢
        /// </summary>
        public void RemoveExEnd()
        {
            Node<T> parent = end.Parent.Parent;
            end.Parent.Parent = null;
            end.Parent = parent;
        }

		/// <summary>
		/// Adds the leaf.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="leaf">Leaf.</param>
		public void AddLeaf (string key, T leaf)
		{
			end.AddChild (key, leaf);
		}

		/// <summary>
		/// Removes the leaf.
		/// </summary>
		/// <returns>The leaf.</returns>
		/// <param name="leaf">Leaf.</param>
		public T RemoveLeaf(string leaf){
		 	return end.RemoveChild (leaf);
		}
        
		#endregion

        /// <summary>
        /// 遍历树结构，测试用！测试用！测试用！
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string Traverse(Node<T> node)
        {
            string idStr = node.Id;
            if (node.Parent != null)
            {
                idStr = idStr + "-" + Traverse(node.Parent);
            }
            
            return idStr;
        }

        private string GetId(Node<T> node)
        {
            return node.Id;
        }

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
//		private Dictionary<string, Node<T>> children;
		private List<Node<T>> children;


		#region 构造器

		public Node (string key, T val, Node<T> p)
		{
			id = key;
			data = val;
			parent = p;
		}

		public Node (string key, T val)
		{
			id = key;
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
		public T GetChild (string leaf)
		{
			if (children != null && children.Count != 0) {
				for (int i = 0; i < children.Count; i++) {
					if (children [i].Id == leaf) {
						return children [i].Data;
					}
				}
			}
			return default(T);
		}

		/// <summary>
		/// 删除叶节点
		/// </summary>
		/// <param name="node">叶节点名</param>
		/// <returns></returns>
		public T RemoveChild (string leaf)
		{
			if (children != null && children.Count != 0) {
				for (int i = 0; i < children.Count; i++) {
					if (children [i].Id == leaf) {
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
		public List<Node<T>> ClearChildren ()
		{
			List<Node<T>> list = children;
			if (children != null) {
				children.Clear ();
			}
			return list;
		}

		public bool HaveChildren(){
			return children != null && children.Count != 0;
		}
	}
}
