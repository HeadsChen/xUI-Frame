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
		/// 判断是否空树
		/// </summary>
		/// <returns></returns>
		public bool IsEmpty ()
		{
			return root == null;
		}

		#region 树结构逻辑

		/// <summary>
		/// Pushs the end.
		/// </summary>
		/// <param name="val">Value.</param>/
		public void PushEnd (string key,T val)
		{
			Node<T> node = new Node<T> (key, val, end);
			End = node;
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
			return new T[end.Data];
		}

		/// <summary>
		/// Peeks the end.
		/// </summary>
		/// <returns>The end.</returns>/
		public T PeekEnd ()
		{
			return end.Data;
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
		public string Id {
			get{ return id; }
		}

		//数据属性
		public T Data {
			get { return data; }
			set { data = value; }
		}

		//父节点属性
		public Node<T> Parent {
			get { return parent; }
			set { parent = value; }
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
			return null;
		}

		/// <summary>
		/// 删除叶节点
		/// </summary>
		/// <param name="node">叶节点名</param>
		/// <returns></returns>
		public T RemoveChild (string leaf)
		{
			T t = GetChild (leaf);
			if (t != null) {
				children.Remove (t);
			}
			return t;
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
			return children != null || children.Count != 0;
		}
	}
}
