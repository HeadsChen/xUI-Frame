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
		//根引用属性
		public Node<T> Root {
			get { return nodeList [0]; }
		}

		//末梢引用属性
		public Node<T> End {
			get{ return nodeList[lastIndex]; }
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
			Node<T> node = new Node<T> (id, val, nodeList[lastIndex]);

            nodeList.Add(node);
            lastIndex++;
		}
        
		/// <summary>
		/// Pops the end.
		/// </summary>
		/// <returns>The end.</returns>//
		public T[] PopEnd ()
		{
			if (nodeList[lastIndex].HaveChildren ()) {
				T[] tArr = EndNodeDataArr ();

				nodeList [lastIndex].ClearChildren ();
                nodeList.RemoveAt(lastIndex--);
				return tArr;
			}

			T t = nodeList [lastIndex].Data;
            nodeList.RemoveAt(lastIndex--);
            return new T[1] { t };
		}


        public T[] Pull(string id)
        {
            int index = FindWithId(id);
            if (index != -1)
            {
				List<Node<T>> list = new List<Node<T>> ();
				for (int i = lastIndex; i > index+1; i--)
                {
					nodeList [i].GetChildrenByList (ref list);
					list.Add (nodeList [i]);
					nodeList [i].ClearChildren ();
				}
				nodeList.RemoveRange (index + 1, lastIndex - index);

				lastIndex = index;
				return GetListDataArr (list);
            }
			return null;
        }

		/// <summary>
		/// Peeks the end.
		/// </summary>
		/// <returns>The end.</returns>/
		public T[] PeekEnd ()
		{
			if (nodeList[lastIndex].HaveChildren())
            {
				return EndNodeDataArr ();
            }
			return new T[1] { nodeList [lastIndex].Data };
		}
        
		/// <summary>
		/// Adds the leaf.
		/// </summary>
		/// <param name="id">Id.</param>
		/// <param name="leaf">Leaf.</param>
		public void AddLeaf (string id, T leaf)
		{
			if (nodeList[lastIndex].ContainsChild (id))
				return;
			nodeList[lastIndex].AddChild (id, leaf);
		}

		/// <summary>
		/// Removes the leaf.
		/// </summary>
		/// <returns>The leaf.</returns>
		/// <param name="leaf">Leaf.</param>
		public T RemoveLeaf(string id){
			return nodeList [lastIndex].RemoveChild (id).Data;
		}

        #endregion

		#region 私有方法

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

		/// <summary>
		/// Ends the node data arr.
		/// </summary>
		/// <returns>The node data arr.</returns>
		private T[] EndNodeDataArr(){
			List<Node<T>> list = new List<Node<T>> ();
			nodeList [lastIndex].GetChildrenByList (ref list);
			list.Add (nodeList [lastIndex]);
			return GetListDataArr (list);
		}

		/// <summary>
		/// Gets the node data arr.
		/// </summary>
		/// <returns>The node data arr.</returns>
		/// <param name="list">List.</param>
		private T[] GetListDataArr(List<Node<T>> list){

			T[] tArr = new T[list.Count];
			for (int i = 0; i < list.Count; i++) {
				tArr [i] = list [i].Data;
			}
			return tArr;
		}

		#endregion

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

				List<Node<T>> list = new List<Node<T>> ();
				node.GetChildrenByList (ref list);
				for (int i = 0; i < list.Count; i++) {
					str = str + list [i].Id + "-";
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
		private Dictionary<string,Node<T>> children; 
        
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

		public Node (string id, Node<T> p)
		{
			this.id = id;
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

		#endregion

		/// <summary>
		/// Adds the child.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="val">Value.</param>
		public void AddChild (string id, T val)
		{
			if (children == null) {
				children = new Dictionary<string, Node<T>> ();
			}
			Node<T> node = new Node<T> (id, val, this);
			children.Add (id, node);
		}

		/// <summary>
		/// 查找叶节点
		/// </summary>
		/// <param name="node">叶节点名</param>
		/// <returns></returns>
		public bool TryGetChild (string id,out Node<T> child)
		{
			return children.TryGetValue (id, out child);
		}

		/// <summary>
		/// Gets the children by list.
		/// </summary>
		/// <param name="list">List.</param>
		public void GetChildrenByList(ref List<Node<T>> list){
			if (HaveChildren ()) {
				foreach (Node<T> child in children.Values) {
					list.Add (child);
				}
			}
		}

		/// <summary>
		/// Gets the children count.
		/// </summary>
		/// <returns>The children count.</returns>
		public int GetChildrenCount(){
			return children.GetCount ();
		}

		/// <summary>
		/// Containses the child.
		/// </summary>
		/// <returns><c>true</c>, if child was containsed, <c>false</c> otherwise.</returns>
		/// <param name="id">Identifier.</param>
		public bool ContainsChild(string id){
			return children.includeKey (id);
		}

		/// <summary>
		/// 删除叶节点
		/// </summary>
		/// <param name="node">叶节点名</param>
		/// <returns></returns>
		public Node<T> RemoveChild (string id)
		{
			if(children.ContainsKey(id)){
				Node<T> node = children.GetValue (id);
				children.Remove (id);
				return node;
			}
			return null;
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

		/// <summary>
		/// Haves the children.
		/// </summary>
		/// <returns><c>true</c>, if children was had, <c>false</c> otherwise.</returns>
		public bool HaveChildren(){
			return children != null && children.Count != 0;
		}
	}
}
