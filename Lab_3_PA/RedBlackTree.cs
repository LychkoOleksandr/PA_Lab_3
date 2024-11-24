using System.Collections;
using System.Text.Json;

namespace Lab_3_PA;

[Serializable]
public class RedBlackTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    where TKey : IComparable<TKey>
{
    private enum NodeColor
    {
        Red,
        Black
    }

    private class Node
    {
        public TKey Key { get; }
        public TValue Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }
        public NodeColor Color { get; set; }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Color = NodeColor.Red; 
        }
    }

    private Node _root = null!;
    private int _count;

    public int Count => _count;

    public void Add(TKey key, TValue value)
    {
        if (SearchInternal(_root, key) != null)
        {
            throw new ArgumentException($"Key '{key}' already exists.");
        }

        var newNode = new Node(key, value);
        if (_root == null)
        {
            _root = newNode;
            _root.Color = NodeColor.Black; 
            _count++;
            return;
        }

        Node parent = null;
        Node current = _root;

        while (current != null)
        {
            parent = current;
            int comparison = key.CompareTo(current.Key);
            current = comparison < 0 ? current.Left : current.Right;
        }

        newNode.Parent = parent;
        if (key.CompareTo(parent.Key) < 0)
            parent.Left = newNode;
        else
            parent.Right = newNode;

        _count++;
        FixInsert(newNode);
    }


    public TValue Search(TKey key, out int comparisons)
    {
        comparisons = 0;
        Node current = _root;

        while (current != null)
        {
            comparisons++;
            int comparison = key.CompareTo(current.Key);
            if (comparison == 0)
                return current.Value;
            current = comparison < 0 ? current.Left : current.Right;
        }

        throw new KeyNotFoundException($"Key '{key}' not found.");
    }

    public bool Remove(TKey key)
    {
        Node nodeToDelete = SearchInternal(_root, key);
        if (nodeToDelete == null)
            return false; 

        DeleteNode(nodeToDelete); 
        _count--; 
        return true;
    }


    public void Clear()
    {
        _root = null;
        _count = 0;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return InOrderTraversal(_root).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private Node SearchInternal(Node node, TKey key)
    {
        while (node != null)
        {
            int comparison = key.CompareTo(node.Key);
            if (comparison == 0)
                return node;
            node = comparison < 0 ? node.Left : node.Right;
        }

        return null;
    }

    private IEnumerable<KeyValuePair<TKey, TValue>> InOrderTraversal(Node node)
    {
        if (node != null)
        {
            foreach (var pair in InOrderTraversal(node.Left))
                yield return pair;

            yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);

            foreach (var pair in InOrderTraversal(node.Right))
                yield return pair;
        }
    }

    private void FixInsert(Node node)
    {
        while (node != _root && node.Parent?.Color == NodeColor.Red)
        {
            Node parent = node.Parent;
            Node grandparent = parent?.Parent;

            if (grandparent == null) break; 

            if (parent == grandparent.Left)
            {
                Node uncle = grandparent.Right;

                if (uncle is { Color: NodeColor.Red }) 
                {
                    parent.Color = NodeColor.Black;
                    uncle.Color = NodeColor.Black;
                    grandparent.Color = NodeColor.Red;
                    node = grandparent;
                }
                else
                {
                    if (node == parent.Right) 
                    {
                        node = parent;
                        RotateLeft(node);
                    }
                        
                    parent.Color = NodeColor.Black;
                    grandparent.Color = NodeColor.Red;
                    RotateRight(grandparent);
                }
            }
            else
            {
                Node uncle = grandparent.Left;

                if (uncle != null && uncle.Color == NodeColor.Red) 
                {
                    parent.Color = NodeColor.Black;
                    uncle.Color = NodeColor.Black;
                    grandparent.Color = NodeColor.Red;
                    node = grandparent;
                }
                else
                {
                    if (node == parent.Left) 
                    {
                        node = parent;
                        RotateRight(node);
                    }

                    parent.Color = NodeColor.Black;
                    grandparent.Color = NodeColor.Red;
                    RotateLeft(grandparent);
                }
            }
        }

        if (_root != null)
        {
            _root.Color = NodeColor.Black;
        }
    }


    private void DeleteNode(Node node)
    {
        if (node == null) return;

        Node nodeToFix;
        NodeColor originalColor = node.Color;

        if (node.Left == null) 
        {
            nodeToFix = node.Right;
            ReplaceNode(node, node.Right); 
        }
        else if (node.Right == null) 
        {
            nodeToFix = node.Left;
            ReplaceNode(node, node.Left); 
        }
        else 
        {
            Node successor = FindMin(node.Right); 
            originalColor = successor.Color; 
            nodeToFix = successor.Right; 

            if (successor.Parent != node)
            {
                ReplaceNode(successor, successor.Right);
                successor.Right = node.Right;
                successor.Right.Parent = successor;
            }

            ReplaceNode(node, successor); 
            successor.Left = node.Left;
            successor.Left.Parent = successor;
            successor.Color = node.Color; 
        }

        if (originalColor == NodeColor.Black)
        {
            FixDelete(nodeToFix); 
        }
    }


    private void FixDelete(Node node)
    {
        while (node != null && node != _root && (node.Color == NodeColor.Black))
        {
            if (node.Parent == null)
                break; 

            if (node == node.Parent.Left) 
            {
                Node sibling = node.Parent.Right;

                if (sibling != null && sibling.Color == NodeColor.Red)
                {
                    sibling.Color = NodeColor.Black;
                    node.Parent.Color = NodeColor.Red;
                    RotateLeft(node.Parent);
                    sibling = node.Parent.Right;
                }

                if (sibling != null &&
                    (sibling.Left == null || sibling.Left.Color == NodeColor.Black) &&
                    (sibling.Right == null || sibling.Right.Color == NodeColor.Black))
                {
                    sibling.Color = NodeColor.Red;
                    node = node.Parent;
                }
                else
                {
                    if (sibling != null && (sibling.Right == null || sibling.Right.Color == NodeColor.Black))
                    {
                        if (sibling.Left != null) sibling.Left.Color = NodeColor.Black;
                        sibling.Color = NodeColor.Red;
                        RotateRight(sibling);
                        sibling = node.Parent.Right;
                    }

                    if (sibling != null)
                    {
                        sibling.Color = node.Parent.Color;
                        node.Parent.Color = NodeColor.Black;
                        if (sibling.Right != null) sibling.Right.Color = NodeColor.Black;
                        RotateLeft(node.Parent);
                        node = _root;
                    }
                }
            }
            else
            {
                Node sibling = node.Parent.Left;

                if (sibling != null && sibling.Color == NodeColor.Red)
                {
                    sibling.Color = NodeColor.Black;
                    node.Parent.Color = NodeColor.Red;
                    RotateRight(node.Parent);
                    sibling = node.Parent.Left;
                }

                if (sibling != null &&
                    (sibling.Left == null || sibling.Left.Color == NodeColor.Black) &&
                    (sibling.Right == null || sibling.Right.Color == NodeColor.Black))
                {
                    sibling.Color = NodeColor.Red;
                    node = node.Parent;
                }
                else
                {
                    if (sibling != null && (sibling.Left == null || sibling.Left.Color == NodeColor.Black))
                    {
                        if (sibling.Right != null) sibling.Right.Color = NodeColor.Black;
                        sibling.Color = NodeColor.Red;
                        RotateLeft(sibling);
                        sibling = node.Parent.Left;
                    }

                    if (sibling != null)
                    {
                        sibling.Color = node.Parent.Color;
                        node.Parent.Color = NodeColor.Black;
                        if (sibling.Left != null) sibling.Left.Color = NodeColor.Black;
                        RotateRight(node.Parent);
                        node = _root;
                    }
                }
            }
        }

        if (node != null) node.Color = NodeColor.Black;
    }




    private NodeColor GetColor(Node node) => node == null ? NodeColor.Black : node.Color;

    private Node FindMin(Node node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }

        return node;
    }

    private void ReplaceNode(Node oldNode, Node newNode)
    {
        if (oldNode.Parent == null) 
        {
            _root = newNode;
        }
        else if (oldNode == oldNode.Parent.Left) 
        {
            oldNode.Parent.Left = newNode;
        }
        else 
        {
            oldNode.Parent.Right = newNode;
        }

        if (newNode != null)
        {
            newNode.Parent = oldNode.Parent;
        }
    }



    private void RotateLeft(Node node)
    {
        Node rightChild = node.Right;
        node.Right = rightChild.Left;

        if (rightChild.Left != null)
        {
            rightChild.Left.Parent = node;
        }

        rightChild.Parent = node.Parent;

        if (node.Parent == null)
        {
            _root = rightChild;
        }
        else if (node == node.Parent.Left)
        {
            node.Parent.Left = rightChild;
        }
        else
        {
            node.Parent.Right = rightChild;
        }

        rightChild.Left = node;
        node.Parent = rightChild;
    }

    private void RotateRight(Node node)
    {
        Node leftChild = node.Left;
        node.Left = leftChild.Right;

        if (leftChild.Right != null)
        {
            leftChild.Right.Parent = node;
        }

        leftChild.Parent = node.Parent;

        if (node.Parent == null)
        {
            _root = leftChild;
        }
        else if (node == node.Parent.Left)
        {
            node.Parent.Left = leftChild;
        }
        else
        {
            node.Parent.Right = leftChild;
        }

        leftChild.Right = node;
        node.Parent = leftChild;
    }

    public bool ContainsKey(TKey key)
    {
        return SearchInternal(_root, key) != null;
    }

    public bool Update(TKey key, TValue newValue)
    {
        var node = SearchInternal(_root, key); 
        if (node == null)
        {
            return false; 
        }

        node.Value = newValue; 
        return true; 
    }
        
    public void SaveToFile(string fileName)
    {
        var treeData = new List<KeyValuePair<TKey, TValue>>();
        foreach (var pair in this)
        {
            treeData.Add(pair);
        }

        var json = JsonSerializer.Serialize(treeData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public static RedBlackTree<TKey, TValue> LoadFromFile(string fileName)
    {
        var tree = new RedBlackTree<TKey, TValue>();
        if (File.Exists(fileName))
        {
            var json = File.ReadAllText(fileName);
            var treeData = JsonSerializer.Deserialize<List<KeyValuePair<TKey, TValue>>>(json);

            foreach (var pair in treeData)
            {
                tree.Add(pair.Key, pair.Value);
            }
        }

        return tree;
    }
}