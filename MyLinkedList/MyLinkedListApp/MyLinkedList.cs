using System.Text;

namespace MyLinkedListApp;

public class MyLinkedList<T>
{
    #region Properties

    private Node<T> Head { get; set; }
    private Node<T> Tail { get; set; }

    public int Count
    {
        get
        {
            int cnt = 0;

            for (Node<T> node = Head; node != null; node = node.Next)
            {
                cnt++;
            }

            return cnt;
        }
    }

    #endregion

    #region Insert Methods

    public void AddAtStart(T data)
    {
        InsertAt(0, data);
    }

    public void AddAtEnd(T data)
    {
        InsertAt(Count, data);
    }

    public void InsertAt(int index, T data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data), "Invalid Parameter");
        }

        int count = Count;

        if (index < 0 || index > count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
        }

        Node<T> newNode = new Node<T>(data);

        // Case 1: List is empty
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
            return;
        }

        // Case 2: Insert at the start
        if (index == 0)
        {
            newNode.Next = Head;
            Head.Prev = newNode;
            Head = newNode;
            return;
        }

        // Case 3: Insert at the end
        if (index == count)
        {
            Tail.Next = newNode;
            newNode.Prev = Tail;
            Tail = newNode;
            return;
        }

        // Case 4: Insert in the middle (before the node currently at index)
        Node<T> current = GetNodeAt(index);

        newNode.Prev = current.Prev;
        newNode.Next = current;
        current.Prev.Next = newNode;
        current.Prev = newNode;
    }

    #endregion

    #region Remove Methods

    public T RemoveFirst()
    {
        return RemoveAt(0);
    }

    public T RemoveLast()
    {
        return RemoveAt(Count - 1);
    }

    public T RemoveAt(int index)
    {
        int count = Count;

        if (count == 0)
        {
            throw new InvalidOperationException("List is empty.");
        }

        if (index < 0 || index >= count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
        }

        Node<T> target = GetNodeAt(index);

        // Unlink from the previous node (or move Head if removing the first node)
        if (target.Prev == null)
        {
            Head = target.Next;
        }
        else
        {
            target.Prev.Next = target.Next;
        }

        // Unlink from the next node (or move Tail if removing the last node)
        if (target.Next == null)
        {
            Tail = target.Prev;
        }
        else
        {
            target.Next.Prev = target.Prev;
        }

        // Clear the removed node's links
        target.Next = null;
        target.Prev = null;

        return target.Data;
    }

    #endregion

    #region Other Methods

    public bool Contains(T other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other), "Invalid Parameter");
        }

        for (Node<T> node = Head; node != null; node = node.Next)
        {
            if (node.Data.Equals(other)) return true;
        }

        return false;
    }

    private Node<T> GetNodeAt(int index)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index cannot be negative.");
        }

        Node<T> current = Head;
        for (int i = 0; i < index && current != null; i++)
        {
            current = current.Next;
        }

        if (current == null)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
        }

        return current;
    }

    #endregion
    
    #region ToString
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (Node<T> node = Head; node != null; node = node.Next)
        {
            sb.AppendLine(node.Data.ToString());
        }
        return sb.ToString();
    }
    
    #endregion
}