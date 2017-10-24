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
	public class UIListTree<T>
    {
        //根引用属性
        public ListNode<T> Root
        {
            get { return nodeList[0]; }
        }

        //末梢引用属性
        public ListNode<T> End
        {
            get { return nodeList[lastIndex]; }
        }


        private List<ListNode<T>> nodeList;

        private int lastIndex;

        #region 构造器

        /// <summary>
        /// 创建新节点，并作为新树根节点及叶节点
        /// </summary>
        /// <param name="val">Value.</param>
        public UIListTree(string id, T val)
        {
            ListNode<T> p = new ListNode<T>(id, val);

            nodeList = new List<ListNode<T>>();
            nodeList.Add(p);
            lastIndex = 0;
        }

        #endregion

        /// <summary>
        /// 判断是否只有根节点
        /// </summary>
        /// <returns></returns>
        public bool NotOnlyRoot()
        {
            //return root.Id != end.Id;
            return lastIndex != 0;
        }

        #region 树结构逻辑

        /// <summary>
        /// Pushs the end.
        /// </summary>
        /// <param name="val">Value.</param>/
        public void PushEnd(string id, T val)
        {
            ListNode<T> node = new ListNode<T>(id, val, nodeList[lastIndex]);

            nodeList.Add(node);
            lastIndex++;
        }

        /// <summary>
        /// Pops the end.
        /// </summary>
        /// <returns>The end.</returns>//
        public T[] PopEnd()
        {
            if (nodeList[lastIndex].HaveChildren())
            {
                T[] tArr = EndNodeDataArr();

                nodeList[lastIndex].ClearChildren();
                nodeList.RemoveAt(lastIndex--);
                return tArr;
            }

            T t = nodeList[lastIndex].Data;
            nodeList.RemoveAt(lastIndex--);
            return new T[1] { t };
        }


        public T[] Pull(string id)
        {
            int index = FindWithId(id);
            if (index != -1)
            {
                List<ListNode<T>> list = new List<ListNode<T>>();
                for (int i = lastIndex; i > index + 1; i--)
                {
                    nodeList[i].GetChildrenByList(ref list);
                    list.Add(nodeList[i]);
                    nodeList[i].ClearChildren();
                }
                nodeList.RemoveRange(index + 1, lastIndex - index);

                lastIndex = index;
                return GetListDataArr(list);
            }
            return null;
        }

        /// <summary>
        /// Peeks the end.
        /// </summary>
        /// <returns>The end.</returns>/
        public T[] PeekEnd()
        {
            if (nodeList[lastIndex].HaveChildren())
            {
                return EndNodeDataArr();
            }
            return new T[1] { nodeList[lastIndex].Data };
        }

        /// <summary>
        /// Adds the leaf.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="leaf">Leaf.</param>
        public void AddLeaf(string id, T leaf)
        {
            if (nodeList[lastIndex].ContainsChild(id))
                return;
            nodeList[lastIndex].AddChild(id, leaf);
        }

        /// <summary>
        /// Removes the leaf.
        /// </summary>
        /// <returns>The leaf.</returns>
        /// <param name="leaf">Leaf.</param>
        public T RemoveLeaf(string id)
        {
            return nodeList[lastIndex].RemoveChild(id).Data;
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
        private T[] EndNodeDataArr()
        {
            List<ListNode<T>> list = new List<ListNode<T>>();
            nodeList[lastIndex].GetChildrenByList(ref list);
            list.Add(nodeList[lastIndex]);
            return GetListDataArr(list);
        }

        /// <summary>
        /// Gets the node data arr.
        /// </summary>
        /// <returns>The node data arr.</returns>
        /// <param name="list">List.</param>
        private T[] GetListDataArr(List<ListNode<T>> list)
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
        public string Traverse(ListNode<T> node)
        {
            string idStr = node.Id + GetChildrenId(node);
            if (node.Parent != null)
            {
                idStr = idStr + "-" + Traverse(node.Parent);
            }

            return idStr;
        }

        private string GetChildrenId(ListNode<T> node)
        {
            if (node.HaveChildren())
            {
                string str = "(";

                List<ListNode<T>> list = new List<ListNode<T>>();
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

    public class ListNode<T>
    {
        //节点ID
        private string id;
        //数据域
        private T data;
        //父节点
        private ListNode<T> parent;
        //孩子集
        private Dictionary<string, ListNode<T>> children;

        #region 构造器

        public ListNode(string id, T val, ListNode<T> p)
        {
            this.id = id;
            data = val;
            parent = p;
        }

        public ListNode(string id, T val)
        {
            this.id = id;
            data = val;
            parent = null;
        }

        public ListNode(string id, ListNode<T> p)
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
        public ListNode<T> Parent
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
                children = new Dictionary<string, ListNode<T>>();
            }
            ListNode<T> node = new ListNode<T>(id, val, this);
            children.Add(id, node);
        }

        /// <summary>
        /// 查找叶节点
        /// </summary>
        /// <param name="node">叶节点名</param>
        /// <returns></returns>
        public bool TryGetChild(string id, out ListNode<T> child)
        {
            return children.TryGetValue(id, out child);
        }

        /// <summary>
        /// Gets the children by list.
        /// </summary>
        /// <param name="list">List.</param>
        public void GetChildrenByList(ref List<ListNode<T>> list)
        {
            if (HaveChildren())
            {
                foreach (ListNode<T> child in children.Values)
                {
                    list.Add(child);
                }
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
        public ListNode<T> RemoveChild(string id)
        {
            if (children.ContainsKey(id))
            {
                ListNode<T> node = children.GetValue(id);
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
            if (children != null)
            {
                children.Clear();
            }
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
