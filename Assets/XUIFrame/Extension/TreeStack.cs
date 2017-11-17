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
    /// 树形栈封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class TreeStack<T>
    {
        #region 节点
        //根引用
        private Node<T> root;

        //根引用属性
        public Node<T> Root
        {
            get { return root; }
        }

        //末梢引用
        private Node<T> end;

        //末梢引用属性
        public Node<T> End
        {
            get { return end; }
        }
        #endregion

        #region 构造器

        /// <summary>
        /// 创建新节点，并作为新树根节点及末梢节点
        /// </summary>
        /// <param name="val">Value.</param>
        public TreeStack(string id, T val)
        {
            Node<T> p = new Node<T>(id, val);
            root = p;
            end = p;
        }

        #endregion

        /// <summary>
        /// 判断是否只有根节点
        /// </summary>
        /// <returns></returns>
        public bool NotOnlyRoot
        {
            //return root.Id != end.Id;
            get { return root.Id != end.Id; }
        }

        #region 结构逻辑 对外接口

        /// <summary>
        /// 添加末梢
        /// </summary>
        /// <param name="val">Value.</param>/
        public void Push(string id, T val)
        {
            Node<T> node = new Node<T>(id, val, end);
            end = node;
        }

        /// <summary>
        /// 取出多个 直到指定节点
        /// </summary>
        /// <param name="id">指定节点名</param>
        /// <returns></returns>
        public T[] Pull(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PopEnd();
            }
            if (ContainsNode(id, end))
            {
                List<Node<T>> list = new List<Node<T>>();
                do
                {
                    if (end.HaveChildren())
                    {
                        end.GetChildrenByList(ref list);
                        end.ClearChildren();
                    }
                    list.Add(end);
                    end = end.Parent;
                } while (end.Id != id);
                return GetListDataArr(list);
            }
            return null;
        }

        /// <summary>
        /// 获得末梢 不取出
        /// </summary>
        /// <returns>The end.</returns>/
        public T[] Peek()
        {
            if (end.HaveChildren())
            {
                List<Node<T>> list = new List<Node<T>>();
                end.GetChildrenByList(ref list);
                list.Add(end);
                return GetListDataArr(list);
            }

            T t = end.Data;
            return new T[1] { t };
        }

        /// <summary>
        /// 添加叶节点
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="leaf">Leaf.</param>
        public void AddLeaf(string id, T leaf)
        {
            if (end.ContainsChild(id))
                return;
            end.AddChild(id, leaf);
        }

        /// <summary>
        /// 移除叶节点
        /// </summary>
        /// <returns>The leaf.</returns>
        /// <param name="leaf">Leaf.</param>
        public T RemoveLeaf(string id)
        {
            return end.RemoveChild(id).Data;
        }


		public void Clear(){
			root = null;
		}

        #endregion

        #region 私有方法

        /// <summary>
        /// 取出末梢
        /// </summary>
        /// <returns>The end.</returns>//
        private T[] PopEnd()
        {
            if (end.HaveChildren())
            {
                List<Node<T>> list = new List<Node<T>>();

                end.GetChildrenByList(ref list);
                end.ClearChildren();
                list.Add(end);
                end = end.Parent;
                return GetListDataArr(list);
            }

            T t = end.Data;
            end = end.Parent;
            return new T[1] { t };
        }

        /// <summary>
        /// 查找指定id的节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool ContainsNode(string id,Node<T> node)
        {
            if (node.Id == id)
            {
                return true;
            }

            if (node.Parent != null)
            {
                return ContainsNode(id, node.Parent);
            }
            return false;
        }
        
        /// <summary>
        /// 取得节点链表中的数据数组
        /// </summary>
        /// <returns>The node data arr.</returns>
        /// <param name="list">List.</param>
        private T[] GetListDataArr(List<Node<T>> list)
        {
            T[] tArr = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                tArr[i] = list[i].Data;
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
            string idStr = node.Id + GetChildrenId(node);
            if (node.Parent != null)
            {
                idStr = idStr + "-" + Traverse(node.Parent);
            }

            return idStr;
        }

        private string GetChildrenId(Node<T> node)
        {
            if (node.HaveChildren())
            {
                string str = "(";

                List<Node<T>> list = new List<Node<T>>();
                node.GetChildrenByList(ref list);
                for (int i = 0; i < list.Count; i++)
                {
                    str = str + list[i].Id + "-";
                }
                int lastIndex = -1;
                lastIndex = str.LastIndexOf('-');
                if (lastIndex != -1)
                {
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
        private Dictionary<string, Node<T>> children;

        #region 构造器

        public Node(string id, T val, Node<T> p)
        {
            this.id = id;
            data = val;
            parent = p;
        }

        public Node(string id, T val)
        {
            this.id = id;
            data = val;
            parent = null;
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
        public void AddChild(string id, T val)
        {
            if (children == null)
            {
                children = new Dictionary<string, Node<T>>();
            }
            Node<T> node = new Node<T>(id, val, this);
            children.Add(id, node);
        }

        /// <summary>
        /// 查找叶节点
        /// </summary>
        /// <param name="node">叶节点名</param>
        /// <returns></returns>
        public bool TryGetChild(string id, out Node<T> child)
        {
            child = null;
            if (HaveChildren())
            {
                child = children.GetValue(id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the children by list.
        /// </summary>
        /// <param name="list">List.</param>
        public void GetChildrenByList(ref List<Node<T>> list)
        {
            foreach (Node<T> child in children.Values)
            {
                list.Add(child);
            }
        }

        /// <summary>
        /// Gets the children count.
        /// </summary>
        /// <returns>The children count.</returns>
        public int GetChildrenCount()
        {
            return children.GetCount();
        }

        /// <summary>
        /// Containses the child.
        /// </summary>
        /// <returns><c>true</c>, if child was containsed, <c>false</c> otherwise.</returns>
        /// <param name="id">Identifier.</param>
        public bool ContainsChild(string id)
        {
            return children.includeKey(id);
        }

        /// <summary>
        /// 删除叶节点
        /// </summary>
        /// <param name="node">叶节点名</param>
        /// <returns></returns>
        public Node<T> RemoveChild(string id)
        {
            if (children.includeKey(id))
            {
                Node<T> node = children.GetValue(id);
                children.Remove(id);
                return node;
            }
            return null;
        }

        /// <summary>
        /// 清空子节点
        /// </summary>
        public void ClearChildren()
        {
            children.Clear();
        }

        /// <summary>
        /// Haves the children.
        /// </summary>
        /// <returns><c>true</c>, if children was had, <c>false</c> otherwise.</returns>
        public bool HaveChildren()
        {
            return children != null && children.Count != 0;
        }
    }
}
