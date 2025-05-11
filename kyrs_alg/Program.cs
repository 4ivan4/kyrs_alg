using System;

public class SegmentTree
{
    private int[] tree;
    private int size;

    public SegmentTree(int[] array)
    {
        size = array.Length;
        tree = new int[4 * size]; 
        Build(array, 1, 0, size - 1);
    }

    private void Build(int[] array, int node, int left, int right)
    {
        if (left == right)
        {
            tree[node] = array[left];
        }
        else
        {
            int mid = (left + right) / 2;
            Build(array, 2 * node, left, mid);
            Build(array, 2 * node + 1, mid + 1, right);
            tree[node] = tree[2 * node] + tree[2 * node + 1]; 
        }
    }

    public int QuerySum(int l, int r)
    {
        return QuerySum(1, 0, size - 1, l, r);
    }

    private int QuerySum(int node, int nodeLeft, int nodeRight, int l, int r)
    {
        if (r < nodeLeft || l > nodeRight)
            return 0;

        if (l <= nodeLeft && nodeRight <= r)
            return tree[node];

        int mid = (nodeLeft + nodeRight) / 2;
        return QuerySum(2 * node, nodeLeft, mid, l, r) +
               QuerySum(2 * node + 1, mid + 1, nodeRight, l, r);
    }

    public void Update(int index, int newValue)
    {
        Update(1, 0, size - 1, index, newValue);
    }

    private void Update(int node, int nodeLeft, int nodeRight, int index, int newValue)
    {
        if (nodeLeft == nodeRight)
        {
            tree[node] = newValue;
        }
        else
        {
            int mid = (nodeLeft + nodeRight) / 2;
            if (index <= mid)
                Update(2 * node, nodeLeft, mid, index, newValue);
            else
                Update(2 * node + 1, mid + 1, nodeRight, index, newValue);

            tree[node] = tree[2 * node] + tree[2 * node + 1];
        }
    }

    public void PrintTree()
    {
        Console.WriteLine("Segment Tree:");
        PrintTree(1, 0, size - 1, 0);
    }

    private void PrintTree(int node, int left, int right, int level)
    {
        if (left > right) return;

        string indent = new string(' ', level * 4);
        if (left == right)
        {
            Console.WriteLine($"{indent}Leaf [{left}] = {tree[node]}");
        }
        else
        {
            Console.WriteLine($"{indent}Node [{left}-{right}] = {tree[node]}");
            int mid = (left + right) / 2;
            PrintTree(2 * node, left, mid, level + 1);
            PrintTree(2 * node + 1, mid + 1, right, level + 1);
        }
    }
}

class Program
{
    static void Main()
    {
        int[] array = { 1, 3, 5, 7, 9, 11 };
        SegmentTree st = new SegmentTree(array);

        st.PrintTree();

        Console.WriteLine("\nSum from index 1 to 4: " + st.QuerySum(1, 4));

        st.Update(2, 10);
        Console.WriteLine("\nAfter update:");
        st.PrintTree();
        Console.WriteLine("\nNew sum from index 1 to 4: " + st.QuerySum(1, 4));
    }
}