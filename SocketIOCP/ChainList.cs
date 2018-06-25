using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIOCP
{
    public class ChainList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        private Node<T> current;

        private int count;

        public void Add(T element)
        {
            Node<T> node = new Node<T>(element);

            if (this.head == null)
            {
                this.head = node;
                this.tail = node;

                this.count = 1;

                return;
            }


            this.tail.Next = node;
            node.Before = this.tail;
            this.tail = node;

            this.count++;
        }

        //public Node<T> Get()
        //{
        //    if (this.head == null)
        //        return null;

        //    Node<T> node = this.head;
        //    this.head = node.Next;

        //    this.count--;

        //    return node;
        //}

        public void Remove(Node<T> node)
        {
            if (node == this.head)
            {
                
                this.head = this.head.Next;

                this.count--;

                return;

            }

            node.Before.Next = node.Next;
            node.Next.Before = node.Before;

            this.count--;

            this.current = node.Next;
        }

        public Node<T> Visit()
        {
            if (this.current == null)
                this.current = this.head;

            return this.current;
        }

        public bool MoveNext()
        {
            if (this.current == null)
            {
                if (this.head == null)
                {
                    return false;
                }
                else
                {
                    this.current = this.head;
                    return true;
                }
            }

            if (this.current.Next == null)
            {
                return false;
            }

            this.current = this.current.Next;

            return true;
        }

        public bool MoveToHead()
        {
            if (this.head == null)
            {
                return false;
            }

            this.current = this.head;
            return true;
        }

        public int Count
        {
            get { return this.count; }
        }

        public class Node<T>
        {
            public Node<T> Before;
            public Node<T> Next;

            public T element;

            public Node(T element)
            {
                this.element = element;
            }
        }
    }
}
